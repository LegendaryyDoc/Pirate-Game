using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBouyancy : MonoBehaviour
{
    private float timer = 0;
    private float startTime;
    
    public float frequencyMin = -.1f;
    public float frequencyMax = .1f;
    public float duration = 3;
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

        /*-------------------------------------------------------------*/
        /*-------------------------------------------------------------*/

        timer += Time.deltaTime;

        float t = (Time.time - startTime) / duration;

        if (rotationPos == true)
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



        /*-------------------------------------------------------------*/
        /*-------------------------------------------------------------*/


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


            /*--------------------------------------------------------------------------------------------*/
            /*--------------------------------------------------------------------------------------------*/

            /* trying to get the bobbing to be less stiff by using point force to allow more torque when it is going up and down */
            /* currently rotates it in the wrong direction because it is off center of the ship giving it insane torque need to get origin point in the middle of the ship at least on the z */

            //rig.AddForceAtPosition(appliedFloatingForce, transform.parent.position);
            //rig.AddForceAtPosition(appliedFloatingForce, new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z - 4.175f)); 

            /*--------------------------------------------------------------------------------------------*/
            /*--------------------------------------------------------------------------------------------*/
        }
    }
}
