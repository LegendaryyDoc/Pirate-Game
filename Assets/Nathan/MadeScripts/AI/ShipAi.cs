    
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
    public float fireDistance = 20f;
    public float ballVelocity = 1500f;
    public bool rightSide;

    private float distanceBetween;
    private bool toClose; // if to close then switched the direction
    private Vector3 direction; // direction moving
    private Vector3 relativePoint; // used for determining if other object is on the left or right of the ai
    private int closestTerm = -1;

    public int amountOfTerminals = 12;
    private Vector3[] terminals;


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

    private void Awake()
    {
        terminals = new Vector3[amountOfTerminals];
        int angle = 0;
        for (int i = 0; i < terminals.Length; i++)
        {
            angle += 360/amountOfTerminals;

            var x = radiusNearestPoint * Mathf.Cos(Mathf.Deg2Rad * angle);
            var y = radiusNearestPoint * Mathf.Sin(Mathf.Deg2Rad * angle);
            terminals[i] = new Vector3(x, 3, y);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (target == null)
        {
            return;
        }

        /*------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------   Basic Movement Towards Player  -----------------------------------*/
        /*------------------------------------------------------------------------------------------------------------*/

        if (movement == true)
        {
            var point = GetPoint();
            // rotate to the target

            var lookPos = point - transform.position; // gets the offset of the 2 objects
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos, Vector3.up); // uses the offest and makes it a angle

            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, perc); // rotates the ai
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);

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
            rightSide = true;
        }
        else if(relativePoint.x < 0)
        {
            rightSide = false;
        }
    }

    Vector3 GetPoint() // Gets the nearest node
    {
        closestTerm = 0;
        float closestCurrentDist = Vector3.Distance(transform.position, target.position + terminals[closestTerm]);
        for(int i = 1; i < terminals.Length; i++)
        {
            float contenderClosestDist = Vector3.Distance(transform.position, target.position + terminals[i]);
            
            if(contenderClosestDist < closestCurrentDist)
            {
                closestTerm = i;
                closestCurrentDist = contenderClosestDist;
            }
        }

        if (rightSide == true)
        {
            if (closestTerm == 0)
            {
                closestTerm = terminals.Length - 1;
            }
            else
            {
                closestTerm = closestTerm - 1;
            }
        }
        else
        {
            if (closestTerm == terminals.Length)
            {
                closestTerm = 0;
            }
            else
            {
                closestTerm = (closestTerm + 1) % terminals.Length;
            }
        }
        return target.position + terminals[closestTerm];
    }

    private void OnDrawGizmos() // Draws all the gizmos
    {
        if(target == null || gizmos == false)
        {
            return;
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < terminals.Length; i++)
        {
            Gizmos.DrawWireSphere(target.position + terminals[i], 1);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target.position + terminals[closestTerm]);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, fireDistance);
    }
}