using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;
    public static MenuManager Instance { get { return instance; } }

    public delegate void SyncingVolumeSlider(float volume);
    public event SyncingVolumeSlider volumeChanged;

    [Header("Menus")]
    public GameObject MainMenu;
    public GameObject PauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        GameManager.UpdateGameState(GameState.MainMenu);
        GameManager.onGameStateChanged += GameManager_onGameStateChanged;
    }

    private void GameManager_onGameStateChanged(GameState obj)
    {
        switch (GameManager.gameState)
        {
            case GameState.MainMenu:
                MainMenu.SetActive(true);
                PauseMenu.SetActive(false);
                break;
            case GameState.Playing:
                MainMenu.SetActive(false);
                PauseMenu.SetActive(false);
                break;
            case GameState.Pause:
                MainMenu.SetActive(false);
                PauseMenu.SetActive(true);
                break;
            default:
                Debug.LogError("Bad gameState");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(volumeChanged != null)
        {
            volumeChanged(GameManager.soundVolume);
        }
    }
}
