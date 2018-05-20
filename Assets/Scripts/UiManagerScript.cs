using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManagerScript : MonoBehaviour {

    private GameObject levelCompletedGo;
    private Text levelCompletedText;
    private Text pushesText;
    private Text bonesText;
    private Text breaksText;
    private Text bridgesText;

    public int pushes = 0;
    public int bones = 0;
    public int breaks = 0;
    public int bridges = 0;

    private bool playerFound = false;
    private PlayerController player;

    // Use this for initialization
    void Start () {
        levelCompletedGo = GameObject.Find("LevelCompleted");
        levelCompletedText = GameObject.Find("LevelCompleted").GetComponent<Text>();

        pushesText = GameObject.Find("PushesText").GetComponent<Text>();
        bonesText = GameObject.Find("BonesText").GetComponent<Text>();
        breaksText = GameObject.Find("BreaksText").GetComponent<Text>();
        bridgesText = GameObject.Find("BridgesText").GetComponent<Text>();

        levelCompletedGo.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerFound = true;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        if (playerFound)
        {
            pushes = player.pushes;
            bones = player.shields;
            bridges = player.builds;
            breaks = player.destroys;
            pushesText.text = string.Format("Pushes: {0}", pushes);
            bonesText.text = string.Format("Bones: {0}", bones);
            breaksText.text = string.Format("Breaks: {0}", breaks);
            bridgesText.text = string.Format("Bridges: {0}", bridges);
        }
    }
}
