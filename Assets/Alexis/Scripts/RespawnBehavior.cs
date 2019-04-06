using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBehavior : MonoBehaviour {

    public Vector3[] respawnPoints;

    public void Respawn()
    {
        int random = Random.Range(0, respawnPoints.Length);
        this.gameObject.transform.position = respawnPoints[random];
    }
}
