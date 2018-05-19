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
					toadda = false;
					// chasm
					break;
				case('_'):
					// just floor
					break;
				case('#'):
					scenario.createPermaWall(i, j);
					break;
				case('I'):
					scenario.createWeakWall(i,j);
					break;
				case('P'):
					scenario.createPlayer(i, j);
					break;
				case('G'):
					scenario.createGoal (i, j);
					break;
				case('A'):
                    scenario.createPeople (i, j, line[j]);
                    break;
                case ('F'):
                    scenario.createPeople(i, j, line[j]);
                    break;
                case ('J'):
                    scenario.createPeople (i, j, line[j]);
                    break;
                case ('S'):
                    scenario.createPeople (i, j, line[j]);
                    break;
                case ('B'):
					scenario.createPeople (i, j, line[j]);
					break;
				case('D'):
					scenario.createDog (i, j);
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
