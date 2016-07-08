using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Autopilot : MonoBehaviour
{
    private float distance, alpha, deltaTangent;
    private float fixedDistance = 0f;
    private float rotationSpeed = 50f;
    private int indexPlanet, indexCollidingPlanet;
    private bool rotating = true;
    private bool justCollided = false;
    private bool planetSelected = false;
    private bool gotDirection = false;
    private bool rotateRight;
    public bool autopilotEnabled;
    private string selectedPlanet;
    private float[] planetsAtmosphere = new float[9];
    private float[] planetsRadius = new float[9];
    private bool[] collisions = new bool[9];
    private string[] planetsName = new string[] { "Sun", "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
    private Transform spaceshipTransform, planetTransform;
    private Rigidbody spaceshipRigidbody;
    private Transform[] planetsTransform = new Transform[9];
    private Toggle enableAutopilot;
    private Vector3 target;
    private Dropdown dropdownSelectPlanet;

    // Use this for initialization
    void Start()
    {
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
        spaceshipRigidbody = GameObject.Find("Arcadia").GetComponent<Rigidbody>();
        spaceshipTransform = GameObject.Find("Arcadia").GetComponent<Transform>();
        enableAutopilot = GameObject.Find("EnableAutopilot").GetComponent<Toggle>();
        dropdownSelectPlanet = GameObject.Find("SelectCelestialObject").GetComponent<Dropdown>();
        autopilotEnabled = false;
        dropdownSelectPlanet.interactable = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
         * Autopilot mode will start only after being enabled and selecting a planet
         */
        if (autopilotEnabled && planetSelected)
        {
            /*
             * The spaceship will translate up or down in order to be on the same level
             * of the planets' center
             */
            if (spaceshipRigidbody.position.y > 0.5 || spaceshipRigidbody.position.y < -0.5)
            {
                ResetYAxis();
            }
            else
            {
                /*
                 * The spaceship will rotate in place aligning with the destination planet
                 */
                if (rotating)
                {
                    planetTransform = GameObject.Find(selectedPlanet).GetComponent<Transform>();
                    RotateTowardsPlanet(planetTransform);
                }
                /*
                 * In case the spaceship is close enough to a planet different from the destination,
                 * it will rotate to a fixed angle and move forward to a fixed distance trying to get
                 * away from the planet it almost collided with
                 */
                if (justCollided)
                {
                    /* Check to see if the spaceship moved sufficiently far away
                     * If not, the spaceship will still have to travel
                     */
                    if (fixedDistance < 75)
                    {
                        /*
                         * Update the status of the collisions 
                         */
                        AddCollision();
                        /* 
                         * Check to see if there's enough space for the spaceship to move
                         * If not, the spaceship will have to rotate before moving forward
                         */
                        if (CheckCollision(collisions))
                        {
                            /*
                             * Get the planet the spaceship is close to
                             */
                            for (int i = 0; i < collisions.Length; i++)
                            {
                                if (collisions[i])
                                {
                                    indexCollidingPlanet = i;
                                }
                            }
                            /*
                             * Calculate the angle of the line tangent to the planet
                             */
                            deltaTangent = GetDelta(indexCollidingPlanet);
                            /* 
                             * If the spaceship's rotation is not tangent with the planet, it
                             * will rotate, otherwise it will start moving by a fixed distance
                             */
                            if (deltaTangent > 0)
                            {
                                RotateAfterCollision();
                            }
                            else
                            {
                                MoveFixedDistance();
                            }
                        }
                        else
                        {
                            MoveFixedDistance();
                        }
                    }
                    else
                    {
                        /*
                         * Reset the distance travelled and signal that the spaceship should
                         * be in a safe zone
                         */
                        fixedDistance = 0f;
                        justCollided = false;
                    }
                }
                /*
                 * The spaceship is aligned with the planet and there are no obstacles around
                 * meaning that it can move freely towards the planet
                 */
                if (!rotating && !justCollided)
                {
                    AddCollision();
                    /* 
                     * If the spaceship is going to collide with a planet, check what planet it's
                     * going to collide with. If it's the destination, the autopilot did its job
                     * and turns off, otherwise the spaceship will have to bypass the planet
                     */
                    if (CheckCollision(collisions))
                    {
                        /*
                         * Scan the collisions array and check if the planet the spaceship collided
                         * with it's the destination. If it is, the autopilot will turn off
                         */
                        for (int i = 0; i < collisions.Length; i++)
                        {
                            if (collisions[i])
                            {
                                if (planetsName[i] == selectedPlanet)
                                {
                                    autopilotEnabled = false;
                                    planetSelected = false;
                                    enableAutopilot.isOn = false;
                                    justCollided = true;
                                    rotating = true;
                                    gotDirection = false;
                                }
                            }
                        }
                        /* 
                         * As before, the spaceship will rotate until it's tangent to the planet
                         * and then it will move forward away from the planet it's traying to bypass
                         */
                        deltaTangent = GetDelta(Array.IndexOf(planetsName, selectedPlanet));
                        if (deltaTangent > 0)
                        {
                            RotateAfterCollision();
                        }
                        else
                        {
                            justCollided = true;
                        }
                    }
                    else
                    {
                        /*
                         * If the spaceship it's no more aligned with the destination, it will re-align
                         */
                        if (Vector3.Angle(target, spaceshipTransform.forward) > 0.5)
                        {
                            rotating = true;
                        }
                        AddCollision();
                        /*
                         * Calculate the distance between the spaceship and the destination planet
                         * If there's still a distance to travel, the spaceship will move forward
                         * checking if it's going to collide with a planet, in which case it will 
                         * try to realign and eventually bypass it
                         */
                        distance = Vector3.Distance(spaceshipTransform.position, planetTransform.position);
                        target = planetTransform.position - spaceshipTransform.position;
                        if (distance > 0)
                        {
                            spaceshipRigidbody.MovePosition(spaceshipRigidbody.position + target.normalized * 0.1f);
                            if (CheckCollision(collisions))
                            {
                                rotating = true;
                            }
                        }
                    }
                }
            }
        }
    }

    /*
     * Method to signal a collision that it's happening between a planet and
     * the spaceship. "Collision" means that the spaceship entered in that
     * planet's atmosphere and continuing to move forward will result in a crash
     * against the planet's surface
     */
    public void AddCollision()
    {
        for (int i = 0; i < planetsName.Length; i++)
        {
            if ((Mathf.Abs(planetsRadius[i] - Vector3.Distance(planetsTransform[i].position, spaceshipTransform.position))) < planetsAtmosphere[i] + 1.0f)
            {
                collisions[i] = true;
            }
            else
            {
                collisions[i] = false;
            }
        }
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
            if (collisions[i])
            {
                return true;
            }
        }
        return false;
    }

    /*
     * Method to move the space forward and keep track of how much distance it
     * travelled
     */
    public void MoveFixedDistance()
    {
        spaceshipRigidbody.MovePosition(spaceshipRigidbody.position + spaceshipTransform.forward * 0.1f);
        fixedDistance += spaceshipRigidbody.velocity.magnitude * Time.fixedDeltaTime;
    }

    /*
     * Method to reset the Y-axis of the spaceship, that is making the spaceship
     * travel at the same height of the planets' center
     */
     public void ResetYAxis()
    {
        if (spaceshipRigidbody.position.y > 0.5)
        {
            spaceshipRigidbody.MovePosition(spaceshipRigidbody.position - spaceshipTransform.up * 0.1f);
        }
        if (spaceshipRigidbody.position.y < -0.5)
        {
            spaceshipRigidbody.MovePosition(spaceshipRigidbody.position + spaceshipTransform.up * 0.1f);
        }
    }

    /*
     * Method to calculate the angle of a line tangent to a circle. In this case
     * the "line" is the spaceship orientation and the circle is the planet's
     * circumference
     */
    public float GetDelta(int indexPlanet)
    {
        float centerAngle = 180.0f - 2 * Vector3.Angle(planetsTransform[indexPlanet].position - spaceshipTransform.position, spaceshipTransform.forward);
        float deltaTangent = centerAngle / 2;
        return deltaTangent;
    }

    /*
     * Method to determine in which direction the spaceship will have to rotate after
     * a collision in order to (try) to close the distance as much as possible
     * The method has to be called only once and give the initial direction only,
     * otherwise, since it tries to reduce the angle between the planet and the spaceship,
     * it will start oscillating between a positive value and a negative value when the
     * angle is really close to zero but not completely zero
     */
    public void DirectionAfterCollision()
    {
        if (!gotDirection)
        {
            gotDirection = true;
            Vector3 relativePoint = spaceshipTransform.InverseTransformPoint(planetTransform.position);
            if (relativePoint.x > 0.0)
            {
                rotateRight = true;
            }
            else
            {
                rotateRight = false;
            }
        }
    }

    /*
     * Method to rotate after colliding with a planet
     */
     public void RotateAfterCollision()
    {
        DirectionAfterCollision();
        if (rotateRight)
        {
            Quaternion deltaRotation = Quaternion.Euler(Vector3.down * rotationSpeed * Time.fixedDeltaTime);
            spaceshipRigidbody.MoveRotation(spaceshipRigidbody.rotation * deltaRotation);
        }
        else
        {
            Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
            spaceshipRigidbody.MoveRotation(spaceshipRigidbody.rotation * deltaRotation);
        }
    }

    /*
     * Method used to align the spaceship with the destination planet
     */
    public void RotateTowardsPlanet(Transform planetTransform)
    {
        /*
         * To prevent "stupid" rotations (that is, making a long rotation
         * instad of a short one) the autopilot needs to know if the
         * spaceship will have to rotate clockwise or counterclockwise and this
         * is done using the cross product of the two vectors: the direction
         * where the spaceship has to go and the direction the spaceship is
         * facing actually
         * A number greater than zero will mean to make a clockwise rotation, 
         * a number smaller than zero will mean to make a counterclockwise rotation
         */
        target = planetTransform.position - spaceshipTransform.position;
        float whichWay = Vector3.Cross(spaceshipTransform.forward, target.normalized).y;
        /*
         * Get the angle between the planet and the spaceship
         */
        alpha = Vector3.Angle(target, spaceshipTransform.forward);
        /*
         * If the spaceship is not colliding with anything, it can rotate freely
         * until it's (more or less) aligned with the planet, otherwise it will
         * have to get away from the planet before
         */
        if (!CheckCollision(collisions) && alpha > 0.5)
        {
            if (whichWay > 0)
            {
                Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
                spaceshipRigidbody.MoveRotation(spaceshipRigidbody.rotation * deltaRotation);
                //spaceshipTransform.Rotate(Vector3.up * 1f * rotationSpeed * Time.deltaTime);
            }
            else
            {
                Quaternion deltaRotation = Quaternion.Euler(Vector3.down * rotationSpeed * Time.fixedDeltaTime);
                spaceshipRigidbody.MoveRotation(spaceshipRigidbody.rotation * deltaRotation);
                //spaceshipTransform.Rotate(Vector3.down * 1f * rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i])
                {
                    if (planetsName[i] == selectedPlanet)
                    {
                        rotating = true;
                        return;
                    }
                    else
                    {
                        if (alpha > 0.5)
                        {
                            if (whichWay < 0)
                            {
                                Quaternion deltaRotation = Quaternion.Euler(Vector3.down * rotationSpeed * Time.fixedDeltaTime);
                                spaceshipRigidbody.MoveRotation(spaceshipRigidbody.rotation * deltaRotation);
                            }
                            else
                            {
                                Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
                                spaceshipRigidbody.MoveRotation(spaceshipRigidbody.rotation * deltaRotation);
                            }
                        }
                        else
                        {
                            if (fixedDistance < 10f)
                            {
                                if (CheckCollision(collisions))
                                {
                                    deltaTangent = GetDelta(Array.IndexOf(planetsName, selectedPlanet));
                                    if (deltaTangent > 0)
                                    {
                                        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
                                        spaceshipRigidbody.MoveRotation(spaceshipRigidbody.rotation * deltaRotation);
                                    }
                                    else
                                    {
                                        MoveFixedDistance();
                                    }
                                }
                                else
                                {
                                    MoveFixedDistance();
                                }
                            }
                            else
                            {
                                fixedDistance = 0f;
                                justCollided = false;
                            }
                        }
                    }
                }
                else
                {
                    rotating = false;
                }
            }
        }
    }

    /*
     * Enable or disable the autopilot according to
     * the toggle's status
     */
    public void EnableAutopilot(bool enable)
    {
        if (enableAutopilot.isOn)
        {
            autopilotEnabled = true;
            dropdownSelectPlanet.interactable = true;
        }
        else
        {
            planetSelected = false;
            dropdownSelectPlanet.interactable = false;
            dropdownSelectPlanet.value = 0;
        }
    }

    /*
     * If the autopilot mode is enable, get the selected planet
     * from the dropdown menu 
     */
    public void SelectPlanet()
    {
        if (dropdownSelectPlanet.value > 0 && autopilotEnabled)
        {
            planetSelected = true;
            selectedPlanet = (planetsName[dropdownSelectPlanet.value - 1]);
        }
    }
}
