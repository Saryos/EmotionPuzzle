using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ScenarioReader : MonoBehaviour{

	public bool readScenario(string filename, Scenario scenario, float creationDelay=-1){
		StreamReader reader = new StreamReader(filename);
		scenario.width = int.Parse(reader.ReadLine());
		scenario.height = int.Parse(reader.ReadLine());
		if (creationDelay < 0) {
			creationDelay = 1.0f / (scenario.width * scenario.height);
			//Debug.Log (creationDelay);
		}
		if (creationDelay != 0) {
			Debug.Log ("delay read");
			StartCoroutine (DelayedRead (reader, scenario, creationDelay));
		} else {
			for (int i = 0; i < scenario.height; i++) {
				//do magic

				string line = reader.ReadLine ();
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
						scenario.createPermaWall (i, j);
						break;
					case('I'):
						scenario.createWeakWall (i, j);
						break;
					case('P'):
						scenario.createPlayer (i, j);
						break;
					case('G'):
						scenario.createGoal (i, j);
						break;
					case('A'):
						scenario.createPeople (i, j, line [j]);
						break;
					case ('F'):
						scenario.createPeople (i, j, line [j]);
						break;
					case ('J'):
						scenario.createPeople (i, j, line [j]);
						break;
					case ('S'):
						scenario.createPeople (i, j, line [j]);
						break;
					case ('B'):
						scenario.createPeople (i, j, line [j]);
						break;
					case('D'):
						scenario.createDog (i, j);
						break;
					case('~'):
						scenario.createWater (i, j);
						toadda = false;
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
		}
		return true;			
	}

	IEnumerator DelayedRead(StreamReader reader, Scenario scenario, float creationDelay){
		for (int i = 0; i < scenario.height; i++){
			//do magic
			//Debug.Log ("delay read");

			string line = reader.ReadLine();
			for (int j = 0; j < line.Length; j++) {
				yield return new WaitForSeconds(creationDelay);
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
				case('~'):
					scenario.createWater (i, j);
					toadda = false;
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
	}

}
