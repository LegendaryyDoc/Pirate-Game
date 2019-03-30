using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopBehavior : MonoBehaviour {
    private ShipBehavior shipBehavior;

    public Button shopEnterButton;
    public Button shopExitButton;
    public Canvas headsUpDisplayCanvas;
    public Canvas pcControlCanvas;
    public Canvas portCanvas;
    public Canvas shipCanvas;
   
    
    private void Start()
    {
        portCanvas.enabled = false;
        shipCanvas.enabled = false;
        shopEnterButton.gameObject.SetActive(false);
        shopEnterButton.onClick.AddListener(delegate { enableShipAndShopCanvas(shipBehavior); });
        shopExitButton.onClick.AddListener(delegate { DisableShipAndShopCanvas(shipBehavior); });
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            shipBehavior = other.GetComponent<ShipBehavior>();
            shopEnterButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shopEnterButton.gameObject.SetActive(false);
        }
    }

    public void DisableShipAndShopCanvas(ShipBehavior shipBehavior)
    {
        headsUpDisplayCanvas.enabled = true;
        pcControlCanvas.enabled = true;
        portCanvas.enabled = false;
        shipBehavior.enabled = true;
        shipCanvas.enabled = false;
        shipBehavior.stopSail();
    }
    public void enableShipAndShopCanvas(ShipBehavior shipBehavior)
    {
        headsUpDisplayCanvas.enabled = false;
        pcControlCanvas.enabled = false;
        portCanvas.enabled = true;
        shipBehavior.enabled = false;
        shipCanvas.enabled = true;
    }
}
