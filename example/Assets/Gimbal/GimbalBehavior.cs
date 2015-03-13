using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GimbalBehavior : MonoBehaviour {
	public string iosApiKey;
	public string androidApiKey;
	public bool autoStartBeaconManager = true;

	public delegate void BeaconSightingHandler(BeaconSighting sighting);
	public event BeaconSightingHandler BeaconSighted = delegate {};

	private bool isListeningForBeacons;
	private bool isListeningForPlaces;

	public bool IsListeningForBeacons {
		get { return isListeningForBeacons; }
	}

	public bool IsListeningForPlaces {
		get { return isListeningForPlaces; }
	}

	void Start() {
		if (gameObject.name != "GimbalPlugin") {
			Debug.LogWarning("Gimbal callbacks will not be called unless the GimbalBehavior is attached to a game object named GimbalPlugin.");
		}

		SetApiKey();

		if (autoStartBeaconManager) {
			StartBeaconManager();
		}
	}

	void SetApiKey() {
#if UNITY_IPHONE
		Gimbal.SetApiKey(iosApiKey);
#elif UNITY_ANDROID
		Gimbal.SetApiKey(androidApiKey);
#endif
	}
	
	void OnBeaconSighting(string message) {
		JSONNode node = JSON.Parse(message);
		BeaconSighting beaconSighting = new BeaconSighting(node);
		BeaconSighted(beaconSighting);
	}

	public void ToggleBeaconListening() {
		if (isListeningForBeacons) {
			StopBeaconManager();
		} else {
			StartBeaconManager();
		}
	}

	public void StartBeaconManager() {
		Gimbal.StartBeaconManager();
		isListeningForBeacons = true;
	}

	public void StopBeaconManager() {
		Gimbal.StopBeaconManager();
		isListeningForBeacons = false;
	}

	void OnBeginVisit() {
	}

	void OnEndVisit() {
	}

	public void TogglePlaceListening() {
	}

	public void StartPlaceManager() {
	}

	public void StopPlaceManager() {
	}

}
