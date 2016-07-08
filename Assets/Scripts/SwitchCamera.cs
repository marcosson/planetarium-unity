using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchCamera : MonoBehaviour {

    private Camera firstPersonCamera, thirdPersonCamera;
    private PlanetariumSettings planetariumSettings;
    private Toggle firstCameraToggle;

    // Use this for initialization
    void Start () {
        firstCameraToggle = GameObject.Find("SwitchCamera").GetComponent<Toggle>();
        planetariumSettings = GameObject.Find("PlanetariumSettings").GetComponent<PlanetariumSettings>();
        firstPersonCamera = GameObject.Find("FirstPersonCamera").GetComponent<Camera>();
        thirdPersonCamera = GameObject.Find("ThirdPersonCamera").GetComponent<Camera>();
        thirdPersonCamera.depth = 1;
        firstPersonCamera.depth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Switch to the first person camera by pressing the F key
         */
        if (planetariumSettings.started)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                firstCameraToggle.isOn = true;
                firstPersonCamera.depth = 1;
                thirdPersonCamera.depth = 0;
            }
            /* 
             * Switch to the third person camera by pressing the T key
             */
            else if (Input.GetKeyDown(KeyCode.T))
            {
                firstCameraToggle.isOn = false;
                thirdPersonCamera.depth = 1;
                firstPersonCamera.depth = 0;
            }
            /*
             * Leave cameras' status as they are
             */
            else
            {
                if (firstPersonCamera.depth == 1)
                {
                    firstPersonCamera.depth = 1;
                    thirdPersonCamera.depth = 0;
                }
                else
                {
                    thirdPersonCamera.depth = 1;
                    firstPersonCamera.depth = 0;
                }
            }
        }
    }

    /*
     * Alternate between first person camera and third
     * person camera according to the toggle's status
     */
    public void SetCamera(bool select)
    {
        if (firstPersonCamera.depth == 1)
        {
            firstPersonCamera.depth = 0;
            thirdPersonCamera.depth = 1;
        }
        else
        {
            firstPersonCamera.depth = 1;
            thirdPersonCamera.depth = 0;
        }
    }
}
