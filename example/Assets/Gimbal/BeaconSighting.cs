using UnityEngine;
using System;
using System.Collections;
using SimpleJSON;

public class BeaconSighting {
	public Beacon beacon;
	public int rssi;
	public DateTime date;

	public BeaconSighting(JSONNode node) {
		Deserialize(node);
	}

	private void Deserialize(JSONNode node) {
		rssi = node["RSSI"].AsInt;
		beacon = new Beacon(node["beacon"]);
		date = Gimbal.ConvertJsonDate(node["date"].Value);
	}
}