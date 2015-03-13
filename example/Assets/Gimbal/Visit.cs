using UnityEngine;
using System.Collections;
using SimpleJSON;

public class Visit {
	public Place place;
	public string arrivalDate;
	public string departureDate;


	public Visit(JSONNode node) {
		Deserialize(node);
	}
	
	private void Deserialize(JSONNode node) {
		place = new Place(node["place"]);
		arrivalDate = node["arrivalDate"].Value;
		departureDate = node["departureDate"].Value;
	}
}
