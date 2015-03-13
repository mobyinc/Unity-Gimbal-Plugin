using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GimbalBehavior : MonoBehaviour {
	public string iosApiKey;
	public string androidApiKey;
	public bool autoStartBeaconManager = true;
	public bool autoStartPlaceManager = true;

	public delegate void BeaconSightingHandler(BeaconSighting sighting);
	public event BeaconSightingHandler BeaconSighted = delegate {};

	public delegate void BeginVisitHandler(Visit visit);
	public event BeginVisitHandler BeginVisit = delegate {};

	public delegate void EndVisitHandler(Visit visit);
	public event EndVisitHandler EndVisit = delegate {};

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

		if (autoStartPlaceManager) {
			StartPlaceManager();
		}

		isListeningForPlaces = autoStartPlaceManager;
		isListeningForBeacons = autoStartBeaconManager;
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

	void OnBeginVisit(string message) {
		JSONNode node = JSON.Parse(message);
		Visit visit = new Visit(node);
		BeginVisit(visit);
	}

	void OnEndVisit(string message) {
		JSONNode node = JSON.Parse(message);
		Visit visit = new Visit(node);
		EndVisit(visit);
	}

	public void TogglePlaceListening() {
		if (isListeningForPlaces) {
			StartPlaceManager();
		} else {
			StopPlaceManager();
		}
	}

	public void StartPlaceManager() {
		Gimbal.StartPlaceManager();
		isListeningForPlaces = false;
	}

	public void StopPlaceManager() {
		Gimbal.StopPlaceManager();
		isListeningForPlaces = true;
	}

	public bool IsMonitoring() {
		return Gimbal.IsMoitoring();
	}

}
