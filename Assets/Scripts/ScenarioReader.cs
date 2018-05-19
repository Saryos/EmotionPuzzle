using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ScenarioReader : MonoBehaviour{

	public bool readScenario(string filename, Scenario scenario){
		StreamReader reader = new StreamReader(filename);
		scenario.width = int.Parse(reader.ReadLine());
		scenario.height = int.Parse(reader.ReadLine());
		for (int i = 0; i < scenario.height; i++){
			//do magic

			string line = reader.ReadLine();
			for (int j = 0; j < line.Length; j++) {
				bool toadda = true; // floor exists by default
				switch (line [j]) {
				case('.'):
					// just floor
					break;
				case('#'):
					scenario.createWall(i, j);
					break;
				case('P'):
					scenario.createPlayer(i, j);
					break;
				case('A'):
				case('F'):
				case('J'):
				case('S'):
				case('B'):
					scenario.createPeople (i, j, 'B');
					break;
				default:
					// should not be here...
					break;
				}
					
				if (toadda) {
					scenario.createFloor (i, j);
				}
	
			}
		}
		return true;
	}

}
