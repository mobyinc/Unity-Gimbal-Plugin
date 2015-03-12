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
    //Convert NSDate to string
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
    [dateFormatter setDateStyle:NSDateFormatterFullStyle];
    NSDate* date = sighting.date;
    NSString *dateString = [dateFormatter stringFromDate:date];
    [dateFormatter release];
    
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
    
}

-(void)placeManager:(GMBLPlaceManager *)manager didEndVisit:(GMBLVisit *)visit {
    
}

-(void)isMonitoring {
    
}

-(void)startPlaceManager {
    [_placeManager startMonitoring];
}

-(void)stopPlaceManager {
    [_placeManager stopMonitoring];
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