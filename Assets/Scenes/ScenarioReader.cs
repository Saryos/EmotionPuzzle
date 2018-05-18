using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ScenarioReader : MonoBehaviour{

	public Transform wallObject;
	public Transform floorObject;
	public Transform humanObject;

	public bool readScenario(string filename, Scenario scenario){
		StreamReader reader = new StreamReader(filename);
		scenario.width = int.Parse(reader.ReadLine());
		scenario.height = int.Parse(reader.ReadLine());
		for (int i = 0; i < scenario.height; i++){
			//do magic

			string line = reader.ReadLine();
			for (int j = 0; j < line.Length; j++) {
				if (line [j] == '.') { // floor
					GameObject.Instantiate(floorObject, new Vector3(i,-0.1f,j), Quaternion.identity);
				}
				if (line [j] == '#') { // wall
					GameObject.Instantiate(wallObject, new Vector3(i,0,j), Quaternion.identity);
					GameObject.Instantiate(floorObject, new Vector3(i,-0.1f,j), Quaternion.identity);
				}
				if (line [j] == 'A') { // angry
					GameObject.Instantiate(humanObject, new Vector3(i,0,j), Quaternion.identity);
					GameObject.Instantiate(floorObject, new Vector3(i,-0.1f,j), Quaternion.identity);
				}
				if (line [j] == 'F') { // fear
					GameObject.Instantiate(humanObject, new Vector3(i,0,j), Quaternion.identity);
					GameObject.Instantiate(floorObject, new Vector3(i,-0.1f,j), Quaternion.identity);
				}
				if (line [j] == 'J') { // joy
					GameObject.Instantiate(humanObject, new Vector3(i,0,j), Quaternion.identity);
					GameObject.Instantiate(floorObject, new Vector3(i,-0.1f,j), Quaternion.identity);
				}
				if (line [j] == 'S') { // sad
					GameObject.Instantiate(humanObject, new Vector3(i,0,j), Quaternion.identity);
					GameObject.Instantiate(floorObject, new Vector3(i,-0.1f,j), Quaternion.identity);
				}
				if (line [j] == 'P') {// player
					GameObject.Instantiate(floorObject, new Vector3(i,-0.1f,j), Quaternion.identity);
					GameObject.Instantiate(humanObject, new Vector3(i,0,j), Quaternion.identity);
				}
				if (line [j] == 'B') {// unfeeling
					GameObject.Instantiate(floorObject, new Vector3(i,-0.1f,j), Quaternion.identity);
					GameObject.Instantiate(humanObject, new Vector3(i,0,j), Quaternion.identity);
				}
			}
		}
		return true;
	}
}
