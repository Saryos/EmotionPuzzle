using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuUiScript : MonoBehaviour {

    private List<Button> levelButtons = new List<Button>();
    private List<GameObject> levelGos = new List<GameObject>();
    public GameObject levelButton;
    public GameObject lockedLevelButton;

    private GameObject canvas;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        int counter = 0;
        foreach (var level in GameState.Instance.AllLevels)
        {
            counter++;
            if (GameState.Instance.UnlockedLevels.Contains(level))
            {
                GameObject newButtonGo = Instantiate(levelButton);
                newButtonGo.transform.position = new Vector3(50 + counter * 75, 100, 0);
                newButtonGo.transform.SetParent(canvas.transform);
                newButtonGo.transform.localScale = new Vector3(1, 1, 1);
                newButtonGo.GetComponent<Button>().onClick.AddListener(() => StartLevel(level));
                newButtonGo.GetComponent<Button>().GetComponentInChildren<Text>().text = level.ToString();
            }
            else
            {
                GameObject newButtonGo = Instantiate(lockedLevelButton);
                newButtonGo.transform.position = new Vector3(50 + counter * 75, 100, 0);
                newButtonGo.transform.SetParent(canvas.transform);
                newButtonGo.transform.localScale = new Vector3(1, 1, 1);
                newButtonGo.GetComponent<Button>().GetComponentInChildren<Text>().text = level.ToString();
            }
        }
    }

    void StartLevel(int level)
    {
        GameState.Instance.LoadLevel(level);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
