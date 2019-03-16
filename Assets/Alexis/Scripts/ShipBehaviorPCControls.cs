using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShipBehaviorPCControls : MonoBehaviour
{
    private Transform tfCam;
    public float currentKnots = 0.0f;
    public float rotateSpeed = 0.5f;

    new Rigidbody rigidbody = new Rigidbody();
    Vector3 vector3 = new Vector3();

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        tfCam = Camera.main.transform;
    }

    void FixedUpdate()
    {
        // Debug.Log("Current Knots: " + currentKnots);
        float moveVertical = Input.GetAxis("Vertical");
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0); 
        vector3 = new Vector3(Input.GetAxis("Vertical") , 0.0f, 0.0f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentKnots += 1.5f;
        }
        if(moveVertical > 0)
        {
            transform.Translate(vector3 * currentKnots * Time.deltaTime);
        }
    }
}