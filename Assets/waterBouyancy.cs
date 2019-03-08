using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBouyancy : MonoBehaviour {
    float frequencyMin = -.2f;
    float frequencyMax = .2f;
    private float randomInterval;

    Vector3 gravity = Physics.gravity;

    public float floatStrength;

    Rigidbody rig;

	// Use this for initialization
	void Start ()
    {
        randomInterval = Random.Range(frequencyMin, frequencyMax);
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // for bobbing effect
        rig.AddForce(-gravity);
        transform.Rotate(randomInterval, randomInterval, 0);
        transform.Rotate(-randomInterval, -randomInterval, 0);
	}
}
