# Unity Gimbal Plugin

The Unity Gimbal Plugin makes it easy to include Gimbal events in your Unity projects for iOS and Android.

### Requirements

* Unity 5.3 (untested on previous versions)
* Android Version 4.4.3 or higher
* iOS
    * Using Xcode 7.3 or higher
    * Targeting iOS 8.0 or higher
    * Using iOS device with Bluetooth 4.0

### Getting Started

1. Create a new Unity Project (or open an existing project)
* Within Unity, switch to iOS or Android platform
* Open and import UnityGimbal.unityPackage into your Unity project
* Open the example scene /gimbalsdk/Gimbal/Example.unity
* Set the example scene as the launch scene (build settings)
* Select the GimbalPlugin object
* If you haven't already, sign up for a developer account with Gimbal and register your becaons and your add bundle id. See: [Gimbal Developers](https://manager.gimbal.com/login/users/sign_up)
* Paste your iOS and/or Android Api Keys from the Gimbal dashboard into the corresponding fields on the GimbalPlugin behavior
* Ensure the bundle id of your project matches the bundle id set in the Gimbal dashboard for the Api Key
* Optionally set Beacon Manager and/or Place Manager to start automatically when the scene starts
* Build and run the project **on a device** (the beacons will not work in the simulator)
* Toggle the managers on if needed
* Beacon and place info will be logged in the UI of the example scene

### Adding Gimbal Event to Your Own Classes

The GimbalBehavior class publishes the following events:

```
public event BeaconSightingHandler BeaconSighted
public event BeginVisitHandler BeginVisit
public event EndVisitHandler EndVisit
```

You can easily add and remove event handlers using standard C# techniques. There are two easy eays to get a reference to the GimbalBehavior class: 

- create a public property and then drag the GimbalPlugin object onto it
- use GetComponent< GimbalBehavior >()

```
public GimbalBehavior gimbalBehavior;

void Start() {
    gimbalBehavior.BeaconSighted += new GimbalBehavior.BeaconSightingHandler(BeaconSightingFound);
}

...

private void BeaconSightingFound(BeaconSighting sighting) {
    // do things in response to beacon sightings...
}
```

### Working with the Gimbal Models

Each of the following models mirrors the fields provided by the native Gimbal SDK.

- BeaconSighting
- Beacon
- Visit
- Place

You can read more about the Gimbal SDK on the [Gimbal Developer Site](http://gimbal.com/doc/).



