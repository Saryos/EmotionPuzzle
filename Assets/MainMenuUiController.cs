using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUiController : MonoBehaviour {

    private Button StartGameButton;
    private Button LevelSelectionButton;
    private Button ExitButton;

    private GameState gameState;

	// Use this for initialization
	void Start () {
        gameState = GameObject.Find("GameMaster").GetComponent<GameState>();

        StartGameButton = GameObject.Find("StartButton").GetComponent<Button>();
        LevelSelectionButton = GameObject.Find("LevelSelectionButton").GetComponent<Button>();
        ExitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        StartGameButton.onClick.AddListener(gameState.StartGame);
        LevelSelectionButton.onClick.AddListener(gameState.LevelMenu);
        ExitButton.onClick.AddListener(gameState.ExitGame);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
