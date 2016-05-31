#!/bin/sh
# Must compile these sources with an older version of Java (1.6) in order to be compatible with Unity
# Dex errors are produced during Unity build process when compiling with Java > 1.6
echo ""
echo "Building Interface..."
/Library/Java/JavaVirtualMachines/1.6.0_65-b14-462.jdk/Contents/Home/bin/javac GimbalUnityInterface.java -cp "$ANDROID_SDK_ROOT/platforms/android-23/android.jar:$ANDROID_SDK_ROOT/extras/android/support/v13/android-support-v13.jar:../gimbal.jar:gimbal-slf4j-impl.jar:slf4j-api.jar:/Applications/Unity/PlaybackEngines/AndroidPlayer/Variations/mono/Release/Classes/classes.jar" -d .

echo ""
echo "Signature dump of Interface..."

/Library/Java/JavaVirtualMachines/1.6.0_65-b14-462.jdk/Contents/Home/bin/javap -s com.gimbal.ScriptBridge.GimbalUnityInterface

echo "Creating GimbalUnityInterface.jar..."
/Library/Java/JavaVirtualMachines/1.6.0_65-b14-462.jdk/Contents/Home/bin/jar cvfM ../GimbalUnityInterface.jar com/

echo ""
echo "Compiling GimbalUnityBridge.cpp..."
$ANDROID_NDK_ROOT/ndk-build NDK_PROJECT_PATH=. NDK_APPLICATION_MK=Application.mk $*
mv libs/armeabi/libgimbalunitybridge.so ..

echo ""
echo "Cleaning up / removing build folders..."
rm -rf libs
rm -rf obj
rm -rf com

echo ""
echo "Done!"
