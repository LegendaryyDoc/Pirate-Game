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
            ShipBehavior player = other.GetComponent<ShipBehavior>();
            if(player == null)
            {
                return;
            }
            //subtract health by damage
            // still need to add player health
        }
        else if(other.tag == "AI")
        {
            ShipAi shipAi = other.GetComponent<ShipAi>();
            if (shipAi == null)
            {
                return;
            }
            shipAi.health -= damage;
            Debug.Log("Health:" + shipAi.health);
        }
    }

    private void Update()
    {
        Destroy(gameObject, 5);
    }
}
