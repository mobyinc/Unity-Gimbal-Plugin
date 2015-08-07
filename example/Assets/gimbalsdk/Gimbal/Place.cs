using UnityEngine;
using System.Collections;
using SimpleJSON;

public class Place {
	public string identifier;
	public string name;
	
	public Place(JSONNode node) {
		Deserialize(node);
	}
	
	private void Deserialize(JSONNode node) {
		identifier = node["identifier"].Value;
		name = node["name"].Value; 
	}
}
