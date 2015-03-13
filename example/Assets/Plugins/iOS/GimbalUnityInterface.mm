#include "GimbalUnityInterface.h"

static GimbalUnityInterface *_instance = [GimbalUnityInterface sharedInstance];
const char *_gimbalObjName = "GimbalPlugin";

@implementation GimbalUnityInterface

+(GimbalUnityInterface*)sharedInstance {
    return _instance;
}

+(void)initialize {
    if(!_instance) {
        _instance = [[GimbalUnityInterface alloc] init];
    }
}

-(id)init {
    if (_instance != nil) {
        return _instance;
    }
    
    self = [super init];
    
    if (!self) {
        return nil;
    }
    
    _instance = self;
    
    UnityRegisterAppDelegateListener(self);
    
    _beaconManager = [[GMBLBeaconManager alloc] init];
    _beaconManager.delegate = self;
    
    _placeManager = [[GMBLPlaceManager alloc] init];
    _placeManager.delegate = self;
    
    return self;
}

-(void)didFinishLaunching:(NSNotification *)notification {
    // Do stuff when app finishes launching
}

-(void)setApiKey:(NSString *) apiKey {
    [Gimbal setAPIKey:apiKey options:nil];
}

-(void)beaconManager:(GMBLBeaconManager *)manager didReceiveBeaconSighting:(GMBLBeaconSighting *)sighting {
    //Convert sighting date to string
    NSString *dateString = [self convertDate:sighting.date];
    
    GMBLBeacon* beacon = sighting.beacon;
    
    //Test to see if beacon icon url exists
    //If not, set to empty string
    NSString *beaconIconUrl = beacon.iconURL;
    if (!beaconIconUrl) {
        beaconIconUrl = @"";
    }
    
    //Create dictionary for sighting values
    NSDictionary* sightingDictionary = @{
                                         @"RSSI": [NSString stringWithFormat:@"%ld", (long)sighting.RSSI],
                                         @"date": dateString,
                                         @"beacon": @{
                                                 @"batteryLevel": [NSString stringWithFormat:@"%ld", (long)beacon.batteryLevel],
                                                 @"iconURL": beaconIconUrl,
                                                 @"identifier": beacon.identifier,
                                                 @"name": beacon.name,
                                                 @"temperature": [NSString stringWithFormat:@"%ld", (long)beacon.temperature]
                                                 }
                                         };
    
    //Convert dictionary to json data
    NSData* JSONData = [NSJSONSerialization dataWithJSONObject:sightingDictionary options:NSJSONReadingMutableContainers error:nil];
    
    //Convert json data to string
    NSString* JSONString = [[NSString alloc] initWithBytes:[JSONData bytes] length:[JSONData length] encoding:NSUTF8StringEncoding];
    
    //Pass json data to unity game object
    UnitySendMessage(_gimbalObjName, "OnBeaconSighting", [JSONString UTF8String]);
}

-(void)startBeaconManager {
    [_beaconManager startListening];
}

-(void)stopBeaconManager {
    [_beaconManager stopListening];
}

-(void)placeManager:(GMBLPlaceManager *)manager didBeginVisit:(GMBLVisit *)visit {
    [self placeManagerHelper:visit :@"OnBeginVisit"];
}

-(void)placeManager:(GMBLPlaceManager *)manager didEndVisit:(GMBLVisit *)visit {
    [self placeManagerHelper:visit :@"OnEndVisit"];
}

-(void)placeManagerHelper:(GMBLVisit*) visit :(NSString*) unityMethod {
    //Convert vist arival and departure dates to strings
    NSString *arrivalDate = [self convertDate:visit.arrivalDate];
    NSString *departureDate = [self convertDate:visit.departureDate];
    
    GMBLPlace* place = visit.place;
    
    //Create dictionary for visit values
    NSDictionary* visitDictionary = @{
                                      @"arrivalDate": arrivalDate,
                                      @"departureDate": departureDate,
                                      @"place": @{
                                              @"identifier": place.identifier,
                                              @"name": place.name,
                                              }
                                      };
    //Convert dictionary to json data
    NSData* JSONData = [NSJSONSerialization dataWithJSONObject:visitDictionary options:NSJSONReadingMutableContainers error:nil];
    
    //Convert json data to string
    NSString* JSONString = [[NSString alloc] initWithBytes:[JSONData bytes] length:[JSONData length] encoding:NSUTF8StringEncoding];
    
    //Pass json data to unity game object
    UnitySendMessage(_gimbalObjName, [unityMethod UTF8String], [JSONString UTF8String]);
}

-(void)startPlaceManager {
    [_placeManager startMonitoring];
}

-(void)stopPlaceManager {
    [_placeManager stopMonitoring];
}

-(NSString*)convertDate:(NSDate*) date {
    //If date is nil return empty string
    if (date == nil) {
        return @"";
    }
    
    //Convert NSDate to string
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
    [dateFormatter setDateStyle:NSDateFormatterFullStyle];
    NSString *dateString = [dateFormatter stringFromDate:date];
    [dateFormatter release];
    return dateString;
}

// Unity interface
extern "C" {
    void setApiKey(const char* apiKey) {
        
        [[GimbalUnityInterface sharedInstance] setApiKey:[NSString stringWithUTF8String:apiKey]];
        
        NSLog(@"setting API Key: %s", apiKey);
        //UnitySendMessage(_gimbalObjName, "OnFoo", "bar");
    }
    
    void startBeaconManager() {
        [[GimbalUnityInterface sharedInstance] startBeaconManager];
    }
    
    void stopBeaconManager() {
        [[GimbalUnityInterface sharedInstance] stopBeaconManager];
    }
    
    void startPlaceManager() {
        [[GimbalUnityInterface sharedInstance] startPlaceManager];
    }
    
    void stopPlaceManager() {
        [[GimbalUnityInterface sharedInstance] stopPlaceManager];
    }
}

@end