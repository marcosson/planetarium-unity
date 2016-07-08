using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveSpaceship : MonoBehaviour
{
    private float speedRotation = 20f, speedTranslation = 0.02f, acceleration = 5f;
    private float horizontalMouse, verticalMouse, horizontal, vertical, up, down, speedTranslationSet;
    private bool enableAutopilot;
    private bool lockCamera = false;
    private Toggle lockCameraToggle;
    private PlanetariumSettings planetariumSettings;
    private Transform spaceshipTransform, firstPersonCameraTransform, thirdPersonCameraTransform;
    private Rigidbody spaceshipRigidbody;
    private Camera thirdPersonCamera;
    private Vector3 upVector = new Vector3();
    private Vector3 downVector = new Vector3();
    private Vector3 verticalVector = new Vector3();

    // Use this for initialization
    void Start()
    {
        spaceshipTransform = GetComponent<Transform>();
        spaceshipRigidbody = GetComponent<Rigidbody>();
        thirdPersonCamera = GameObject.Find("ThirdPersonCamera").GetComponent<Camera>();
        firstPersonCameraTransform = GameObject.Find("FirstPersonCamera").GetComponent<Transform>();
        thirdPersonCameraTransform = GameObject.Find("ThirdPersonCamera").GetComponent<Transform>();
        planetariumSettings = GameObject.Find("PlanetariumSettings").GetComponent<PlanetariumSettings>();
        lockCameraToggle = GameObject.Find("LockCamera").GetComponent<Toggle>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
         * Lock the interactivity until the start button in the initial
         * scene is pressed
         */
        if (planetariumSettings.SceneStarted())
        {
            /*
             * Pressing the L key will lock or unlock the camera and
             * update the toggle status
             */
            if (Input.GetKeyUp(KeyCode.L))
            {
                lockCamera = !lockCamera;
                lockCameraToggle.isOn = !lockCameraToggle.isOn;
            }
            /*
             * If the camera is unlocked the spaceship can't move but it allows 
             * the user to look around
             */
            if (lockCamera)
            {
                lockCameraToggle.isOn = false;
                horizontalMouse = Input.GetAxis("Mouse X") * 10 * Time.fixedDeltaTime;
                verticalMouse = Input.GetAxis("Mouse Y") * 10 * Time.fixedDeltaTime;
                if (thirdPersonCamera.depth == 1)
                {
                    thirdPersonCameraTransform.Rotate(verticalMouse, horizontalMouse, 0, Space.World);
                }
                else
                {
                    firstPersonCameraTransform.Rotate(verticalMouse, horizontalMouse, 0, Space.World);
                }
            }
            else
            {
                firstPersonCameraTransform.rotation = new Quaternion(0, 0, 0, 0);
                thirdPersonCameraTransform.rotation = new Quaternion(0, 0, 0, 0);
            }
            /*
             * Check whether the spaceship is running in autopilot mode or it
             * can be moved freely
             */
            enableAutopilot = GameObject.Find("EnableAutopilot").GetComponent<Toggle>().isOn;
            if (!enableAutopilot)
            {
                /*
                 * If the camera is locked, the spaceship can move but moving the
                 * mouse will have no effect
                 */
                if (!lockCamera)
                {
                    lockCameraToggle.isOn = true;
                    firstPersonCameraTransform.rotation = new Quaternion(0, 0, 0, 0);
                    thirdPersonCameraTransform.rotation = new Quaternion(0, 0, 0, 0);
                    vertical = Input.GetAxis("Vertical") * speedTranslation;
                    verticalVector = spaceshipTransform.forward * vertical;
                    horizontal = Input.GetAxis("Horizontal");
                    /*
                     * Pressing the E key will make the spaceship go up until 
                     * the key is pressed
                     */
                    if (Input.GetKey(KeyCode.E))
                    {
                        up = speedTranslation;
                    }
                    else
                    {
                        up = 0.0f;
                    }
                    /*
                     * Pressing the Q key will make the spaceship down up until 
                     * the key is pressed
                     */
                    if (Input.GetKey(KeyCode.Q))
                    {
                        down = -speedTranslation;
                    }
                    else
                    {
                        down = 0.0f;
                    }
                    /*
                    * Pressing the Left Shift key will make the spaceship accellerate
                    * until the key is pressed
                    */
                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    {
                        verticalVector = verticalVector * acceleration;
                    }
                    else
                    {
                        verticalVector = verticalVector * 1.0f;
                    }
                    upVector = spaceshipTransform.up * up;
                    downVector = spaceshipTransform.up * down;
                    Quaternion deltaRotation = Quaternion.Euler(Vector3.up * horizontal * speedRotation * Time.fixedDeltaTime);
                    spaceshipRigidbody.MoveRotation(spaceshipRigidbody.rotation * deltaRotation);
                    spaceshipRigidbody.MovePosition(spaceshipRigidbody.position + verticalVector + upVector + downVector);
                }
            }
        }
    }

    /*
     * Alternate between locked and free camera according
     * to the toggle's status
     */
    public void LockCamera(bool locked)
    {
        if (lockCameraToggle.isOn && lockCamera)
        {
            lockCamera = false;
        }
        if (!lockCameraToggle.isOn && !lockCamera)
        {
            lockCamera = true;
        }
    }
}
