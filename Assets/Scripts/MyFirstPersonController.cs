using UnityEngine;
using System.Collections;

public class MyFirstPersonController : MonoBehaviour
{

    Transform cameraTransform;
    float pitchR, yawR, pitchT, yawT;

    // Use this for initialization
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        pitchR = Input.GetAxis("Mouse Y");
        yawR = Input.GetAxis("Mouse X");

        cameraTransform.Rotate(pitchR, 0f, 0f, Space.Self);
        cameraTransform.Rotate(0f, yawR, 0f, Space.Self);

        pitchT = Input.GetAxis("Horizontal");
        yawT = Input.GetAxis("Vertical");

        cameraTransform.Translate(pitchT * Time.deltaTime, 0f, yawT * Time.deltaTime, Space.Self);

    }
}
