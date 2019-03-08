using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour {

    public float maxSpeed = 5.0f;
    public float speed = 0.0f;
    public float accel = .2f;
    public float rotateSpeed = 0.5f;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0); // changes the rotation of the player

        float vertical = Input.GetAxis("Vertical");

        transform.Translate(0, 0, vertical * maxSpeed * Time.deltaTime);
    }
}
