using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject cannonBalls;
    public TextMeshProUGUI shopPcEnterKeyCode;
    public TextMeshProUGUI shopPcExitKeyCode;

    private void Start()
    {
        portCanvas.enabled = false;
        shipCanvas.enabled = false;
        shopEnterButton.gameObject.SetActive(false);
        shopPcEnterKeyCode.enabled = false;

        if (Application.platform == RuntimePlatform.Android)
        {
            shopEnterButton.onClick.AddListener(delegate { enableShipAndShopCanvas(shipBehavior); });
            shopExitButton.onClick.AddListener(delegate { DisableShipAndShopCanvas(shipBehavior); });
        }
    }
    private void Update()
    {
        if (shipCanvas.enabled == true)
        {
            if (Application.platform == (RuntimePlatform.WindowsEditor | RuntimePlatform.WindowsPlayer))
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    DisableShipAndShopCanvas(shipBehavior);
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            cannonBalls.SetActive(false);
            shipBehavior = other.GetComponent<ShipBehavior>();
            shopEnterButton.gameObject.SetActive(true);

            if(Application.platform == (RuntimePlatform.WindowsEditor | RuntimePlatform.WindowsPlayer))
            {
                shopPcEnterKeyCode.enabled = true;
                if (Input.GetKeyDown(KeyCode.I))
                {
                    enableShipAndShopCanvas(shipBehavior);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cannonBalls.SetActive(true);
            shopEnterButton.gameObject.SetActive(false);
            shopPcEnterKeyCode.enabled = false;
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
        shopPcExitKeyCode.enabled = false;
    }
    public void enableShipAndShopCanvas(ShipBehavior shipBehavior)
    {
        headsUpDisplayCanvas.enabled = false;
        pcControlCanvas.enabled = false;
        portCanvas.enabled = true;
        shipBehavior.enabled = false;
        shipCanvas.enabled = true;
        shopPcExitKeyCode.enabled = true;
    }
}
