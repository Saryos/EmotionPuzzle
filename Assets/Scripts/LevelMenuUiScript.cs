using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuUiScript : MonoBehaviour {

    private List<Button> levelButtons = new List<Button>();
    private List<GameObject> levelGos = new List<GameObject>();
    public GameObject levelButton;

    private GameObject canvas;

    void Start()
    {

        canvas = GameObject.FindGameObjectWithTag("Canvas");
        foreach (var level in GameState.Instance.AllLevels)
        {
            GameObject newButtonGo = Instantiate(levelButton);
            newButtonGo.transform.position = new Vector3(250 + level * 75, 250, 0);
            newButtonGo.transform.SetParent(canvas.transform);
            newButtonGo.transform.localScale = new Vector3(1, 1, 1);
            newButtonGo.GetComponent<Button>().onClick.AddListener(() => StartLevel(level));
            newButtonGo.GetComponent<Button>().GetComponentInChildren<Text>().text = level.ToString();

        }
    }

    void StartLevel(int level)
    {
        GameState.Instance.LoadLevel(level);
        //Here i want to know which button was Clicked.
        //or how to pass a param through addListener
    }

    // Update is called once per frame
    void Update () {
		
	}
}
