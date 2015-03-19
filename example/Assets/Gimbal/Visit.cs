using UnityEngine;
using System.Collections;
using SimpleJSON;
using System;
using System.Globalization;

public class Visit {
	public Place place;
	public DateTime arrivalDate;
	public DateTime? departureDate;
	
	public Visit(JSONNode node) {
		Deserialize(node);
	}
	
	private void Deserialize(JSONNode node) {
		place = new Place(node["place"]);
		arrivalDate = arrivalDateConverter(node["arrivalDate"].Value);
	}

	private DateTime arrivalDateConverter(string date) {
		return DateTime.ParseExact(date, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);;
	}

	private void departureDateConverter(string date) {
		if (date.Equals("N/A")) {
			departureDate = null;
		} else {
			departureDate = arrivalDateConverter(date);
		}
	}                                        
}
