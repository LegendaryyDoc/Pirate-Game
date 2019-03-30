using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ShipBehavior : MonoBehaviour
{
    new Rigidbody rigidbody = new Rigidbody();

    // private Canvas shopCanvas;
    private float rotateSpeed = 0.5f;
    private Transform tfCam;

    public Animator animator;
    [HideInInspector]
    //public Canvas playerInventoryCanvas;
    public ShopScrollList sSL;

    static float currentKnots = 0.0f;
    static Vector3 vector3 = new Vector3();

    // Android Controls
    public Button DecreaseSail;
    public Button IncreaseSail;

    void Start()
    {
        animator.enabled = false;
        //playerInventoryCanvas.enabled = false;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        tfCam = Camera.main.transform;
        //playerInventoryCanvas.enabled = false;

        if (Application.platform == RuntimePlatform.Android)
        {
            DecreaseSail = GameObject.Find("Decrease Sail").GetComponent<Button>();
            IncreaseSail = GameObject.Find("Increase Sail").GetComponent<Button>();

            Button increaseSailButton = IncreaseSail;
            Button decreaseSailButton = DecreaseSail;
        }
    }

    void FixedUpdate()
    {
        Debug.Log("Current Knots: " + currentKnots);

        if (Application.platform == RuntimePlatform.Android)
        {
            if (CrossPlatformInputManager.GetAxis("Horizontal") != 0)
            {
                animator.enabled = true;
            }
            else
            {
                animator.enabled = false;
            }

            DecreaseSail.onClick.AddListener(decreaseSail);
            IncreaseSail.onClick.AddListener(increaseSail);
            transform.Rotate(0.0f, CrossPlatformInputManager.GetAxis("Horizontal") * rotateSpeed, 0.0f);
            transform.Translate(vector3 * Time.deltaTime);
        }

        else if (Application.platform == (RuntimePlatform.WindowsPlayer | RuntimePlatform.WindowsEditor))
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
            transform.Translate(vector3 * Time.deltaTime);

            if (Input.GetAxis("Horizontal") < 0)
            {
                animator.enabled = true;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                animator.enabled = true;
            }
            else
            {
                animator.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (currentKnots > 10.0f)
                {
                    currentKnots = 10.0f;
                }
                else
                {
                    currentKnots += 1.5f;
                }
                vector3 = new Vector3(currentKnots, 0.0f, 0.0f);
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (currentKnots < 0.0f)
                {
                    currentKnots = 0.0f;
                }
                else
                {
                    currentKnots -= 1.5f;
                }
                vector3 = new Vector3(currentKnots, 0.0f, 0.0f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        sSL.otherShop = other.GetComponentInChildren<ShopScrollList>();
        //shopCanvas = other.GetComponentInChildren<Canvas>();
        //playerInventoryCanvas.enabled = true;
        //shopCanvas.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        sSL.otherShop = null;
        //playerInventoryCanvas.enabled = false;
    }

    // Android Functions
    public void increaseSail()
    {
        if (currentKnots > 10.0f)
        {
            currentKnots = 10.0f;
        }
        else
        {
            currentKnots += 1.5f;
        }
        vector3 = new Vector3(currentKnots, 0.0f, 0.0f);
    }

    public void decreaseSail()
    {
        if (currentKnots < 0.0f)
        {
            currentKnots = 0.0f;
        }
        else
        {
            currentKnots -= 1.5f;
        }
        vector3 = new Vector3(currentKnots, 0.0f, 0.0f);
    }

    public void stopSail()
    {
        currentKnots = 0.0f;
        vector3 = new Vector3(currentKnots, 0.0f, 0.0f);
    }
}
