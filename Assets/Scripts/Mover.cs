using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public Vector3 destination;
	public float speed= 4f;

	// Use this for initialization
	void Start () {
		//Debug.Log ("mover created");
		//destination = transform.position;
		//transform.position = transform.position + new Vector3 (5f, 3, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 difference = destination - transform.position;
		if (difference.magnitude < speed * Time.deltaTime) {
			transform.position = destination;
			Destroy (this);
		} else {
			transform.position = (transform.position+(difference.normalized*speed * Time.deltaTime));
		}
	}
}
