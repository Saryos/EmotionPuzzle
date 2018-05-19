using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour {
	
	public Scenario scenario;
	public ScenarioReader myReader;
	public Transform mainCamera;
	// Use this for initialization
	void Start () {
		//scenario = new(Scenario);
		//myReader =  new(ScenarioReader);
		myReader.readScenario ("Assets/Levels/scene1.txt", scenario);
		mainCamera.position = new Vector3 (scenario.width / 2, 10, scenario.height / 2);
		mainCamera.rotation = Quaternion.Euler(90,0,0);
	}

	public void StartGame (string level){
		myReader.readScenario (level, scenario);
		mainCamera.position = new Vector3 (scenario.width / 2, 10, scenario.height / 2);
		mainCamera.rotation = Quaternion.Euler (90, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
