package org.example.ScriptBridge;

import android.app.Activity;
import android.util.Log;
import java.io.File;

import com.gimbal.android.Gimbal;
import com.gimbal.android.PlaceManager;
import com.gimbal.android.PlaceEventListener;
import com.gimbal.android.Place;
import com.gimbal.android.Visit;
import com.gimbal.android.BeaconEventListener;
import com.gimbal.android.BeaconManager;
import com.gimbal.android.BeaconSighting;

public class GimbalUnityInterface
{
	private Activity activity;
	private PlaceEventListener placeEventListener;
	private BeaconEventListener beaconSightingListener;
	private BeaconManager beaconManager;

	public GimbalUnityInterface(Activity currentActivity)
	{
		Log.i("GimbalUnityInterface", "Constructor called with currentActivity = " + currentActivity);
		activity = currentActivity;

		beaconSightingListener = new BeaconEventListener() {
      @Override
      public void onBeaconSighting(BeaconSighting sighting) {
        Log.i("INFO", sighting.toString());
      }
    };

    beaconManager = new BeaconManager();
    beaconManager.addListener(beaconSightingListener);
	}

	public void setApiKey(String apiKey)
	{
		Gimbal.setApiKey(activity.getApplication(), apiKey);
	}

	public void startBeaconManager()
	{

	}

	public void stopBeaconManager()
	{

	}
}
