using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroy : MonoBehaviour
{
    public float damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<ShipBehavior>();
        }
        else if(other.tag == "AI")
        {
            other.GetComponent<ShipAi>().health -= damage;
        }
    }

    private void Update()
    {
        Destroy(gameObject, 5);
    }
}
