using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExampleSceneController : MonoBehaviour {
	public GimbalBehavior gimbalBehavior;
	public Text beaconStatusText;
	public Button togglePlacesManagerButton;
	public Button toggleBeaconManagerButton;

	private Dictionary<string, BeaconSighting> sightings;

	void Start () {
		
	}

	void Update () {
	
	}

	void TogglePlacesManager() {
	}

	void ToggleBeaconManager() {
	}
}
