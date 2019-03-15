using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ShipBehaviorAndroidControls : MonoBehaviour
{
    public Button IncreaseSail;
    public Button DecreaseSail;

    public float rotateSpeed = 0.5f;
    public float speed = 5.0f;

    new Rigidbody rigidbody = new Rigidbody();
    static Vector3 vector3 = new Vector3();

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        Button increaseSailButton = IncreaseSail.GetComponent<Button>();
        Button decreaseSailButton = DecreaseSail.GetComponent<Button>();

        increaseSailButton.onClick.AddListener(increaseSail);
        decreaseSailButton.onClick.AddListener(stopSail);
    }

    void Update()
    {
        transform.Rotate(0.0f, CrossPlatformInputManager.GetAxis("Horizontal") * rotateSpeed, 0.0f);
        IncreaseSail.onClick.AddListener(increaseSail);
        DecreaseSail.onClick.AddListener(stopSail);
        transform.Translate(vector3 * speed * Time.deltaTime);
    }
    
    public void increaseSail() { vector3 = new Vector3(5.0f, 0.0f, 0.0f); }

    public void decreaseSail() { vector3 = new Vector3(-5.0f, 0.0f, 0.0f); }

    public void stopSail() { vector3 = new Vector3(0.0f, 0.0f, 0.0f); }
}
