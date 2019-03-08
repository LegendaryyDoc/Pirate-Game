using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBouyancy : MonoBehaviour {
    public float frequencyMin = -.1f;
    public float frequencyMax = .1f;
    public float duration = 3;
    float startTime;
    private bool rotationPos = false;

    public float floatForce = 50f;
    public float floatheight = 3.2f;

    private float powerInput;

    Rigidbody rig;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start ()
    {
        startTime = Time.time;
	}

    private void Update()
    {
    }

    void FixedUpdate()
    {
        float t = (Time.time - startTime) / duration;

        if (rotationPos == true)
        {
            transform.Rotate(new Vector3(0f, 0f, Mathf.Lerp(frequencyMin, frequencyMax, t)));
        }
        else
        {
            transform.Rotate(new Vector3 (0f, 0f, Mathf.Lerp(frequencyMax, frequencyMin, t)));
        }

        if (transform.eulerAngles.z > 3f || transform.eulerAngles.z < -3f)
        {
            rotationPos = !rotationPos;
        }

        /*---------------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------------------------------------------------------------------------------------*/

        Ray ray = new Ray(transform.position, -transform.up); // sending a ray down towards the ground from objects position
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, floatheight)) // sends out a ray at the distance over hover height and then detects the hit
        {
            float proportionalHeight = (floatheight - hit.distance) / floatheight; // changes the height from the ground
            Vector3 appliedFloatingForce = Vector3.up * proportionalHeight * floatForce; // applies a force upward based on how close to the ground
            rig.AddForce(appliedFloatingForce, ForceMode.Acceleration); // applies the force as an acceleration
        }
    }
}
