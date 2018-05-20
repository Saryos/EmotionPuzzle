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

	public bool moveUnlocked = false;

	Scenario scenario;

    private enum pointToBeAdded { BUILD, BREAK, DOG, PUSH, NONE}
    private pointToBeAdded combinationPoint;

    private GameObject animatedGo;
    private GameObject idleGo;

	// must be called at initialization
	public void setScenario(Scenario s){
		scenario = s;
	}

    void Start()
    {
        animatedGo = transform.Find("Charru_Animated").gameObject;
        idleGo = transform.Find("Charru2.2Joined").gameObject;
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
            animatedGo.SetActive(true);
            idleGo.SetActive(false);
        }
        else
        {
            animatedGo.SetActive(false);
            idleGo.SetActive(true);
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
        
        Vector3 startPos = transform.position;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, attractiveHuman.transform.position, step);

        Vector3 targetDir = transform.position - startPos;
        // The step size is equal to speed times frame time.
        float step2 = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step2, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
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
		combinationPoint = pointToBeAdded.NONE;
        if (mood == 'S' && humanMood == 'A' )
        {
            combinationPoint = pointToBeAdded.BREAK;
            return true;
        }
        else if (mood == 'S' && humanMood == 'F')
        {
            combinationPoint = pointToBeAdded.DOG;
            return true;
        }
        else if (mood == 'J' && humanMood == 'F')
        {
            combinationPoint = pointToBeAdded.BUILD;
            return true;
        }
        else if (mood == 'J' && humanMood == 'A')
        {
            combinationPoint = pointToBeAdded.PUSH;
            return true;
		}
		else if (mood == 'A' && humanMood == 'S' )
		{
			return true;
		}
		else if (mood == 'F' && humanMood == 'S')
		{
			return true;
		}
		else if (mood == 'F' && humanMood == 'J')
		{
			return true;
		}
		else if (mood == 'A' && humanMood == 'J')
		{
			return true;
		}
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == attractiveHuman)
        {
            switch (combinationPoint)
            {
                case pointToBeAdded.BREAK:
                    scenario.player.addDestroy();
                    break;
                case pointToBeAdded.BUILD:
                    scenario.player.addBuild();
                    break;
                case pointToBeAdded.DOG:
					scenario.player.addShield();
                    break;
                case pointToBeAdded.PUSH:
                    scenario.player.addPush();
                    break;
            }
            attracted = false;
            mood = 'B';
            
        }

    }
}
