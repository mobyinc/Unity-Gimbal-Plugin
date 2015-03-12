using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GimbalBehavior : MonoBehaviour {
	public string apiKey;
	public bool autoStartBeaconManager = true;

	private bool beaconListening;
	private bool placeListening;

	public delegate void BeaconSightingHandler(BeaconSighting sighting);
	public event BeaconSightingHandler BeaconSighted = delegate {};

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
		Gimbal.SetApiKey(apiKey);
	}
	
	void OnBeaconSighting(string message) {
		JSONNode node = JSON.Parse(message);
		BeaconSighting beaconSighting = new BeaconSighting(node);
		BeaconSighted(beaconSighting);
	}

	public void ToggleBeaconListening() {
		if (beaconListening) {
			StopBeaconManager();
		} else {
			StartBeaconManager();
		}
	}

	public void StartBeaconManager() {
		Gimbal.StartBeaconManager();
		beaconListening = true;
	}

	public void StopBeaconManager() {
		Gimbal.StopBeaconManager();
		beaconListening = false;
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
