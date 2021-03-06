﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFireAI : MonoBehaviour {

    public Transform target;
    public GameObject cannonBallPrefab;
    public float fireDistance = 30f;
    public float ballVelocity = 1500f;

    private GameObject cannon;
    private float cooldownTimerForCannonballs = 0;
    private float cannonShootDelay = 3f;

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

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        cooldownTimerForCannonballs -= Time.deltaTime;
        if(target == null)
        {
            return;
        }

        if (cooldownTimerForCannonballs >= 0)
        {
            return;
        }

        if (Vector3.Distance(target.position, transform.position) <= fireDistance) // if cannon is in firing distance of player
        {
            cannon = Instantiate(cannonBallPrefab, transform.position, transform.rotation); // creates a cannonball
            if (tag == "RightCannon") // checks to see what side the player is and fires the cannons accordingly
            {
                //cannon.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.right) * ballVelocity, ForceMode.Force);

                Vector3 dir = transform.right;
                dir += (.1f * transform.up);
                cannon.GetComponent<Rigidbody>().AddForce(dir * ballVelocity, ForceMode.Force);
            }
            else if (tag == "LeftCannon")
            {
                //cannon.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.left) * ballVelocity, ForceMode.Force);

                Vector3 dir = -transform.right;
                dir += (.1f * transform.up);
                cannon.GetComponent<Rigidbody>().AddForce(dir * ballVelocity, ForceMode.Force);
            }
            cooldownTimerForCannonballs = cannonShootDelay; // resets the cannon countdown until can fire again
        }
    }
}
