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
  if (_instance != nil) return _instance;

  self = [super init];

  if (!self) return nil;

  _instance = self;

  UnityRegisterAppDelegateListener(self);

  return self;
}

-(void)didFinishLaunching:(NSNotification *)notification {
  // Do stuff when app finishes launching
}

// Unity interface

extern "C" {
  void setApiKey(const char* apiKey) {
    NSLog(@"setting API Key: %s", apiKey);
    UnitySendMessage(_gimbalObjName, "OnFoo", "bar");
  }
}

@end