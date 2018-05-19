﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    private int currentLevelNumber = 0;

    public List<int> AllLevels { get; set; }

    public List<string> AllLevelPaths;
    public List<int> UnlockedLevels = new List<int> { 1 };

    private MasterScript master;
    private AudioSource audiosource;
    private AudioListener audiolistener;
    public AudioClip music;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        audiolistener = new AudioListener();
        audiosource = GetComponent<AudioSource>();
        audiosource.Play();

        AllLevels = new List<int>();
        AllLevelPaths = new List<string>();
        int counter = 0;
        while (true)
        {
            counter++;
            string path = string.Format("Assets/Levels/scene{0}.txt", counter);
            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    AllLevelPaths.Add(path);
                    AllLevels.Add(counter);
                }
            }
            catch (FileNotFoundException e)
            {
                break;
            }
        }
        foreach (var item in AllLevelPaths)
        {
            Debug.Log(item);

        }


        SceneManager.activeSceneChanged += ActiveSceneChanged;

        SceneManager.LoadScene("MainMenuScene");
    }

    private void ActiveSceneChanged(Scene arg0, Scene arg1)
    {
        currentLevelName = arg1.name;

        if (currentLevelName == "Game")
        {
            master = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<MasterScript>();
            master.StartGame(AllLevelPaths[currentLevelNumber - 1]);
        }
    }

    public void LoadLevel(int level)
    {
        if (!AllLevels.Contains(level))
        {
            Debug.Log(level);
            Debug.Log("No such level exist");
        }
        else if (UnlockedLevels.Contains(level))
        {
            currentLevelNumber = level;
            SceneManager.LoadScene("Game");
        }
        else
        {
            Debug.Log("Level is not unlocked yet");
        }
    }


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            ReturnPressed();
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(currentLevelName);
        if (Input.GetKeyDown(KeyCode.Home))
            UnlockedLevels = AllLevels;
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
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ReturnPressed()
    {
        if (currentLevelName == "MainMenuScene")
        {
            Debug.Log("Exiting game..");
            ExitGame();
        }
        else
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
