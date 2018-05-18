using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour {
	
	Scenario scenario = new Scenario();
	public ScenarioReader myReader;
	// Use this for initialization
	void Start () {
		//scenario = new(Scenario);
		//myReader =  new(ScenarioReader);
		myReader.readScenario ("scene1.txt", scenario);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
