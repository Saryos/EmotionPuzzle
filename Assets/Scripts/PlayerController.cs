using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float turnSpeed;
	Scenario scenario;
	char emotion;

	// public for testing
	public int destroys = 0;
	int runs = 0;
	int builds = 0;
	int shields = 0;

	// must be called at initialization
	public void setScenario(Scenario s){
		scenario = s;
	}

	void Start() {

	}

	void Update() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		int x = Mathf.RoundToInt (transform.position.x);
		int z = Mathf.RoundToInt (transform.position.z);


		if (moveHorizontal > 0 && transform.position.x > x) {
			//Debug.Log (scenario.isPassable (x + 1, z));
			moveHorizontal *= scenario.isPassable (x + 1, z);
			if (destroys>0) {
				destroys -= scenario.Destroy (x + 1, z);
			}
		}
		if (moveHorizontal < 0 && transform.position.x < x) {
			moveHorizontal *= scenario.isPassable (x - 1, z);
			if (destroys>0) {
				destroys -= scenario.Destroy (x -1, z);
			}
		}
		if (moveVertical < 0 && transform.position.z < z) {
			moveVertical *= scenario.isPassable (x , z-1);
			if (destroys>0) {
				destroys -= scenario.Destroy (x , z-1);
			}
		}
		if (moveVertical > 0 && transform.position.z > z) {
			moveVertical *= scenario.isPassable (x , z+1);
			if (destroys>0) {
				destroys -= scenario.Destroy (x , z+1);
			}
		}

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		if (movement != Vector3.zero) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed);
		}

		transform.Translate(movement * Time.deltaTime * speed, Space.World);
	}
}
