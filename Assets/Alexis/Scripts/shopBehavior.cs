using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class shopBehavior : MonoBehaviour {
    private Canvas currentPortCanvas;
    private Canvas portCanvas;
    private ShipBehavior shipBehavior;

    public Button shopEnterButton;
    public Button shopExitButton;
    public Canvas headsUpDisplayCanvas;
    public Canvas pcControlCanvas;
    // public Canvas portCanvas;
    public Canvas shipCanvas;
    public GameObject cannonBalls;
    public TextMeshProUGUI shopPcEnterKeyCode;

    private void Start()
    {
        portCanvas = this.GetComponentInChildren<Canvas>();
        portCanvas.enabled = false;
        shipCanvas.enabled = false;
        shopEnterButton.gameObject.SetActive(false);
        shopExitButton.gameObject.SetActive(false);
        shopPcEnterKeyCode.enabled = false;

        //shopEnterButton.onClick.AddListener(delegate { enableShipAndShopCanvas(shipBehavior); });
        //shopExitButton.onClick.AddListener(delegate { DisableShipAndShopCanvas(shipBehavior); });
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            currentPortCanvas = this.portCanvas;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Current Port Canvas: " + portCanvas);

            cannonBalls.SetActive(false);
            shipBehavior = other.GetComponent<ShipBehavior>();
            shopEnterButton.onClick.AddListener(delegate { enableShipAndShopCanvas(shipBehavior); });
            shopExitButton.onClick.AddListener(delegate { DisableShipAndShopCanvas(shipBehavior); });
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
            currentPortCanvas = null;
            cannonBalls.SetActive(true);
            shopEnterButton.gameObject.SetActive(false);
            shopPcEnterKeyCode.enabled = false;
        }
    }

    public void DisableShipAndShopCanvas(ShipBehavior shipBehavior)
    {
        currentPortCanvas.enabled = false;
        headsUpDisplayCanvas.enabled = true;
        pcControlCanvas.enabled = true;
        shipBehavior.enabled = true;
        shipCanvas.enabled = false;
        shipBehavior.stopSail();
    }
    public void enableShipAndShopCanvas(ShipBehavior shipBehavior)
    {
        if(currentPortCanvas == null)
        {
            return;
        }
        currentPortCanvas.enabled = true;
        shipCanvas.enabled = true;
        headsUpDisplayCanvas.enabled = false;
        pcControlCanvas.enabled = false;
        shipBehavior.enabled = false;
        shopExitButton.gameObject.SetActive(true);
    }
}
