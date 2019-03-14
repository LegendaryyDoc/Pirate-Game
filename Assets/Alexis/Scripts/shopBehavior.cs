using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopBehavior : MonoBehaviour {
    public Canvas canvas;
    public GameObject gameObject;
    private ShipBehavior shipBehavior;

    private void Start()
    {
        canvas.enabled = false;
        shipBehavior = gameObject.GetComponent<ShipBehavior>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == gameObject.name)
        {
            canvas.enabled = true;
            shipBehavior.noSail();
        }
    }
}
