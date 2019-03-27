using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopBehavior : MonoBehaviour {
    public Canvas canvas;
    public GameObject gameObject;
    private ShipBehaviorControls shipBehaviorControls;

    private void Start()
    {
        canvas.enabled = false;
        shipBehaviorControls = gameObject.GetComponent<ShipBehaviorControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == gameObject.name)
        {
            canvas.enabled = true;
            shipBehaviorControls.stopSail();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == gameObject.name)
        {
            canvas.enabled = false;
        }
    }
}
