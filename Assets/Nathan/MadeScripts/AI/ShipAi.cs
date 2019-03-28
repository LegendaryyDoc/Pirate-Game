using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAi : MonoBehaviour
{
    public Transform target;
    public float toCloseDistance = 10f;
    private float distanceBetween;
    public float speed = .5f;
    
    private bool toClose;
    private Vector3 direction;
    private Quaternion targetRotation;
    private float str;
    private float strength = .5f;
    private float lerpTime = 1f;
    private float currentLerpTime;

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
        currentLerpTime += Time.deltaTime;

        if (target == null)
        {
            return;
        }

        // rotate to the target
        float perc = currentLerpTime / lerpTime;

        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, perc);

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
