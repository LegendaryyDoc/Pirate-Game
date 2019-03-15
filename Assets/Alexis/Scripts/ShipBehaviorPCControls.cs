using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviorPCControls : MonoBehaviour
{

    public float speed = 5.0f;
    public float rotateSpeed = 0.5f;

    Rigidbody rigidbody = new Rigidbody();
    Vector3 vector3 = new Vector3();

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0); 
        vector3 = new Vector3(Input.GetAxis("Vertical"), 0.0f, 0.0f);
        transform.Translate(vector3 * speed * Time.deltaTime);
    }
}