using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAi : MonoBehaviour
{
    public bool movement = true; // for shutting off movement section of code
    public bool gizmos = true; // for shutting off gizmos drawing

    public float radiusNearestPoint = 30f;

    public Transform target; // other object
    public float toCloseDistance = 10f;
    public float speed = .5f;
    public float turnSpeed = 10f;

    private float PI = 3.14159265359f;
    private float distanceBetween;
    private bool toClose; // if to close then switched the direction
    private Vector3 direction; // direction moving
    private float lerpTime = 1f; // max lerp life
    private float currentLerpTime; // how far in the lerp currently
    private Vector3 relativePoint; // used for determining if other object is on the left or right of the ai

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

        /*------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------   Basic Movement Towards Player  -----------------------------------*/
        /*------------------------------------------------------------------------------------------------------------*/

        if (movement == true)
        {

            var point = GetPoint();
            // rotate to the target
            float perc = currentLerpTime / lerpTime / turnSpeed; // for the lerp

            var lookPos = target.position + point - transform.position; // gets the offset of the 2 objects
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos, Vector3.up); // uses the offest and makes it a angle

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, perc); // rotates the ai

            distanceBetween = Vector3.Distance(transform.position, target.position); // checks to see how close or far the player is from the ai
            toClose = distanceBetween < toCloseDistance; // checks to see if to close
            direction = toClose ? Vector3.back : Vector3.forward; // checks if to close or not and will go in a certain direction if it is or not

            transform.Translate(direction * Time.deltaTime * speed); // move in the direction towards the player
        }

        /*------------------------------------------------------------------------------------------------------------*/
        /*--------------------------------------  Line Boat Up So Can Fire Cannons  ----------------------------------*/
        /*------------------------------------------------------------------------------------------------------------*/

        relativePoint = transform.InverseTransformPoint(target.position); // checks what side the player ship is on
        if(relativePoint.x > 0)
        {
            Debug.Log("Right");
        }
        else if(relativePoint.x < 0)
        {
            Debug.Log("Left");
        }
	}

    Vector3 GetPoint()
    {
        float difX = (transform.position.x - target.position.x);
        float difY = (transform.position.z - target.position.z);

        float angle = Mathf.Atan2(difY, difX);

        var x = radiusNearestPoint * Mathf.Cos(angle);
        var y = radiusNearestPoint * Mathf.Sin(angle);
        return new Vector3(x, 0, y);
    }

    private void OnDrawGizmos()
    {
        if(target == null || gizmos == false)
        {
            return;
        }

        var point = GetPoint();

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(new Vector3(target.position.x + point.x, 3, target.position.z + point.z), 1);
    }
}
