#include <stdlib.h>
#include <jni.h>
#include <android/log.h>

extern "C"
{

JavaVM*		java_vm;
jobject		JavaClass;
jmethodID 	setApiKeyJ;
jmethodID	startBeaconManagerJ;
jmethodID	stopBeaconManagerJ;
jmethodID	startPlaceManagerJ;
jmethodID	stopPlaceManagerJ;
jmethodID	isMonitoringJ;

jint JNI_OnLoad(JavaVM* vm, void* reserved)
{
	java_vm = vm;

	// attach our thread to the java vm
	JNIEnv* jni_env = 0;
	java_vm->AttachCurrentThread(&jni_env, 0);

	// try to find our main activity
	jclass cls_Activity		= jni_env->FindClass("com/unity3d/player/UnityPlayer");
	jfieldID fid_Activity	= jni_env->GetStaticFieldID(cls_Activity, "currentActivity", "Landroid/app/Activity;");
	jobject obj_Activity	= jni_env->GetStaticObjectField(cls_Activity, fid_Activity);

	// create a JavaClass object for GimbalUnityInterface
	jclass cls_JavaClass	= jni_env->FindClass("com/gimbal/ScriptBridge/GimbalUnityInterface");
	jmethodID mid_JavaClass	= jni_env->GetMethodID(cls_JavaClass, "<init>", "(Landroid/app/Activity;)V");
	jobject obj_JavaClass	= jni_env->NewObject(cls_JavaClass, mid_JavaClass, obj_Activity);

	// create a global reference to the JavaClass object and fetch method id(s)
	JavaClass	= jni_env->NewGlobalRef(obj_JavaClass);
	setApiKeyJ = jni_env->GetMethodID(cls_JavaClass, "setApiKey", "(Ljava/lang/String;)V");
	startBeaconManagerJ = jni_env->GetMethodID(cls_JavaClass, "startBeaconManager", "()V");
	stopBeaconManagerJ = jni_env->GetMethodID(cls_JavaClass, "stopBeaconManager", "()V");
	startPlaceManagerJ = jni_env->GetMethodID(cls_JavaClass, "startPlaceManager", "()V");
	stopPlaceManagerJ = jni_env->GetMethodID(cls_JavaClass, "stopPlaceManager", "()V");
	isMonitoringJ = jni_env->GetMethodID(cls_JavaClass, "isMonitoring", "()B");

	return JNI_VERSION_1_6; // minimum JNI version
}

void setApiKey(char* apiKey){
	JNIEnv* jni_env = 0;
	java_vm->AttachCurrentThread(&jni_env, 0);
	jstring jApiKey = jni_env->NewStringUTF(apiKey);
	jni_env->CallVoidMethod(JavaClass, setApiKeyJ, jApiKey);
}

void startBeaconManager() {
	JNIEnv* jni_env = 0;
	java_vm->AttachCurrentThread(&jni_env, 0);
	jni_env->CallVoidMethod(JavaClass, startBeaconManagerJ);
}

void stopBeaconManager() {
	JNIEnv* jni_env = 0;
	java_vm->AttachCurrentThread(&jni_env, 0);
	jni_env->CallVoidMethod(JavaClass, stopBeaconManagerJ);
}

void startPlaceManager() {
	JNIEnv* jni_env = 0;
	java_vm->AttachCurrentThread(&jni_env, 0);
	jni_env->CallVoidMethod(JavaClass, startPlaceManagerJ);
}

void stopPlaceManager() {
	JNIEnv* jni_env = 0;
	java_vm->AttachCurrentThread(&jni_env, 0);
	jni_env->CallVoidMethod(JavaClass, stopPlaceManagerJ);
}

bool isMonitoring() {
	JNIEnv* jni_env = 0;
	java_vm->AttachCurrentThread(&jni_env, 0);
	return jni_env->CallObjectMethod(JavaClass, isMonitoringJ);
}

}