using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour {

	public GameObject wallObject;
	public GameObject floorObject;
	public GameObject humanObject;
	public GameObject playerObject;

	public int width;
	public int height;
	public GameObject player;
	// Should all objects be on the same list?
	public List<GameObject> Walls = new List<GameObject>();
	public List<GameObject> People = new List<GameObject>();


	GameObject makeObject(GameObject toadd, int i, int j){
		return (GameObject)Instantiate (toadd, new Vector3 (i, 0, j), Quaternion.identity);
	}

	private bool isInSquare(GameObject item, int i, int j){
		int x = Mathf.RoundToInt (item.transform.position.x);
		int z = Mathf.RoundToInt (item.transform.position.z);
		if (i == x && j == z) {
			return true;
		}
		return false;
	}

	public bool Destroy(int i, int j){
		foreach(GameObject item in Walls){
			if (isInSquare (item, i, j)) {
				Destroy (item);
				return true;
			}
		}
		return true;
	}

	public bool isPassable(int i, int j){
		foreach(GameObject item in Walls){
			if (isInSquare (item, i, j)) {
				return false;
			}
		}
		foreach(GameObject item in People){
			if (isInSquare (item, i, j)) {
				return false;
			}
		}
		return true;
	}

	public void createWall(int i, int j){
		GameObject newWall = makeObject (wallObject, i, j);
		Walls.Add (newWall);
	}

	public void createPeople(int i, int j, char mood){
		GameObject newHuman = makeObject (humanObject, i, j);
		Human temp = newHuman.GetComponent<Human>();
		temp.mood='B';
		People.Add(newHuman);
	}

	public void createPlayer(int i, int j){
		player = makeObject (playerObject, i, j);
	}

	public void createFloor(int i, int j){
		GameObject.Instantiate(floorObject, new Vector3(i,0,j), Quaternion.identity);
	}
}
