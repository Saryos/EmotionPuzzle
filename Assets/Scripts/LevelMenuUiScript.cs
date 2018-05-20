using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuUiScript : MonoBehaviour {

    private List<Button> levelButtons = new List<Button>();
    private List<GameObject> levelGos = new List<GameObject>();
    public GameObject levelButton;
    public GameObject lockedLevelButton;

    public GameObject placement;

    private GameObject canvas;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        placement = transform.Find("Placement").gameObject;
        Vector3 pos = placement.transform.position;
        int counter = 0;
        foreach (var level in GameState.Instance.AllLevels)
        {
            counter++;
            if (GameState.Instance.UnlockedLevels.Contains(level))
            {
                GameObject newButtonGo = Instantiate(levelButton);
                newButtonGo.transform.position = new Vector3(pos.x + counter * 75, pos.y, 0);
                newButtonGo.transform.SetParent(canvas.transform);
                newButtonGo.transform.localScale = new Vector3(1, 1, 1);
                newButtonGo.GetComponent<Button>().onClick.AddListener(() => StartLevel(level));
                newButtonGo.GetComponent<Button>().GetComponentInChildren<Text>().text = level.ToString();
            }
            else
            {
                GameObject newButtonGo = Instantiate(lockedLevelButton);
                newButtonGo.transform.position = new Vector3(pos.x + counter * 75, pos.y, 0);
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
