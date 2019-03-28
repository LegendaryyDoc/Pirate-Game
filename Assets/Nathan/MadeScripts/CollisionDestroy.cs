using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroy : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 5);
    }
}
