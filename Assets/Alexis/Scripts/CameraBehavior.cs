using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    private Vector3 offset;
    public GameObject target;

    // Use this for initialization
    void Start () {
        offset = transform.position - target.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Q))
        {
            Rotate90Left();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Rotate90Right();
        }
        transform.position = target.transform.position + offset;
    }

    void Rotate90Left()
    {
        transform.Rotate(0.0f, 90.0f * Time.deltaTime, 0.0f);
    }

    void Rotate90Right()
    {
        transform.Rotate(0.0f, -90.0f * Time.deltaTime, 0.0f);
    }
}
