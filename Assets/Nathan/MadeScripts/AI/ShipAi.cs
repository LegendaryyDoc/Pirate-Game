using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAi : MonoBehaviour
{
    public Transform target;
    public float toCloseDistance = 10f;
    private float distanceBetween;
    
    private bool toClose;
    private Vector3 direction;
    private Quaternion targetRotation;
    private float str;
    private float strength = .5f;

    /*-----   Check if player is in range   -----*/

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger");
        if (other.tag == "Player")
        {
            target = other.transform;
            //Debug.Log(other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            target = null;
        }
    }

    /*-------------------------------------------*/
    /*-------------------------------------------*/


    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (target == null)
        {
            return;
        }


        //Debug.Log(target.rotation.eulerAngles);
        // rotate to the target
        //transform.LookAt(target); // ai looks towards the target
        targetRotation = Quaternion.LookRotation(target.position - transform.position);
        Debug.Log(targetRotation.eulerAngles);
        str = Mathf.Min(strength * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);

        distanceBetween = Vector3.Distance(transform.position, target.position); // checks to see how close or far the player is from the ai
        toClose = distanceBetween < toCloseDistance; // checks to see if to close
        direction = toClose ? Vector3.back : Vector3.forward; // checks if to close or not and will go in a certain direction if it is or not

        // move in the direction towards the player
        transform.Translate(direction * Time.deltaTime);

        // if player can be hit by cannons
        // check what side the player is on
        // shoot the cannons when can
        // keep the side of the ship you are attacking with inline with the player ship
	}
}
