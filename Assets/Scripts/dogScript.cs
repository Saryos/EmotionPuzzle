using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogScript : MonoBehaviour {

    private GameObject player;

    public float speed;
    public float turnSpeed;

    public float huntDistance = 5;
    private float timer = 0.0f;

    private bool playerFound;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            tryFindHuman();
        }

        if (playerFound)
        {
            hunt();
        }
    }

    private void hunt()
    {
        Debug.Log("HUNTING PLAYER");
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    private void tryFindHuman()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < huntDistance)
        {
            playerFound = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            // KILL PLAYER
            Debug.Log("Attacking Player");
        }
    }
}
