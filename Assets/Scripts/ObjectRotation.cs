using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class ObjectRotation : MonoBehaviour {
    
    string planetName;
    float rotationSpeed, planetSpeed;
    PlanetariumSettings planetariumSettings;
    Transform planetTransform;
    IPlanet planet;

	// Use this for initialization
	void Start () {
        planetariumSettings = GameObject.Find("PlanetariumSettings").GetComponent<PlanetariumSettings>();
        planetName = GetComponent<Transform>().name;
        /*
         * Create an object by passing the name of the GameObject
         * This new object will be one of the nine planets and will have its
         * own data
         */
        planetTransform = GameObject.Find(planetName).GetComponent<Transform>();
        planet = PlanetFactory.GetPlanet(planetName);
	}
	
	// Update is called once per frame
	void Update ()
    {
        /* 
         * Make the planet rotate around its axis according to its data
         */
        rotationSpeed = planetariumSettings.rotationSpeed;
        planetSpeed = (planet.GetRotationPeriod() * 24.0f) / 3600.0f;
        planetTransform.Rotate(Vector3.up * planetSpeed * rotationSpeed * Time.deltaTime, Space.World);
    }
}
