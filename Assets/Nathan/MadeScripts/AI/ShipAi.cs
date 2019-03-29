using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAi : MonoBehaviour
{
    public Transform target;
    public float toCloseDistance = 10f;
    private float distanceBetween;
    public float speed = .5f;
    public float turnSpeed = 10f;
    
    private bool toClose;
    private Vector3 direction;
    private float lerpTime = 1f;
    private float currentLerpTime;

    /*-----   Check if player is in range   -----*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.transform;
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
	
	// Update is called once per frame
	void Update ()
    {
        if (target == null)
        {
            return;
        }
        currentLerpTime += Time.deltaTime;

        // rotate to the target
        float perc = currentLerpTime / lerpTime / turnSpeed; // for the lerp


        var lookPos = target.position - transform.position; // gets the offset of the 2 objects
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos, Vector3.up); // uses the offest and makes it a angle

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, perc); // rotates the ai

        distanceBetween = Vector3.Distance(transform.position, target.position); // checks to see how close or far the player is from the ai
        toClose = distanceBetween < toCloseDistance; // checks to see if to close
        direction = toClose ? Vector3.back : Vector3.forward; // checks if to close or not and will go in a certain direction if it is or not

        // move in the direction towards the player
        transform.Translate(direction * Time.deltaTime * speed);
	}
}
