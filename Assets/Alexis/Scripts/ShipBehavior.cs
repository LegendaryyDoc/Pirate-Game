using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipBehavior : MonoBehaviour {

    public float speed = 5.0f;
    public float rotateSpeed = 0.5f;

    Rigidbody rigidbody = new Rigidbody();
    Vector3 vector3 = new Vector3();

    public Button button;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //This locks the RigidBody so that it does not move or rotate in the X axis.
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0); // changes the rotation of the player

        vector3 = new Vector3(Input.GetAxis("Vertical"), 0.0f,  0.0f);

        transform.Translate(vector3 * speed * Time.deltaTime);
    }

    public void rotateLeft()
    {
        transform.Rotate(0, -rotateSpeed, 0); // changes the rotation of the player
    }
}
