using UnityEngine;
using System.Runtime.InteropServices;


public class Gimbal {

	public static void SetApiKey(string apiKey) {
		Debug.Log("unity set api key: " + apiKey);
		setApiKey(apiKey);
	}

	[DllImport ("__Internal")]
	private static extern void setApiKey(string apiKey);
}
