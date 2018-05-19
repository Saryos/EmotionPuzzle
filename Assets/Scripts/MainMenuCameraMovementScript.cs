using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraMovementScript : MonoBehaviour {

    float angle;

    // Use this for initialization
    void Start () {
        angle = transform.rotation.eulerAngles.y;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, Time.deltaTime * 2f, 0);
    }
}
