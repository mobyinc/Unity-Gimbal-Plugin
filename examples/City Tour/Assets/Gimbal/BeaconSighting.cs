using UnityEngine;
using System.Collections;
using SimpleJSON;

public class BeaconSighting {
	public Beacon beacon;
	public int rssi;

	public BeaconSighting(JSONNode node) {
		Deserialize(node);
	}

	private void Deserialize(JSONNode node) {
		rssi = node["RSSI"].AsInt;
		beacon = new Beacon(node["beacon"]);
	}
}