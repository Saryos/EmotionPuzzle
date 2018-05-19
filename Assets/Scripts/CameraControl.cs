using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float value = Input.GetAxis ("Mouse ScrollWheel");
		Vector3 temp = transform.position;
		Vector3 diff = new Vector3 (0f, value * Time.deltaTime*-80, 0f);
		transform.position = temp + diff;
	}
}
