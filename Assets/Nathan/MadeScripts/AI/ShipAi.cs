    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAi : MonoBehaviour
{
    /*--- All Variables ---*/
    #region variables 
    public float health = 100f;

    public bool movement = true; // for shutting off movement section of code
    public bool gizmos = true; // for shutting off gizmos drawing

    public float radiusNearestPoint = 30f;

    public Transform target; // other object
    public float toCloseDistance = 10f;
    public float speed = .5f;
    public float maxSpeed = 5f;
    public float turnSpeed = 10f;
    public float fireDistance = 20f;
    public float ballVelocity = 1500f;
    public float islandAlertDistance = 20f;
    public bool rightSide;

    private float distanceBetween;
    private bool toClose; // if to close then switched the direction
    private Vector3 direction; // direction moving
    private Vector3 relativePoint; // used for determining if other object is on the left or right of the ai
    private int closestTerm = -1;
    private Vector3 point;

    public int amountOfTerminals = 12;
    private Vector3[] terminals;
    private bool[] terminalGoodToTravel;
    private bool newTerminalNeeded = true;
    public float groundCheckLength = 3.5f;
    #endregion


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
        terminalGoodToTravel = new bool[amountOfTerminals];
        int angle = 0;
        for (int i = 0; i < terminals.Length; i++)
        {
            angle += 360/amountOfTerminals;

            var x = radiusNearestPoint * Mathf.Cos(Mathf.Deg2Rad * angle);
            var y = radiusNearestPoint * Mathf.Sin(Mathf.Deg2Rad * angle);
            terminals[i] = new Vector3(x, 3, y);
        }
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        if (target == null)
        {
            return;
        }

        /*------------------------------------------------------------------------------------------------------------*/
        /*--------------------------------------  Line Boat Up So Can Fire Cannons  ----------------------------------*/
        /*------------------------------------------------------------------------------------------------------------*/

        relativePoint = transform.InverseTransformPoint(target.position); // checks what side the player ship is on
        if (relativePoint.x > 0)
        {
            rightSide = true;
        }
        else if (relativePoint.x < 0)
        {
            rightSide = false;
        }

        /*------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------  RayCasting Down From Terminals  -----------------------------------*/
        /*------------------------------------------------------------------------------------------------------------*/

        RaycastHit hitTerm;

        for (int i = 0; i < amountOfTerminals; i++) // going through each terminal so to check if the terminal is over a island
        {
            if (Physics.Raycast(target.position + terminals[i], Vector3.down, out hitTerm, groundCheckLength) && hitTerm.transform.gameObject.tag == "Floor")
            {
                terminalGoodToTravel[i] = false;
            }
            else
            {
                terminalGoodToTravel[i] = true;
            }
        }

        /*------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------  RayCasting To Avoid Islands  -------------------------------------*/
        /*------------------------------------------------------------------------------------------------------------*/

        //DrawRay(transform.position, transform.forward, Color.red);

        RaycastHit hit;

        
        if (Physics.Raycast(transform.position + Vector3.down * 1.5f, transform.forward, out hit, islandAlertDistance) && hit.transform.gameObject.tag == "Floor") // checking to see if raycast hits a island in the direction moving
        {
            Debug.DrawRay(transform.position + Vector3.down * 1.5f, transform.forward * islandAlertDistance, Color.red);
            if (hit.distance <= islandAlertDistance) // checks to see if the distance the ray hit at is less than the distance it should be turning to avoid hitting the island
            {
                float pDistance = (islandAlertDistance - hit.distance) / islandAlertDistance;
                float newSpeed = maxSpeed * pDistance; // reduces the speed based off of distance

                Vector3 perpVec = transform.right;
                //Vector3 perpVec =  (-transform.forward).perp;
                //Vector3 perpVec = Vector3.Cross(direction, Vector3.up).normalized; // gives back a perpendicular vector

                var rotation = Quaternion.LookRotation(perpVec, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
                transform.Translate(transform.forward * Time.deltaTime * newSpeed);
            }
            return;
        }
        Debug.DrawRay(transform.position + Vector3.down * 1.5f, transform.forward * islandAlertDistance, Color.yellow);

        /*------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------------   Basic Movement Towards Player  -----------------------------------*/
        /*------------------------------------------------------------------------------------------------------------*/

        if (movement == true)
        {
            if (newTerminalNeeded == true) // when ai makes it to a terminal will need to get a new terminal
            {
                 point = GetPoint();
            }

            float dis = Vector3.Distance(transform.position, target.position + terminals[closestTerm]);

            if (dis <= 10) // if at the position as the terminal
            {
                newTerminalNeeded = true; // set so need a new terminal
            }

            // rotate to the target
            var lookPos = point - transform.position; // gets the offset of the 2 objects
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos, Vector3.up); // uses the offest and makes it a angle

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);

            distanceBetween = Vector3.Distance(transform.position, target.position); // checks to see how close or far the player is from the ai
            toClose = distanceBetween < toCloseDistance; // checks to see if to close
            direction = Vector3.forward; // checks if to close or not and will go in a certain direction if it is or not

            transform.Translate(direction * Time.deltaTime * speed); // move in the direction towards the player
        }
    }

    Vector3 GetPoint() // Gets the nearest node
    {
        newTerminalNeeded = false;
        closestTerm = 0;
        float closestCurrentDist = Vector3.Distance(transform.position, target.position + terminals[closestTerm]);
        for (int i = 0; i < terminals.Length; i++)
        {
            float contenderClosestDist = Vector3.Distance(transform.position, target.position + terminals[i]);
        
            if (contenderClosestDist < closestCurrentDist && terminalGoodToTravel[i] == true)
            {
                closestTerm = i;
                closestCurrentDist = contenderClosestDist;
            }
        }
        
        bool goRight = true; // used to check if should go left or right based on if the next terminal is over land or not
        bool goLeft = true;
        
        if (terminalGoodToTravel[closestTerm - 1] == false) // if the one to thye right is over land
        {
            goRight = false;
        }
        if (terminalGoodToTravel[closestTerm + 1] == false) // if the one to the left is over land
        {
            goLeft = false;
        }
        
        if (goRight == true && rightSide == false || rightSide == true && goLeft == false && goRight == true) // check to see if should go right
        {
            if (closestTerm == 0) // if closest term = 0 set it to be the amount of terminals
            {
                closestTerm = terminals.Length - 1;
            }
           /* else
            {
                closestTerm = closestTerm - 1;
            }*/
        }
        else if (goLeft == true && rightSide == true || rightSide == false && goRight == false && goLeft == true) // check to see if should go left
        {
            if (closestTerm == terminals.Length) // if closest term = max amount of terminals then set closest term to 0
            {
                closestTerm = 0;
            }
            else
            {
                closestTerm = (closestTerm) % terminals.Length;
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
            if (terminalGoodToTravel[i])
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.yellow;
            }

            Gizmos.DrawWireSphere(target.position + terminals[i], 1);
            Gizmos.DrawLine(target.position + terminals[i], target.position + terminals[i] + Vector3.down * groundCheckLength);
            
        }

        if(closestTerm == -1)
        {
            return;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target.position + terminals[closestTerm]);
    }
}