using System.Collections;
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
    public GameObject cannonBallPrefab;
    public GameObject camera;
    public float ballVelocity = 5.0f;
    public float delayMin = 0f;
    public float delayMax = 0f;

    private float randomNumber;
    private Vector3 direction;
    private GameObject cannon;


	// Use this for initialization
	void Start ()
    {
        randomNumberGen();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(tag == "RightCannon" && camera.GetComponent<Camera>().transform.rotation.eulerAngles.y >= 80 && camera.GetComponent<Camera>().transform.rotation.eulerAngles.y <= 100)
            {
                direction = transform.TransformDirection(Vector3.right);
            }
            else if (tag == "LeftCannon" && camera.GetComponent<Camera>().transform.rotation.eulerAngles.y >= 260 && camera.GetComponent<Camera>().transform.rotation.eulerAngles.y <= 280)
            {
                direction = transform.TransformDirection(Vector3.left);
            }
            else if (camera.GetComponent<Camera>().transform.rotation.eulerAngles.y >= -20 && camera.GetComponent<Camera>().transform.rotation.eulerAngles.y <= 20 || camera.GetComponent<Camera>().transform.rotation.eulerAngles.y >= 160 && camera.GetComponent<Camera>().transform.rotation.eulerAngles.y <= 200)
            {
                if (tag == "RightCannon")
                {
                    direction = transform.TransformDirection(Vector3.right);
                }
                else if (tag == "LeftCannon")
                {
                    direction = transform.TransformDirection(Vector3.left);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

            cannon = Instantiate(cannonBallPrefab, transform.position, transform.rotation);

            cannon.GetComponent<Rigidbody>().AddForce(direction * ballVelocity, ForceMode.Force);

            randomNumberGen();
        }
	}

    void randomNumberGen()
    {
        randomNumber = Random.Range(delayMin, delayMax);
    }
}
