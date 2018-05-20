using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {

	public float psize=0.2f;
	public float speed;
	public float turnSpeed;
	Scenario scenario;
	public char emotion = 'B';

	// public for testing
	public int destroys = 0;
	public int pushes = 0;
	public int builds = 0;
	public int shields = 0;

    private GameObject animatedGo;
    private GameObject idleGo;

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
        animatedGo = transform.Find("Charru_Animated").gameObject;
        idleGo = transform.Find("Charru2.2Joined").gameObject;
        animatedGo.SetActive(false);
    }

    void Update() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		int x = Mathf.RoundToInt (transform.position.x);
		int z = Mathf.RoundToInt (transform.position.z);

        if (moveHorizontal > 0.1f || moveVertical > 0.1f || moveHorizontal < -0.1f || moveVertical < -0.1f)
        {
            animatedGo.SetActive(true);
            idleGo.SetActive(false);
        }
        else
        {
            animatedGo.SetActive(false);
            idleGo.SetActive(true);
        }

        int axx = x;
//		int axz = z;
//		int azx = x;
		int azz = z;

		if (moveHorizontal > 0 && transform.position.x-psize > x) {
			//Debug.Log (scenario.isPassable (x + 1, z));
			axx++;
			moveHorizontal *= scenario.isPassable (axx, z);
			scenario.Act (axx,z,'E');
		}
		if (moveHorizontal < 0 && transform.position.x+psize < x) {
			axx--;
			moveHorizontal *= scenario.isPassable (axx, z);
			scenario.Act (axx,z,'W');
		}
		if (moveVertical < 0 && transform.position.z+psize < z) {
			azz--;
			moveVertical *= scenario.isPassable (x , azz);
			scenario.Act (x,azz,'S');
		}
		if (moveVertical > 0 && transform.position.z-psize > z) {
			azz++;
			moveVertical *= scenario.isPassable (x , azz);
			scenario.Act (x,azz,'N');
		}
        
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        /*
		if (movement != Vector3.zero) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed);
		}*/
        
		transform.Translate(movement * Time.deltaTime * speed, Space.World);

        float step2 = speed * Time.deltaTime * 10;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, movement, step2, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
