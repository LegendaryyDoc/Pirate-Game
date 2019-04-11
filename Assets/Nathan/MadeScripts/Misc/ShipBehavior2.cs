using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ShipBehavior2 : MonoBehaviour
{
    new Rigidbody rigidbody = new Rigidbody();

    //private float health;
    //private float respawnTimer = 0.0f;
    private float rotateSpeed = 0.5f;
    //private RespawnBehavior respawnBehavior;
    private Transform tfCam;

    //[HideInInspector]
    //public float shipHealth;
    //public ShopScrollList sSL;
    //public UserStatistics userStatistics;

    static float currentKnots = 0.0f;
    static Vector3 vector3 = new Vector3();


    void Start()
    {
       // shipHealth = userStatistics.shipHealth;

        //respawnBehavior = this.GetComponent<RespawnBehavior>();
        //respawnBehavior.Respawn();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        tfCam = Camera.main.transform;
    }

    void FixedUpdate()
    {
        Debug.Log("Current Knots: " + currentKnots);
        //health = userStatistics.health;
        //userStatistics.shipHealth = shipHealth;

        //if (health <= 0 | shipHealth <= 0)
        //{
            //shipAnimator.Play("isShipDestroyed")
         //   respawnBehavior.Respawn();
         ///   userStatistics.health = 100.0f;
        //    userStatistics.shipHealth = 100.0f;
        //}

        
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
            transform.Translate(vector3 * Time.deltaTime);

            

            if (Input.GetKeyDown(KeyCode.W))
            {
                currentKnots += 1.5f;

                if (currentKnots > 10.0f)
                {
                    currentKnots = 10.0f;
                }
                vector3 = new Vector3(currentKnots, 0.0f, 0.0f);
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                currentKnots -= 1.5f;

                if (currentKnots < 0.0f)
                {
                    currentKnots = 0.0f;
                }
                vector3 = new Vector3(currentKnots, 0.0f, 0.0f);
            }
        
    }

    private void OnTriggerStay(Collider other)
    {
       /* if (other.tag == "Port")
        {
            sSL.otherShop = other.GetComponentInChildren<ShopScrollList>();
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.tag == "Port")
        {
            sSL.otherShop = null;
        }*/
    }

    // Ship Functions
}
