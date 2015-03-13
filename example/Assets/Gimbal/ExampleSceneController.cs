using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExampleSceneController : MonoBehaviour {
	public GimbalBehavior gimbalBehavior;
	public Text beaconStatusText;
	public Text placeStatusText;
	public Text beaconsFoundText;
	public Button togglePlacesManagerButton;
	public Button toggleBeaconManagerButton;

	private Dictionary<string, BeaconSighting> sightings = new Dictionary<string, BeaconSighting>();

	void Start () {
		gimbalBehavior.BeaconSighted += new GimbalBehavior.BeaconSightingHandler(BeaconSightingFound);

		placeStatusText.text = "Place Sighting: " + updateStatusText(gimbalBehavior.autoStartPlaceManager);
		beaconStatusText.text = "Beacon Sighting: " + updateStatusText(gimbalBehavior.autoStartBeaconManager);
	}

	void Update () {
		beaconsFoundText.text = "";
		foreach (BeaconSighting sighting in sightings.Values) {
			beaconsFoundText.text += "\nName: " + sighting.beacon.name + "\t Identifier: " + sighting.beacon.identifier + "\t RSSI: " + sighting.rssi;
		}
	}

	private void BeaconSightingFound(BeaconSighting sighting) {
		if (!sightings.ContainsKey(sighting.beacon.identifier)) {
			sightings.Add(sighting.beacon.identifier, sighting);
			beaconsFoundText.text += "\nName: " + sighting.beacon.name + "\t Identifier: " + sighting.beacon.identifier + "\t RSSI: " + sighting.rssi;
		} else {
			sightings[sighting.beacon.identifier] = sighting;
		}
	}
	
	public void TogglePlacesManager() {
		gimbalBehavior.TogglePlaceListening();
		placeStatusText.text = "Place Sighting: " + updateStatusText(gimbalBehavior.IsListeningForPlaces);
	}

	public void ToggleBeaconManager() {
		gimbalBehavior.ToggleBeaconListening();
		beaconStatusText.text = "Beacon Sighting: " + updateStatusText(gimbalBehavior.IsListeningForBeacons);
	}

	private string updateStatusText(bool sightingStatus) {
		if (sightingStatus) {
			return "On";
		} else {
			return "Off";
		}
	}
}
