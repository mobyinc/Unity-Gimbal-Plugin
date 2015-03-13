#include <stdlib.h>
#include <jni.h>
#include <android/log.h>

extern "C"
{

JavaVM*		java_vm;
jobject		JavaClass;
jmethodID	getActivityCacheDir;

jint JNI_OnLoad(JavaVM* vm, void* reserved)
{
	// use __android_log_print for logcat debugging...
	// __android_log_print(ANDROID_LOG_INFO, "JavaBridge", "[%s] Creating java link", __FUNCTION__);
	java_vm = vm;

	// attach our thread to the java vm; obviously it's already attached but this way we get the JNIEnv..
	JNIEnv* jni_env = 0;
	java_vm->AttachCurrentThread(&jni_env, 0);

	// first we try to find our main activity..
	jclass cls_Activity		= jni_env->FindClass("com/unity3d/player/UnityPlayer");
	jfieldID fid_Activity	= jni_env->GetStaticFieldID(cls_Activity, "currentActivity", "Landroid/app/Activity;");
	jobject obj_Activity	= jni_env->GetStaticObjectField(cls_Activity, fid_Activity);

	// create a JavaClass object...
	jclass cls_JavaClass	= jni_env->FindClass("org/example/ScriptBridge/GimbalUnityInterface");
	jmethodID mid_JavaClass	= jni_env->GetMethodID(cls_JavaClass, "<init>", "(Landroid/app/Activity;)V");
	jobject obj_JavaClass	= jni_env->NewObject(cls_JavaClass, mid_JavaClass, obj_Activity);

	// create a global reference to the JavaClass object and fetch method id(s)..
	JavaClass			= jni_env->NewGlobalRef(obj_JavaClass);
	getActivityCacheDir	= jni_env->GetMethodID(cls_JavaClass, "getActivityCacheDir", "()Ljava/lang/String;");

	return JNI_VERSION_1_6;		// minimum JNI version
}

char* cacheDir = 0;
const char* getCacheDir()
{
	if (cacheDir)
		return cacheDir;

	JNIEnv* jni_env = 0;
	java_vm->AttachCurrentThread(&jni_env, 0);

	jstring str_cacheDir 	= (jstring)jni_env->CallObjectMethod(JavaClass, getActivityCacheDir);

	jsize stringLen = jni_env->GetStringUTFLength(str_cacheDir);

	cacheDir = new char[stringLen+1];

	const char* path = jni_env->GetStringUTFChars(str_cacheDir, 0);

	strcpy(cacheDir, path);

	return cacheDir;
}

} // extern "C"