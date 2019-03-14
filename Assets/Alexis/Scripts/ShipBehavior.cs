using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipBehavior : MonoBehaviour {

    private bool isLeftRotationButtonPressed = false;
    private bool isRightRotationButtonPressed = false;

    public Button increaseSail;
    public Button decreaseSail;
    public Button rotateLeft;
    public Button rotateRight;

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
        Button rotateLeftButton = rotateLeft.GetComponent<Button>();
        Button rotateRightButton = rotateRight.GetComponent<Button>();

        increaseSailButton.onClick.AddListener(go);
        decreaseSailButton.onClick.AddListener(stop);
        rotateLeftButton.onClick.AddListener(rotLeft);
        rotateRightButton.onClick.AddListener(rotRight);
    }

    // Update is called once per frame
    void Update()
    {
        
        increaseSail.onClick.AddListener(go);
        increaseSail.onClick.AddListener(stop);
        rotateLeft.onClick.AddListener(rotLeft);
        rotateRight.onClick.AddListener(rotRight);

        //transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0); // changes the rotation of the player

        //vector3 = new Vector3(Input.GetAxis("Vertical"), 0.0f, 0.0f);

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

    public void rotLeft()
    {
        isLeftRotationButtonPressed = true;
        if (isLeftRotationButtonPressed)
        {
            transform.Rotate(0, -rotateSpeed, 0);
        }
        isLeftRotationButtonPressed = false;
    }

    public void rotRight()
    {
        if (isRightRotationButtonPressed)
        {
            transform.Rotate(0, rotateSpeed, 0);
        }
        isRightRotationButtonPressed = false;
    }
}
