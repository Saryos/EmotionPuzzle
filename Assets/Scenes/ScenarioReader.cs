using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ScenarioReader : MonoBehaviour{

	public GameObject wallObject;
	public GameObject floorObject;
	public GameObject humanObject;

	GameObject makeObject(GameObject toadd, int i, int j){
		return (GameObject)Instantiate (toadd, new Vector3 (i, 0, j), Quaternion.identity);
	}

	public bool readScenario(string filename, Scenario scenario){
		StreamReader reader = new StreamReader(filename);
		scenario.width = int.Parse(reader.ReadLine());
		scenario.height = int.Parse(reader.ReadLine());
		for (int i = 0; i < scenario.height; i++){
			//do magic

			string line = reader.ReadLine();
			for (int j = 0; j < line.Length; j++) {
				GameObject toadda = floorObject; // floor exists by default
				switch (line [j]) {
				case('.'):
					// just floor
					break;
				case('#'):
					GameObject newWall = makeObject (wallObject, i, j);
					scenario.Walls.Add (newWall);
					break;
				case('P'):
					scenario.player = makeObject (humanObject, i, j);
					break;
				case('A'):
				case('F'):
				case('J'):
				case('S'):
				case('B'):
					scenario.People.Add(makeObject(humanObject,i,j));
					break;
				default:
					// should not be here...
					break;
				}
					
				if (toadda) {
					GameObject.Instantiate(toadda, new Vector3(i,0,j), Quaternion.identity);
				}
	
			}
		}
		return true;
	}

}
