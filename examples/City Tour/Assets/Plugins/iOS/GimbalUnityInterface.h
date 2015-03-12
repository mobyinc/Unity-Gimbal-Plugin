
#import <UIKit/UIKit.h>
#import <Gimbal/Gimbal.h>

#import "AppDelegateListener.h"
@interface GimbalUnityInterface : NSObject <AppDelegateListener, GMBLBeaconManagerDelegate, GMBLPlaceManagerDelegate>

+(GimbalUnityInterface*)sharedInstance;

@property (strong, nonatomic) GMBLBeaconManager* beaconManager;
@property (strong, nonatomic) GMBLPlaceManager* placeManager;

@end