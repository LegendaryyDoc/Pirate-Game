﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopBehavior : MonoBehaviour {
    private ShipBehaviorAndroidControls shipBehaviorAndroidControls;
    private ShipBehaviorPCControls shipBehaviorPCControls;
    public Canvas canvas;
    public GameObject gameObject;

    private void Start()
    {
        canvas.enabled = false;
        shipBehaviorAndroidControls = gameObject.GetComponent<ShipBehaviorAndroidControls>();
        shipBehaviorPCControls = gameObject.GetComponent<ShipBehaviorPCControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == gameObject.name)
        {
            canvas.enabled = true;
            shipBehaviorAndroidControls.noSail();
            shipBehaviorAndroidControls.rotateSpeed = 0.0f;
            shipBehaviorPCControls.speed = 0.0f;
            shipBehaviorPCControls.rotateSpeed = 0.0f;
        }
    }
}