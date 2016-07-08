using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DetectCollision : MonoBehaviour {

    private float distance;
    private bool played = false;
    private bool autopilotEnabled;
    private string[] planetsName = new string[] { "Sun", "Mercury", "Venus", "Earth", "Moon", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
    private float[] planetsRadius = new float[10];
    private float[] planetsAtmosphere = new float[10];
    private bool[] collisions = new bool[10];
    public AudioClip alarmAudioClip;
    public AudioSource alarmAudio;
    private Transform[] planetsTransform = new Transform[10];
    private Transform planetTransform, spaceshipTransform;
    private GameObject UI;
    private Canvas canvas;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < planetsName.Length; i++)
        {
            planetsTransform[i] = GameObject.Find(planetsName[i]).GetComponent<Transform>();
            planetsRadius[i] = GameObject.Find(planetsName[i]).GetComponent<Transform>().localScale.x / 2;
            planetsAtmosphere[i] = planetsRadius[i] * 1.5f;
            if (planetsAtmosphere[i] > 5)
            {
                planetsAtmosphere[i] = 5.0f;
            }
        }
        spaceshipTransform = GameObject.Find("Arcadia").GetComponent<Transform>();
        UI = GameObject.Find("CollisionWarning");
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*
         * Scan all the distances between the spaceship and the planets and
         * if the spaceship is near enough to one of the planets send a 
         * warning
         */
        for (int i = 0; i < planetsName.Length; i++)
        {
            if ((Mathf.Abs(planetsRadius[i] - Vector3.Distance(planetsTransform[i].position, spaceshipTransform.position))) < planetsAtmosphere[i])
            {
                collisions[i] = true;
            } else
            {
                collisions[i] = false;
            }
        }
        /*
         * If the spaceship is in the atmosphere of a planet, play a sound that
         * will warn the user of the imminent collision and stop immediately if
         * the spaceship is no more in a dangerous position
         * The sound is played once (in loop) to prevent the overlap of multiple
         * instances of the same sound played at once.
         */
        if (CheckCollision(collisions)) {
            if (!played)
            {
                alarmAudio.PlayOneShot(alarmAudioClip);
                played = true;
            }
        }
        else
        {
            alarmAudio.Stop();
            played = false;
        }
        /*
         * If the autopilot is enabled, disable the warnings that may occur
         * when the spaceship is traveling and reactivate them as soon as the
         * autopilot is disabled
         */
        autopilotEnabled = GameObject.Find("EnableAutopilot").GetComponent<Toggle>().isOn;
        UI.SetActive(CheckCollision(collisions) && !autopilotEnabled);
	}

    /*
     * Method called when the spaceship collides with a
     * planet's surface and takes the spaceship back to the
     * starting point
     */
    void OnCollisionEnter(Collision col)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /*
     * Method that checks the collisions array and will return true if there is
     * at least a collision, that is the spaceship is in a certain planet's
     * atmosphere
     */
    public bool CheckCollision(bool[] collisions)
    {
        for (int i = 0; i < collisions.Length; i++)
        {
            if(collisions[i])
            {
                return true;
            }
        }
        return false;
    }
}
