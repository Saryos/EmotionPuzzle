using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour {

	public GameObject wallObject;
	public GameObject floorObject;
	public GameObject humanObject;

	public int width;
	public int height;
	public GameObject player;
	public List<GameObject> Walls = new List<GameObject>();
	public List<GameObject> People = new List<GameObject>();


	GameObject makeObject(GameObject toadd, int i, int j){
		return (GameObject)Instantiate (toadd, new Vector3 (i, 0, j), Quaternion.identity);
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
		player = makeObject (humanObject, i, j);
	}

	public void createFloor(int i, int j){
		GameObject.Instantiate(floorObject, new Vector3(i,0,j), Quaternion.identity);
	}
}
