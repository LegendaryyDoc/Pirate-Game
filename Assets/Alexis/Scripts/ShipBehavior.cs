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

    private float health;
    private float respawnTimer = 0.0f;
    private float rotateSpeed = 0.5f;
    private RespawnBehavior respawnBehavior;
    private Transform tfCam;

    public Animator animator;
    public Animator fadeTransitions;
    [HideInInspector]
    private Animator shipAnimator;
    [HideInInspector]
    public float shipHealth;
    public ShopScrollList sSL;
    public UserStatistics userStatistics;

    static float currentKnots = 0.0f;
    static Vector3 vector3 = new Vector3();

    // Android Controls
    public Button DecreaseSail;
    public Button IncreaseSail;

    void Start()
    {
        shipHealth = userStatistics.shipHealth;
        DecreaseSail.onClick.AddListener(decreaseSail);
        IncreaseSail.onClick.AddListener(increaseSail);

        animator.enabled = false;
        fadeTransitions.enabled = true;
        fadeTransitions.SetBool("isObjectResetting", false);

        respawnBehavior = this.GetComponent<RespawnBehavior>();
        respawnBehavior.Respawn();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        tfCam = Camera.main.transform;
    }

    void FixedUpdate()
    {
        Debug.Log("Current Knots: " + currentKnots);
        health = userStatistics.health;
        userStatistics.shipHealth = shipHealth;

        if (health <= 0 | shipHealth <= 0)
        {
            //shipAnimator.Play("isShipDestroyed");
            fadeTransitions.SetBool("isObjectResetting", true);
            respawnBehavior.Respawn();
            userStatistics.health = 100.0f;
            userStatistics.shipHealth = 100.0f;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            transform.Rotate(0.0f, CrossPlatformInputManager.GetAxis("Horizontal") * rotateSpeed, 0.0f);
            transform.Translate(vector3 * Time.deltaTime);

            if (CrossPlatformInputManager.GetAxis("Horizontal") != 0)
            {
                animator.enabled = true;
            }
            else
            {
                animator.enabled = false;
            }
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
                currentKnots += 1.5f;

                if (currentKnots > 10.0f)
                {
                    currentKnots = 10.0f;
                }
                vector3 = new Vector3(currentKnots, 0.0f, 0.0f);
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                currentKnots -= 1.5f;

                if (currentKnots < 0.0f)
                {
                    currentKnots = 0.0f;
                }
                vector3 = new Vector3(currentKnots, 0.0f, 0.0f);
            }
        }
        respawn();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Port")
        {
            sSL.otherShop = other.GetComponentInChildren<ShopScrollList>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Port")
        {
            sSL.otherShop = null;
        }
    }

    // Ship Functions
    private void respawn()
    {
        respawnTimer += Time.deltaTime;
        if (transform.position.y < -5.0f | transform.position.y > 8.5f)
        {
            fadeTransitions.SetBool("isObjectResetting", true);
            respawnTimer = 0.0f;
            Mathf.RoundToInt(sSL.gold /= 2);
            respawnBehavior.Respawn();
            currentKnots = 0.0f;
            vector3 = new Vector3(currentKnots, 2.5f, 0.0f);
        }
        if (respawnTimer > 1.5f)
        {
            fadeTransitions.SetBool("isObjectResetting", false);
        }
    }

    // Android Functions
    public void increaseSail()
    {
        currentKnots += 1.5f;

        if (currentKnots > 10.0f)
        {
            currentKnots = 10.0f;
        }
        vector3 = new Vector3(currentKnots, 0.0f, 0.0f);
    }

    public void decreaseSail()
    {
        currentKnots -= 1.5f;

        if (currentKnots < 0.0f)
        {
            currentKnots = 0.0f;
        }
        vector3 = new Vector3(currentKnots, 0.0f, 0.0f);
    }

    public void stopSail()
    {
        currentKnots = 0.0f;
        vector3 = new Vector3(currentKnots, 0.0f, 0.0f);
    }
}
