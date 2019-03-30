using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBouyancy : MonoBehaviour
{
    private float timer = 0; // timer for side movement of the ship
    private float startTime; // gets the time at the start

    private float fbSwayTimer = 0; // time for forward and back movement
    public float fbMin = -.1f; // rotation min
    public float fbMax = .1f; // rotation max 
    public float fbDuration = 3; // how long rotations will last
    private bool fbRotationPos = false; // used for switching between the rotations
    
    public float frequencyMin = -.1f; // side to side rotation min
    public float frequencyMax = .1f; // rotation max
    public float duration = 3; // how long rotations will last
    private bool rotationPos = false; // used for switching

    public float floatForce = 50f; // how much force is being applied upwards on the boat to keep it floating on the water
    public float floatheight = 3.2f; // height at which you want the boat to be floating 

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

    void FixedUpdate()
    {

        /*-------------------------------------------------------------*/
        /*-------------------------------------------------------------*/

        timer += Time.deltaTime; // timer for side to side sway
        fbSwayTimer += Time.deltaTime; // timer for forward and back sway

        float t = (Time.time - startTime) / duration;

        /*-------------------------------------------------------------*/
        /*-------------------------------------------------------------*/

        if (rotationPos == true) // for side to side sway
        {
            transform.Rotate(new Vector3(Mathf.LerpAngle(transform.rotation.x, frequencyMax, t), 0f, 0f));
        }
        else
        {
            transform.Rotate(new Vector3(Mathf.LerpAngle(transform.rotation.x, frequencyMin, t), 0f, 0f));
        }

        if (timer >= duration)
        {
            rotationPos = !rotationPos;
            timer = 0;
        }

        /*-------------------------------------------------------------*/
        /*-------------------------------------------------------------*/

        if (rotationPos == true) // for forward to back sway
        {
            transform.Rotate(new Vector3(0f, 0f, Mathf.LerpAngle(transform.rotation.z, fbMax, t)));
        }
        else
        {
            transform.Rotate(new Vector3(0f, 0f, Mathf.LerpAngle(transform.rotation.z, fbMin, t)));
        }

        if (fbSwayTimer >= fbDuration)
        {
            fbRotationPos = !fbRotationPos;
            fbSwayTimer = 0;
        }

        /*-------------------------------------------------------------*/
        /*-------------------------------------------------------------*/

        Ray ray = new Ray(transform.position, -transform.up); // sending a ray down towards the ground from objects position
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, floatheight) && hit.transform.gameObject.tag == "Floor") // sends out a ray at the distance over hover height and then detects the hit
        {
            float proportionalHeight = (floatheight - hit.distance) / floatheight; // changes the height from the ground
            Vector3 appliedFloatingForce = Vector3.up * proportionalHeight * floatForce; // applies a force upward based on how close to the ground
            rig.AddForce(appliedFloatingForce, ForceMode.Acceleration); // applies the force as an acceleration
        }
    }
}
