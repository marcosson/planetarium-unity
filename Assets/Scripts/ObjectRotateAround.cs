using UnityEngine;
using System;
using System.Collections;
using Assets.Scripts;

public class ObjectRotateAround : MonoBehaviour {

    string planetName;
    PlanetariumSettings planetariumSettings;
    Transform planetTransform;
    float rotationSpeed, planetOrbitalSpeed, planetAngularSpeed, planetEccentricity, normalizedSemiMajorAxis,
        normalizedSemiMinorAxis, normalizedCenter;
    float planetAlpha = 0.0f;
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
        planetOrbitalSpeed = planet.GetOrbitalPeriod() * 24.0f * 60.0f * 60.0f;
        planetAngularSpeed = (2.0f / planetOrbitalSpeed);
        planetEccentricity = planet.GetEccentricity();
        normalizedSemiMajorAxis = planet.GetNormalizedSemiMajorAxis();
        normalizedSemiMinorAxis = normalizedSemiMajorAxis * Mathf.Sqrt(1 - Mathf.Pow(planetEccentricity, 2));
        normalizedCenter = normalizedSemiMinorAxis - normalizedSemiMajorAxis;
        planetAlpha = planet.GetAlphaStart();
    }

    // Update is called once per frame
    void Update () {
        /* 
         * Make the planet rotate around the Sun according to its data
         */
        rotationSpeed = planetariumSettings.orbitalSpeed;
        planetTransform.position = new Vector3(normalizedCenter - normalizedSemiMajorAxis * Mathf.Cos(planetAlpha), 0f, normalizedSemiMinorAxis * Mathf.Sin(planetAlpha));
        planetAlpha += planetAngularSpeed * rotationSpeed * Time.deltaTime;
    }
}
