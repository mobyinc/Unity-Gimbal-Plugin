using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExampleSceneController : MonoBehaviour {
	public GimbalBehavior gimbalBehavior;
	public Text beaconStatusText;
	public Text placeStatusText;
	public Text beaconsFoundText;
	public Text placesVisitingText;
	public Text placesVisitedText;
	public Button togglePlacesManagerButton;
	public Button toggleBeaconManagerButton;

	private Dictionary<string, BeaconSighting> sightings = new Dictionary<string, BeaconSighting>();
	private Dictionary<string, Visit> currentVisits = new Dictionary<string, Visit>();
	private Dictionary<string, Visit> endedVisits = new Dictionary<string, Visit>();

	void Start () {
		gimbalBehavior.BeaconSighted += new GimbalBehavior.BeaconSightingHandler(BeaconSightingFound);
		gimbalBehavior.BeginVisit += new GimbalBehavior.BeginVisitHandler(StartedPlaceVisit);
		gimbalBehavior.EndVisit += new GimbalBehavior.EndVisitHandler(EndedPlaceVisit);

		placeStatusText.text = "Place Sighting: " + updateStatusText(gimbalBehavior.autoStartPlaceManager);
		beaconStatusText.text = "Beacon Sighting: " + updateStatusText(gimbalBehavior.autoStartBeaconManager);
	}

	void Update () {
		beaconsFoundText.text = "Beacons:";
		foreach (BeaconSighting sighting in sightings.Values) {
			beaconsFoundText.text += "\nName: " + sighting.beacon.name + "\t Identifier: " + sighting.beacon.identifier + "\t RSSI: " + sighting.rssi;
		}

		placesVisitingText.text = "Visiting:";
		updateVisitTextHelper(placesVisitingText, currentVisits);

		placesVisitedText.text = "Visited:";
		updateVisitTextHelper(placesVisitedText, endedVisits);

	}

	private void updateVisitTextHelper(Text visitText, Dictionary<string, Visit> visits) {
		foreach (Visit visit in visits.Values) {
			visitText.text += "\nPlace Name: " + visit.place.name + "\t Identifier: " + visit.place.identifier + 
				"\t Arrival Date: " + visit.arrivalDate + "\t Departure Date: " + visit.departureDate;
		}
	}
	
	private void BeaconSightingFound(BeaconSighting sighting) {
		if (!sightings.ContainsKey(sighting.beacon.identifier)) {
			sightings.Add(sighting.beacon.identifier, sighting);
		} else {
			sightings[sighting.beacon.identifier] = sighting;
		}
	}

	private void StartedPlaceVisit(Visit visit) {
		VisitHelper(visit, currentVisits, endedVisits);
	}

	private void EndedPlaceVisit(Visit visit) {
		VisitHelper(visit, endedVisits, currentVisits);
	}

	private void VisitHelper(Visit visit, Dictionary<string, Visit> addVisit, Dictionary<string, Visit> removeVisit) {
		if (!addVisit.ContainsKey(visit.place.identifier)) {
			addVisit.Add(visit.place.identifier, visit);
		} else {
			addVisit[visit.place.identifier] = visit;
		}

		if (removeVisit.ContainsKey(visit.place.identifier)) {
			removeVisit.Remove(visit.place.identifier);
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
