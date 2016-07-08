using UnityEngine;
using System.Collections;

public class SunRotate : MonoBehaviour {

    private PlanetariumSettings planetariumSettings;
    private Transform sunTransform;
    private float rotationSpeed, sunSpeed;

    // Use this for initialization
    void Start()
    {
        planetariumSettings = GameObject.Find("PlanetariumSettings").GetComponent<PlanetariumSettings>();
        sunTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Make the Sun rotate according to its parameters
         * and the speed set in the settings
         */
        rotationSpeed = planetariumSettings.rotationSpeed;
        sunSpeed = (25.05f * 24.0f) / 3600.0f;
        sunTransform.Rotate(Vector3.up * sunSpeed * rotationSpeed * Time.deltaTime, Space.World);
    }
}
