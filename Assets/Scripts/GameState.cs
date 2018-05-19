using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    private static GameState instance;
    public static GameState Instance
    {
        get
        {
            return instance;
        }
    }

    private string currentLevelName;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += ActiveSceneChanged;

        SceneManager.LoadScene("MainMenu");
    }

    private void ActiveSceneChanged(Scene arg0, Scene arg1)
    {
        currentLevelName = arg1.name;
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(string.Format("Level{0:D2}", level));
    }


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(currentLevelName);
    }

    public void StartGame()
    {
        LoadLevel(1);
    }

    public void LevelMenu()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
