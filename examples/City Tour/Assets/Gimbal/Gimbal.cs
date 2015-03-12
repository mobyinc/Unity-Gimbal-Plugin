using UnityEngine;
using System.Runtime.InteropServices;

public class Gimbal {
	public static void SetApiKey(string apiKey) {
		Debug.Log("unity set api key: " + apiKey);
		setApiKey(apiKey);
	}

	public static void StartBeaconManager() {
		Debug.Log("unity started listening");
		startBeaconManager();
	}

	public static void StopBeaconManager() {
		Debug.Log("unity stopped listening");
		stopBeaconManager();
	}

	public static void StartPlaceManager() {
		Debug.Log("unity started listening for places");
		startPlaceManager();
	}

	public static void StopPlaceManager() {
		Debug.Log("unity stopped listening for places");
		stopPlaceManager();
	}

	[DllImport ("__Internal")]
	private static extern void setApiKey(string apiKey);

	[DllImport ("__Internal")]
	private static extern void startBeaconManager();

	[DllImport ("__Internal")]
	private static extern void stopBeaconManager();

	[DllImport ("__Internal")]
	private static extern void startPlaceManager();

	[DllImport ("__Internal")]
	private static extern void stopPlaceManager();
}
