using UnityEngine;
using System.Linq;
using System.Collections;
using Assets.Scripts;
using UnityEngine.UI;

public class NearestPlanet : MonoBehaviour {

    bool autopilotEnabled = false;
    string[] planetsName = new string[] { "Sun", "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
    float[] planetsRadius = new float[9];
    float[] distances = new float[9];
    float[] planetsAtmosphere = new float[9];
    Transform spaceshipTransform, planetTransform;
    Transform[] planetsTransform = new Transform[9];
    IPlanet planet;
    Text textPlanet, textMass, textDiameter, textGravity, textMeanTemperature, textNMoons, textOrbital, textRotation;
    GameObject infoPanel;

    // Use this for initialization
    void Start () {
        spaceshipTransform = GetComponent<Transform>();
        textPlanet = GameObject.Find("PlanetName").GetComponent<Text>();
        textMass = GameObject.Find("MassInfo").GetComponent<Text>();
        textDiameter = GameObject.Find("DiameterInfo").GetComponent<Text>();
        textGravity = GameObject.Find("GravityInfo").GetComponent<Text>();
        textMeanTemperature = GameObject.Find("MeanTemperatureInfo").GetComponent<Text>();
        textNMoons = GameObject.Find("NumberOfMoonsInfo").GetComponent<Text>();
        textOrbital = GameObject.Find("OrbitalPeriodInfo").GetComponent<Text>();
        textRotation = GameObject.Find("RotationPeriodInfo").GetComponent<Text>();
        infoPanel = GameObject.Find("InfoPanel");
        infoPanel.SetActive(false);
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
    }
	
	// Update is called once per frame
	void Update () {
        /*
         * Scan all the distances between the spaceship and the planets and
         * if the spaceship is near enough to one of the planets, show the
         * information related to it
         */
        for (int i = 0; i < planetsName.Length; i++)
        {
            distances[i] = Vector3.Distance(planetsTransform[i].position, spaceshipTransform.position) - planetsRadius[i];
            if (distances[i] < planetsAtmosphere[i] + 1.1f)
            {
                textPlanet.text = "<color=\"white\">" + planetsName[i] + "</color>";
                if (planetsName[i] != "Sun")
                {
                    planet = PlanetFactory.GetPlanet(planetsName[i]);
                    textMass.text = "<color=\"white\">Mass: " + planet.GetMass() + "*10^24 kg </color>";
                    textDiameter.text = "<color=\"white\">Diameter: " + planet.GetDiameter() + " km </color>";
                    textGravity.text = "<color=\"white\">Gravity: " + planet.GetGravity() + " m/s^2</color>";
                    textMeanTemperature.text = "<color=\"white\">Mean temperature: " + planet.GetTemperature() + " °C</color>";
                    textNMoons.text = "<color=\"white\">Number of moons: " + planet.GetNumberOfMoons() + "</color>";
                    textOrbital.text = "<color=\"white\">Orbital period: " + planet.GetOrbitalPeriod() + " hours</color>";
                    if (planet.GetRotationPeriod() < 0)
                    {
                        textRotation.text = "<color=\"white\">Rotation period: <size=9>" + planet.GetRotationPeriod() + " days (retrograde)</size></color>";
                    }
                    else
                    {
                        textRotation.text = "<color=\"white\">Rotation period: " + planet.GetRotationPeriod() + " days</color>";
                    }
                } else
                {
                    textMass.text = "<color=\"white\">Mass: 1.98855*10^30 kg</color>";
                    textDiameter.text = "<color=\"white\">Diameter: 1392520 km</color>";
                    textGravity.text = "<color=\"white\">Gravity: 240 m/s^2</color>";
                    textMeanTemperature.text = "<color=\"white\">Mean temperature: 5500 °C</color>";
                    textNMoons.text = "<color=\"white\">Number of moons: 0</color>";
                    textOrbital.text = "<color=\"white\">Orbital period: 609.12 hours</color>";
                    textRotation.text = "<color=\"white\">Rotation period: -</color>";
                }
            }
        }
        /* 
         * Disable the info panel as long as the autopilot is enabled, so it
         * won't pop up when not needed
         */
        autopilotEnabled = GameObject.Find("EnableAutopilot").GetComponent<Toggle>().isOn;
        infoPanel.SetActive(checkDistance(distances) && !autopilotEnabled);
    }

    /*
     * Scan the distances' array and if there is at least one
     * distance between spaceship and a planet which is smaller 
     * than an edge, return true
     */
    bool checkDistance(float[] distances)
    {
        for (int i = 0; i < distances.Length; i++)
        {
            if (distances[i] < planetsAtmosphere[i] + 1.1f)
            {
                return true;
            }
        }
        return false;
    }
}
