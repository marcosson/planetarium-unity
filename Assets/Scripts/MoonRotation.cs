using UnityEngine;
using System.Collections;

public class MoonRotation : MonoBehaviour {

    float rotationSpeed, moonSpeed;
    PlanetariumSettings planetariumSettings;
    Transform moonTransform;

	// Use this for initialization
	void Start () {
        planetariumSettings = GameObject.Find("PlanetariumSettings").GetComponent<PlanetariumSettings>();
        moonTransform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        /*
         * Make the moon rotate according to its parameters
         * and the speed set in the settings
         */
        rotationSpeed = planetariumSettings.rotationSpeed;
        moonSpeed = (27.321661f * 24.0f) / 3600.0f;
        moonTransform.Rotate(Vector3.up * moonSpeed * rotationSpeed * Time.deltaTime, Space.World);
    }
}
