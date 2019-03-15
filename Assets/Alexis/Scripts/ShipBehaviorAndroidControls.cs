﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ShipBehaviorAndroidControls : MonoBehaviour {

    public Button increaseSail;
    public Button decreaseSail;

    private float speed = 0.0f;
    public float rotateSpeed = 0.5f;

    Rigidbody rigidbody = new Rigidbody();
    static Vector3 vector3 = new Vector3();

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //This locks the RigidBody so that it does not move or rotate in the X axis.
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

        Button increaseSailButton = increaseSail.GetComponent<Button>();
        Button decreaseSailButton = decreaseSail.GetComponent<Button>();

        increaseSailButton.onClick.AddListener(go);
        decreaseSailButton.onClick.AddListener(stop);
    }

    // Update is called once per frame
    void Update()
    {
        
        increaseSail.onClick.AddListener(go);
        increaseSail.onClick.AddListener(stop);

        transform.Rotate(0, CrossPlatformInputManager.GetAxis("Horizontal") * rotateSpeed, 0);
        transform.Translate(vector3 * Time.deltaTime);
    }
    
    public void go()
    {
        vector3 = new Vector3(++speed, 0.0f, 0.0f);
    }

    public void noSail()
    {
        vector3 = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void stop()
    {
        vector3 = new Vector3(--speed, 0.0f, 0.0f);
    }
}
