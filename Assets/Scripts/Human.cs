using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {

	public char mood;

    public float speed;
    public float turnSpeed;

    public float attractionDistance = 5;
    private float timer = 0.0f;

    private GameObject attractiveHuman;
    private bool attracted = false;

    public Material basicMat;
    public Material sadMat;
    public Material fearMat;
    public Material joyMat;
    public Material angryMat;

	Scenario scenario;

	// must be called at initialization
	public void setScenario(Scenario s){
		scenario = s;
	}

    void Start()
    {
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            checkAttractiveHumans();
        }

        if (attracted)
        {
            moveToAttraction();
        }


        changeColor();

    }

    private void changeColor()
    {
        switch (mood)
        {
            case ('S'):
                this.gameObject.GetComponentInChildren<MeshRenderer>().material = sadMat;
                break;
            case ('F'):
                this.gameObject.GetComponentInChildren<MeshRenderer>().material = fearMat;
                break;
            case ('A'):
                this.gameObject.GetComponentInChildren<MeshRenderer>().material = angryMat;
                break;
            case ('J'):
                this.gameObject.GetComponentInChildren<MeshRenderer>().material = joyMat;
                break;
            case ('B'):
                this.gameObject.GetComponentInChildren<MeshRenderer>().material = basicMat;
                break;
        }
    }

    private void moveToAttraction()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, attractiveHuman.transform.position, step);
        /*
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeed);
        }*/

        //transform.Translate(movement * Time.deltaTime * speed, Space.World);

    }

    private void checkAttractiveHumans()
    {
		foreach (var human in scenario.People)
        {
            if(human != this.gameObject)
            {
                if (match(human.GetComponent<Human>().mood))
                {
                    if (Vector3.Distance(human.gameObject.transform.position, transform.position) < attractionDistance)
                    {
                        attractiveHuman = human;
                        attracted = true;
                        break;
                    }
                }               
            }
            attracted = false;
        }
    }

    private bool match(char humanMood)
    {
        if ((mood == 'S' && humanMood == 'A') ||
            (mood == 'S' && humanMood == 'F') ||
            (mood == 'A' && humanMood == 'J') ||
            (mood == 'F' && humanMood == 'J') ||
            (humanMood == 'S' && mood == 'A') ||
            (humanMood == 'S' && mood == 'F') ||
            (humanMood == 'A' && mood == 'J') ||
            (humanMood == 'F' && mood == 'J'))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == attractiveHuman)
        {
            attracted = false;
            mood = 'B';
        }
    }
}
