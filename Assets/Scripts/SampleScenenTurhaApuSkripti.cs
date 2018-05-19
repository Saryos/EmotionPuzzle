using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScenenTurhaApuSkripti : MonoBehaviour {

	public MasterScript master;
	// Use this for initialization
	void Start () {
		master.StartGame ("Assets/Levels/scene1.txt");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
