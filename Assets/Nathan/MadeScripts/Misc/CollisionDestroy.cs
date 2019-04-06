using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroy : MonoBehaviour
{
    private float damage; // how much damage a cannonball does
    // Delete Below Later (Once Nathan is done with the AI code, implment that there
    public UserStatistics userStatistics;

    private void Start()
    {
        damage = 10.5f;
    }

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
            CannonFireAI cannonFireAI = other.GetComponent<CannonFireAI>();
            RespawnBehavior respawnBehavior = other.GetComponent<RespawnBehavior>();
            ShipAi shipAi = other.GetComponent<ShipAi>(); // grabs the ai script
            if (shipAi == null)
            {
                return;
            }
            shipAi.health -= damage; // subtracts damge from the health of the ai
            if (shipAi.health <= 0.0f)
            {
                respawnBehavior.Respawn();
                shipAi.health = 100.0f;
                // Delete Below Later (Once Nathan is done with the AI code, implment that there
                userStatistics.addFood(Random.Range(1, 100));
                userStatistics.addGold(Random.Range(1, 25));
            }
            Debug.Log("AIHealth: " + shipAi.health);
        }
    }

    private void Update()
    {
        Destroy(gameObject, 5);
    }
}
