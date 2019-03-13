using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    void rotateLeft(GameObject gameObject)
    {
        gameObject.transform.Rotate(0, -1, 0); // changes the rotation of the player
    }
}
