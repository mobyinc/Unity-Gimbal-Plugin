package org.example.ScriptBridge;

import android.app.Activity;
import android.util.Log;
import java.io.File;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

import com.gimbal.android.Gimbal;
import com.gimbal.android.PlaceManager;
import com.gimbal.android.PlaceEventListener;
import com.gimbal.android.Place;
import com.gimbal.android.Visit;
import com.gimbal.android.BeaconEventListener;
import com.gimbal.android.BeaconManager;
import com.gimbal.android.BeaconSighting;
import com.gimbal.android.Beacon;

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

		beaconSightingListener = new BeaconEventListener() {
	    	@Override
	    	public void onBeaconSighting(BeaconSighting sighting) {
	      		try {
	      			String dateString = convertDate(sighting.getTimeInMillis());

		      		JSONObject jsonObj = new JSONObject();
		        	jsonObj.put("RSSI", String.valueOf(sighting.getRSSI()));
		        	jsonObj.put("date", dateString);

		        	Beacon beacon = sighting.getBeacon();
		        	JSONObject jsonAdd = new JSONObject();
		        	jsonAdd.put("batteryLevel", String.valueOf(beacon.getBatteryLevel()));
		        	jsonAdd.put("icontURL", beacon.getIconURL());
		        	jsonAdd.put("identifier", beacon.getIdentifier());
		        	jsonAdd.put("name", beacon.getName());
		        	jsonAdd.put("temperature", String.valueOf(beacon.getTemperature()));

		        	jsonObj.put("beacon", jsonAdd);

		        	String jsonString = jsonObj.toString();
	      		}
	      		catch (JSONException ex) {
	      			ex.printStackTrace();
	      		}
	      	}
	    };

	    beaconManager = new BeaconManager();
	    beaconManager.addListener(beaconSightingListener);

		placeEventListener = new PlaceEventListener() {
			@Override
			public void onVisitStart(Visit visit) {
				placeManagerHelper(visit, "unityMethod");
			}

			@Override
			public void onVisitEnd(Visit visit) {
				placeManagerHelper(visit, "unityMethod");
			}
		};
		PlaceManager.getInstance().addListener(placeEventListener);
	}

	private void placeManagerHelper(Visit visit, String unityMethod) {
		try {
			String arrivalDate = convertDate(visit.getArrivalTimeInMillis());
			String departureDate = convertDate(visit.getDepartureTimeInMillis());
			JSONObject jsonObj = new JSONObject();
			jsonObj.put("arrivalDate", arrivalDate);
	        jsonObj.put("departureDate", departureDate);

			Place place = visit.getPlace();
			JSONObject jsonAdd = new JSONObject();
			jsonAdd.put("identifier", place.getIdentifier());
			jsonAdd.put("name", place.getName());

			jsonObj.put("place", jsonAdd);

			String jsonString = jsonObj.toString();
			//com.unity3d.player.UnityPlayer.UnitySendMessage("Name", "MethodName", "Parameter");
		}
		catch (JSONException ex) {
	      	ex.printStackTrace();
	    }
	}

	private String convertDate(Long date) {
		DateFormat df = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
		String stringDate = df.format(date);
		return stringDate;
	}

		public void setApiKey(String apiKey)
	{
		Gimbal.setApiKey(activity.getApplication(), apiKey);
	}

	public void startBeaconManager()
	{	
		beaconManager.startListening();	
	}

	public void stopBeaconManager()
	{
		beaconManager.stopListening();
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
}
