
#import <UIKit/UIKit.h>
#import <Gimbal/Gimbal.h>

#import "AppDelegateListener.h"
@interface GimbalUnityInterface : NSObject <AppDelegateListener>

+(GimbalUnityInterface*)sharedInstance;

@end