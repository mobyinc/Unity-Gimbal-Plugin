using UnityEngine;
using System;
using System.Collections;
using SimpleJSON;
using System.Globalization;

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
		date = dateConverter(node["date"].Value);
	}

	private DateTime dateConverter(string date) {
		return DateTime.ParseExact(date, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
	}
}