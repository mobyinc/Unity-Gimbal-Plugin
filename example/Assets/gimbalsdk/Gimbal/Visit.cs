using UnityEngine;
using System.Collections;
using SimpleJSON;
using System;

public class Visit {
	public Place place;
	public DateTime arrivalDate;
	public DateTime? departureDate;
	
	public Visit(JSONNode node) {
		Deserialize(node);
	}
	
	private void Deserialize(JSONNode node) {
		place = new Place(node["place"]);
		arrivalDate = Gimbal.ConvertJsonDate(node["arrivalDate"].Value);
	}
}
