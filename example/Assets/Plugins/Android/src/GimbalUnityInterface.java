package org.example.ScriptBridge;

import android.app.Activity;
import android.util.Log;
import java.io.File;

public class GimbalUnityInterface
{
	private Activity mActivity;
	public GimbalUnityInterface(Activity currentActivity)
	{
		Log.i("GimbalUnityInterface", "Constructor called with currentActivity = " + currentActivity);
		mActivity = currentActivity;
	}
	
	// we could of course do this straight from native code using JNI, but this is an example so.. ;)
	public String getActivityCacheDir()
	{
		// calling Context.getCacheDir();
		// http://developer.android.com/reference/android/content/Context.html#getCacheDir()
		//
		File cacheDir = mActivity.getCacheDir();
		String path = cacheDir.getPath();
		Log.i("GimbalUnityInterface", "getActivityCacheDir returns = " + path);
		return path;
	}
}
