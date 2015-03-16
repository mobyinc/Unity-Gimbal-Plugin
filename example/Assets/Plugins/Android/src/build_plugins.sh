#!/bin/sh
# Must compile these sources with an older version of java in order to be compatible with Unity
echo ""
echo "Building Interface..."
# javac GimbalUnityInterface.java -bootclasspath $ANDROID_SDK_ROOT/platforms/android-21/android.jar -d .
/Library/Java/JavaVirtualMachines/1.6.0_65-b14-462.jdk/Contents/Home/bin/javac GimbalUnityInterface.java -cp "$ANDROID_SDK_ROOT/platforms/android-21/android.jar:../gimbal.jar:gimbal-dev-logging.jar:spring-android-core-1.0.1.RELEASE.jar:spring-android-rest-template-1.0.1.RELEASE.jar" -d .

echo ""
echo "Signature dump of Interface..."

# javap -s org.example.ScriptBridge.GimbalUnityInterface
/Library/Java/JavaVirtualMachines/1.6.0_65-b14-462.jdk/Contents/Home/bin/javap -s org.example.ScriptBridge.GimbalUnityInterface

echo "Creating GimbalUnityInterface.jar..."
# jar cvfM ../GimbalUnityInterface.jar org/
/Library/Java/JavaVirtualMachines/1.6.0_65-b14-462.jdk/Contents/Home/bin/jar cvfM ../GimbalUnityInterface.jar org/

echo ""
echo "Compiling GimbalUnityBridge.cpp..."
$ANDROID_NDK_ROOT/ndk-build NDK_PROJECT_PATH=. NDK_APPLICATION_MK=Application.mk $*
mv libs/armeabi/libgimbalunitybridge.so ..

echo ""
echo "Cleaning up / removing build folders..."  #optional..
rm -rf libs
rm -rf obj
rm -rf org

echo ""
echo "Done!"
