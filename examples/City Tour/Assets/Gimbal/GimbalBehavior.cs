using UnityEngine;
using System.Collections;

public class GimbalBehavior : MonoBehaviour {

	public string apiKey;

	void Start () {
		if (gameObject.name != "GimbalPlugin") {
			Debug.LogWarning("Gimbal callbacks will not be called unless the GimbalBehavior is attached to a game object named GimbalPlugin.");
		}

		Gimbal.SetApiKey(apiKey);	
	}

	void OnFoo(string message) {
		Debug.Log(message);
	}
}
