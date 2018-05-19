using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour {

	public GameObject permaWallObject;
	public GameObject weakWallObject;
	public GameObject floorObject;
	public GameObject humanObject;
	public GameObject playerObject;
	public GameObject goalObject;
	public GameObject bridgeObject;

	public int width;
	public int height;
	public GameObject player;
	// Should all objects be on the same list?
	public List<GameObject> Walls = new List<GameObject>(); // Normal objects
	public List<GameObject> People = new List<GameObject>(); // possible actors
	public List<GameObject> Floors = new List<GameObject>(); // floor level objects


	GameObject makeObject(GameObject toadd, int i, int j){
		return (GameObject)Instantiate (toadd, new Vector3 (i, 0, j), Quaternion.identity);
	}

	private bool isInSquare(GameObject item, int i, int j){
		int x = Mathf.RoundToInt (item.transform.position.x);
		int z = Mathf.RoundToInt (item.transform.position.z);

		if (i == x && j == z) {
//			Debug.Log (i);
//			Debug.Log (x);
			return true;
		}
		return false;
	}

	public int Destroy(int i, int j){
		for(int k=0;k<Walls.Count;k++){
			GameObject item = Walls [k];
			if (isInSquare (item, i, j)) {
				if (item.GetComponent<Cake> ()) {
					Debug.Log ("You grabbed the cake, you naughty cake grabber!");
				}
				Walls.Remove(item);
				Destroy(item);
				return 1;
			}
		}
		return 0;
	}

	public int isPassable(int i, int j){
		foreach(GameObject item in Walls){
			if (isInSquare (item, i, j)) {
				//item.GetComponent<Renderer>().material.color = new Color (1, 0, 0);
				return 0;
			}
		}
		foreach(GameObject item in People){
			if (isInSquare (item, i, j)) {
				return 0;
			}
		}
		return 1;
	}

	public void createPermaWall(int i, int j){
		GameObject newWall = makeObject (permaWallObject, i, j);
		Walls.Add (newWall);
	}

	public void createWeakWall(int i, int j){
		GameObject newWall = makeObject (weakWallObject, i, j);
		Walls.Add (newWall);
	}

	public void createPeople(int i, int j, char mood){
		GameObject newHuman = makeObject (humanObject, i, j);
		Human temp = newHuman.GetComponent<Human>();
		temp.mood=mood;
		People.Add(newHuman);
	}

	public void createGoal(int i, int j){
		GameObject newWall = makeObject (goalObject, i, j);
		Walls.Add (newWall);
	}

	public void createPlayer(int i, int j){
		player = makeObject (playerObject, i, j);
		(player.GetComponent (typeof(PlayerController)) as PlayerController).setScenario(this);
	}

	public void createFloor(int i, int j){
		Floors.Add(GameObject.Instantiate(floorObject, new Vector3(i,-0.5f,j), Quaternion.identity));
	}
}
