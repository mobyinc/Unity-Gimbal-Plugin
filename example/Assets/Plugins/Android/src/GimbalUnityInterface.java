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

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

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

		placeEventListener = new PlaceEventListener() {
			@Override
			public void onVisitStart(Visit visit) {
				Log.i("INFO", visit.toString());

				try {
					JSONObject jsonObj = new JSONObject();
					String myname = "chris";
					jsonObj.put("name", myname);
				} catch(JSONException ex) {
					ex.printStackTrace();
				}
			}

			@Override
			public void onVisitEnd(Visit visit) {
				Log.i("INFO", visit.toString());
			}
		};

		PlaceManager.getInstance().addListener(placeEventListener);

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

	public void startPlaceManager() 
	{
		PlaceManager.getInstance().startMonitoring();
	}

	public void stopPlaceManager()
	{
		PlaceManager.getInstance().stopMonitoring();
	}

	public boolean isMonitoring()
	{
		return PlaceManager.getInstance().isMonitoring();
	} 

	public void startBeaconManager()
	{
		beaconManager.startListening();
	}

	public void stopBeaconManager()
	{
		beaconManager.stopListening();
	}
}
