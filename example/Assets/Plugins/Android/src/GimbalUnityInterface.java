package com.gimbal.ScriptBridge;

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

import com.unity3d.player.UnityPlayerActivity;
import com.unity3d.player.UnityPlayer;

public class GimbalUnityInterface
{
  private final String LOG_ID = "GimbalUnityInterface";

  private Activity activity;
  private PlaceEventListener placeEventListener;
  private BeaconEventListener beaconSightingListener;
  private BeaconManager beaconManager;

  public GimbalUnityInterface(Activity currentActivity)
  {
    Log.i(LOG_ID, "GimbalUnityInterface created with activity: " + currentActivity);

    activity = currentActivity;

    placeEventListener = new PlaceEventListener() {
      @Override
      public void onVisitStart(Visit visit) {
        placeManagerHelper(visit, "OnBeginVisit");
      }

      @Override
      public void onVisitEnd(Visit visit) {
        placeManagerHelper(visit, "OnEndVisit");
      }
    };

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

          Log.i(LOG_ID, "Beacon sighted: " + beacon.getIdentifier());

          String jsonString = jsonObj.toString();
          com.unity3d.player.UnityPlayer.UnitySendMessage("GimbalPlugin", "OnBeaconSighting", jsonString);
        }
        catch (JSONException ex) {
          ex.printStackTrace();
        }
      }
    };
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

      Log.i(LOG_ID, "Places event: " + unityMethod + ", " + place.getIdentifier());

      String jsonString = jsonObj.toString();
      com.unity3d.player.UnityPlayer.UnitySendMessage("GimbalPlugin", unityMethod, jsonString);
    }
    catch (JSONException ex) {
      ex.printStackTrace();
    }
  }

  private String convertDate(Long date) {
    if (date == null) {
      return "N/A";
    }

    DateFormat df = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
    String stringDate = df.format(date);

    return stringDate;
  }

  public void setApiKey(final String apiKey)
  {
    Log.i(LOG_ID, "Set API Key: " + apiKey);

    activity.runOnUiThread(new Runnable() {
      public void run() {
        Gimbal.setApiKey(activity.getApplication(), apiKey);

        beaconManager = new BeaconManager();
        beaconManager.addListener(beaconSightingListener);

        PlaceManager.getInstance().addListener(placeEventListener);
      }
    });
  }

  public void startBeaconManager()
  {
    Log.i(LOG_ID, "Starting Beacon Manager");

    if (beaconManager != null) {
      beaconManager.startListening();
    }
  }

  public void stopBeaconManager()
  {
    Log.i(LOG_ID, "Stopping Beacon Manager");

    if (beaconManager != null) {
      beaconManager.stopListening();
    }
  }

  public void startPlaceManager()
  {
    Log.i(LOG_ID, "Starting Place Manager");

    activity.runOnUiThread(new Runnable() {
      public void run() {
        PlaceManager.getInstance().startMonitoring();
      }
    });
  }

  public void stopPlaceManager()
  {
    Log.i(LOG_ID, "Stopping Place Manager");

    activity.runOnUiThread(new Runnable() {
      public void run() {
        PlaceManager.getInstance().stopMonitoring();
      }
    });
  }

  public boolean isMonitoring()
  {
    return PlaceManager.getInstance().isMonitoring();
  }
}
