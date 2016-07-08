using UnityEngine;
using System.Collections;

public class MoonRotateAround : MonoBehaviour {

    float rotationSpeed, moonAlpha, earthX, earthZ;
    PlanetariumSettings planetariumSettings;
    Transform earthTransform, moonTransform;

	// Use this for initialization
	void Start () {
	    planetariumSettings = GameObject.Find("PlanetariumSettings").GetComponent<PlanetariumSettings>();
        earthTransform = GameObject.Find("Earth").GetComponent<Transform>();
        moonTransform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        /*
         * Get the current position of the Earth and make the Moon rotate
         * around it according to the speed set in the settings
         */
        rotationSpeed = planetariumSettings.orbitalSpeed;
        earthX = earthTransform.position.x;
        earthZ = earthTransform.position.z;
        moonTransform.position = new Vector3(earthX - 1.9f * Mathf.Cos(moonAlpha), 0f, earthZ - 1.9f * Mathf.Sin(moonAlpha));
        moonAlpha += (2.0f / (29.530589f * 24.0f * 60.0f * 60.0f)) * rotationSpeed * Time.deltaTime;
    }
}
