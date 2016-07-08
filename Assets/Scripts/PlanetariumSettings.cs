using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlanetariumSettings : MonoBehaviour {

    public bool started = false;
    public float rotationSpeed = 1;
    public float orbitalSpeed = 1;
    private GameObject startScreen;
    private Slider rotationSlider, orbitalSlider;

	// Use this for initialization
	void Start ()
    {
        startScreen = GameObject.Find("StartScreen");
        rotationSlider = GameObject.Find("RotationSlider").GetComponent<Slider>();
        orbitalSlider = GameObject.Find("OrbitalSlider").GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*
         * Disable the start scene and unlock interactivity for
         * the rotation and orbital sliders
         */
        if (started)
        {
            startScreen.SetActive(false);
            /*
             * Update the sliders' value according to the pressed keys that
             * will increment or decrement the various speeds
             */
            if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                rotationSlider.value += 10;
            }
            if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                rotationSlider.value -= 10;
            }
            if ((Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.LeftAlt)) && Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                orbitalSlider.value += 10;
            }
            if ((Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.LeftAlt)) && Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                orbitalSlider.value -= 10;
            }
        }
    }

    /*
     * Update the rotation speed according to the slider's value
     */
    public void RotationSpeedChange(float newRotationSpeed)
    {
        rotationSpeed = newRotationSpeed;
    }

    /*
     * Update the orbital speed according to the slider's value
     */
    public void OrbitalSpeedChange(float newOrbitalSpeed)
    {
        orbitalSpeed = newOrbitalSpeed;
    }

    /*
     * Exit from the start screen and unlock the interactivity with
     * the other UI and the spaceship
     */
    public void StartScene()
    {
        started = true;
    }

    /*
     * Returns true if the user pressed the Start button, false
     * otherwise
     */
    public bool SceneStarted()
    {
        return started;
    }
}
