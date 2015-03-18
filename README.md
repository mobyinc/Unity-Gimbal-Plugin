# Unity Gimbal Plugin

The Unity Gimbal Plugin helps provide a way to see Gimbal Beacon Sightings and Visits in Unity Projects deployed as native Android or iOS applications.

### Requirements

* Unity 4.6.3f1 (untested on previous versions)
* Android
    * Version 4.4.3 or higher
    * Version of Android equal to API 21
* iOS
    * Using Xcode 4.4 or higher
    * Targeting iOS 5.1.1 or higher
    * Using iOS device with Bluetooth 4.0

### Import package into Unity

* Open and import the Unity Gimbal Plugin package into your Unity project.

### iOS Specific Usage Requirements

* Login to Gimbal developer profile and download Gimbal.framework
* Place Gimbal.framework in your projects main folder.

## API

Create a reference to the GimbalBehavoir class in your scene.

    public GimbalBehavior gimbalBehavior;

### Setting Up Beacon Sighting Events
    
    void Start() {
        //Create event listener for BeaconSightings
        gimbalBehavior.BeaconSighted += new GimbalBehavior.BeaconSightingHandler(BeaconSightingFound);
    }
    
    ...

    //Create function that will handle BeaconSighting events
    private void BeaconSightingFound(BeaconSighting sighting) {
        
    }

### Beacon Sighting Properties

#### rssi

Type: int

The proximity to the beacon sighting.

    private void BeaconSightingFound(BeaconSighting sighting) {
        int RSSI = sighting.rssi;
    }

#### beacon

Type: Beacon

The beacon associated with the beacon sighting.

    private void BeaconSightingFound(BeaconSighting sighting) {
        Beacon beacon = sighting.beacon;
    }

### Beacon Properties

#### batteryLevel

Type: int

The battery level of the beacon.

    private void BeaconSightingFound(BeaconSighting sighting) {
        int batteryLevel = sighting.beacon.batteryLevel;
    }

#### iconURL

Type: string

The icon url set for the beacon.

    private void BeaconSightingFound(BeaconSighting sighting) {
        string iconURL = sighting.beacon.iconURL;
    }

#### identifier

Type: string

A unique string identifier for the beacon.

    private void BeaconSightingFound(BeaconSighting sighting) {
        string identifier = sighting.beacon.identifier;
    }

#### name

Type: string

The name of the beacon.

    private void BeaconSightingFound(BeaconSighting sighting) {
        string name = sighting.beacon.name;
    }

#### temperature

Type: int

The temperature detected by the beacon.

    private void BeaconSightingFound(BeaconSighting sighting) {
        int temperature = sighting.beacon.temperature;
    }

### Setting up Visit events

    void Start() {
        //Create event listeners for begining and ending visits
        gimbalBehavior.BeginVisit += new GimbalBehavior.BeginVisitHandler(StartedPlaceVisit);
        gimbalBehavior.EndVisit += new GimbalBehavior.EndVisitHandler(EndedPlaceVisit);
    }
    
    ...
    
    //Create functions that will handle Visit events
    private void StartedPlaceVisit(Visit visit) {
        
    }

    private void EndedPlaceVisit(Visit visit) {

    }

### Visit Properties

#### place

Type: Place

The place associated with the visit

    private void StartedPlaceVisit(Visit visit) {
        Place place = visit.place;
    }

#### arrivalDate

Type: string

The arrival date associated with the visit.

    private void StartedPlaceVisit(Visit visit) {
        string arrivalDate = visit.arrivalDate;
    }

#### departureDate

Type: string

The departure date associated with the visit

    private void StartedPlaceVisit(Visit visit) {
        string departureDate = visit.departureDate;
    }

### Place Properties

#### identifier

Type: string

A unique string identifier for the place.

    private void StartedPlaceVisit(Visit visit) {
        string identifier = visit.place.identifier;
    }

#### name

Type: string

The name of the place.

    private void StartedPlaceVisit(Visit visit) {
        string name = visit.place.name;
    }



