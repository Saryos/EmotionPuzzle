using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ScenarioReader : MonoBehaviour{

	public Transform wallObject;

	public bool readScenario(string filename, Scenario scenario){
		StreamReader reader = new StreamReader(filename);
		scenario.width = int.Parse(reader.ReadLine());
		scenario.height = int.Parse(reader.ReadLine());
		for (int i = 0; i < scenario.height; i++){
			//do magic

			string line = reader.ReadLine();
			for (int j = 0; j < line.Length; j++) {
				if (line [j] == '#') {
					GameObject.Instantiate(wallObject, new Vector3(i,j,0), Quaternion.identity);
					// add wall
				}
			}
		}
		return true;
	}
}
