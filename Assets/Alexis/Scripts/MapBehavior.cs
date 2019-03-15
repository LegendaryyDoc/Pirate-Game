using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBehavior : MonoBehaviour {
    private ShipBehaviorAndroidControls androidControls;
    private ShipBehaviorPCControls pcControls;
    public Canvas canvas;
    public GameObject gameObject;

	// Use this for initialization
	void Start () {
        androidControls = gameObject.GetComponent<ShipBehaviorAndroidControls>();
        canvas.enabled = false;
        pcControls = gameObject.GetComponent<ShipBehaviorPCControls>();

        if (Application.platform == (RuntimePlatform.WindowsPlayer | RuntimePlatform.WindowsEditor))
        {
            pcControls.enabled = true;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            androidControls.enabled = true;
            canvas.enabled = true;
        }
	}
}
