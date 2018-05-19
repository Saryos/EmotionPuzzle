using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float turnSpeed;
	Scenario scenario;

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
		}
		if (moveHorizontal < 0 && transform.position.x < x) {
			moveHorizontal *= scenario.isPassable (x - 1, z);
		}
		if (moveVertical < 0 && transform.position.z < z) {
			moveVertical *= scenario.isPassable (x , z-1);
		}
		if (moveVertical > 0 && transform.position.z > z) {
			moveVertical *= scenario.isPassable (x , z+1);
		}

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		if (movement != Vector3.zero) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed);
		}

		transform.Translate(movement * Time.deltaTime * speed, Space.World);
	}
}
