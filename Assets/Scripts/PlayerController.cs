using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float turnSpeed;
	Scenario scenario;
	public char emotion = 'B';

	// public for testing
	public int destroys = 0;
	public int pushes = 0;
	public int builds = 0;
	public int shields = 0;

	public void addDestroy(){
		destroys++;
	}
	public void addPush(){
		pushes++;
	}
	public void addBuild(){
		builds++;
	}
	public void addShield(){
		shields++;
	}
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

		int axx = x;
//		int axz = z;
//		int azx = x;
		int azz = z;

		if (moveHorizontal > 0 && transform.position.x > x) {
			//Debug.Log (scenario.isPassable (x + 1, z));
			axx++;
			moveHorizontal *= scenario.isPassable (axx, z);
			scenario.Act (axx,z);
		}
		if (moveHorizontal < 0 && transform.position.x < x) {
			axx--;
			moveHorizontal *= scenario.isPassable (axx, z);
			scenario.Act (axx,z);
		}
		if (moveVertical < 0 && transform.position.z < z) {
			azz--;
			moveVertical *= scenario.isPassable (x , azz);
			scenario.Act (x,azz);
		}
		if (moveVertical > 0 && transform.position.z > z) {
			azz++;
			moveVertical *= scenario.isPassable (x , azz);
			scenario.Act (x,azz);
		}

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		if (movement != Vector3.zero) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed);
		}

		transform.Translate(movement * Time.deltaTime * speed, Space.World);
	}
}
