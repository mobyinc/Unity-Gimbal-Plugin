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

		beaconSightingListener = new BeaconEventListener() {
	      @Override
	      public void onBeaconSighting(BeaconSighting sighting) {
	        Log.i("INFO", sighting.toString());
	      }
	    };

	    beaconManager = new BeaconManager();
	    beaconManager.addListener(beaconSightingListener);
	    
		Log.i("GimbalUnityInterface", "Constructor called with currentActivity = " + currentActivity);
		activity = currentActivity;

		// placeEventListener = new PlaceEventListener() {
		// 	@Override
		// 	public void onVisitStart(Visit visit) {
		// 		Log.i("INFO", visit.toString());
		// 	}

		// 	@Override
		// 	public void onVisitEnd(Visit visit) {
		// 		Log.i("INFO", visit.toString());
		// 	}
		// };

		// PlaceManager.getInstance().addListener(placeEventListener);
	}

	public void setApiKey(String apiKey)
	{
		Log.i("Set API Key", "Key: " + apiKey);
		Gimbal.setApiKey(activity.getApplication(), apiKey);
	}

	public void startBeaconManager()
	{
		Log.i("Starting", "Starting Beacon Manager");
		beaconManager.startListening();
	}

	public void stopBeaconManager()
	{
		Log.i("Stopping", "Stopping Beacon Manager");
		beaconManager.stopListening();
	}

	public void startPlaceManager() 
	{
		// PlaceManager.getInstance().startMonitoring();
	}

	public void stopPlaceManager()
	{
		// PlaceManager.getInstance().stopMonitoring();
	}

	public boolean isMonitoring()
	{
		// return PlaceManager.getInstance().isMonitoring();
		return true;
	} 
}
