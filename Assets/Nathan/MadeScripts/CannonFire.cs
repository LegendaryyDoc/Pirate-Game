﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  PLAN  */

// Get a cannonball to be fired from the cannons in the direction you are facing
// have a cannonball on the cannons called cannonright or cannonleft
// when looked in the direction of certain cannons it will spawn a cannonball with a velocity that will be fired out in a direction until it hits an object
// once hits an object check if its an enemy
// if it is an enemy then we will get its health component and then subtract damage based on where it was hit and have the cannonball explode, maybe show damage
// if it is not an enemy it will explode on inpact

public class CannonFire : MonoBehaviour
{
    public float ballVelocity = 5.0f;
    private Rigidbody rig;

	// Use this for initialization
	void Start ()
    {
        rig.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void FixedUpdate()
    {

    }
}
