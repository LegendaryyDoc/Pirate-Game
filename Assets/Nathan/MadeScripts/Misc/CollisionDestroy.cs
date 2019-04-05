using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroy : MonoBehaviour
{
    public float damage = 10; // how much damage a cannonball does

    private void OnTriggerEnter(Collider other) // when the cannonball collides with something
    {
        if(other.tag == "Player") // if object colliding with is a player
        {
            ShipBehavior player = other.GetComponent<ShipBehavior>(); // gets the player script
            if(player == null)
            {
                return;
            }
            player.shipHealth -= damage; // subtracts damage from the health of the player
            Debug.Log("PlayerHealth: " + player.shipHealth);
        }
        else if(other.tag == "AI") // if object colliding with is an ai
        {
            ShipAi shipAi = other.GetComponent<ShipAi>(); // grabs the ai script
            if (shipAi == null)
            {
                return;
            }
            shipAi.health -= damage; // subtracts damge from the health of the ai
            Debug.Log("AIHealth: " + shipAi.health);
        }
    }

    private void Update()
    {
        Destroy(gameObject, 5);
    }
}
