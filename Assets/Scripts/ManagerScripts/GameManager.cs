using JetBrains.Annotations;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static event Action<GameState> onGameStateChanged;
    public static float soundVolume = 100;
    public static GameState gameState;
    public delegate void GameStateDelegate(GameState state);
    public static StarterAssetsInputs SAInputs;
    private StarterAssetsInputs _SAInputs;

    private void Start()
    {
        if(SAInputs != null)
            _SAInputs = SAInputs;
        gameState = GameState.MainMenu;
    }

    public static void UpdateGameState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                toggleCursor(false);
                Cursor.visible = true;
                break;
            case GameState.Pause:
                toggleCursor(false);
                Cursor.visible = true;
                Time.timeScale = 0;
                break;
            case GameState.Playing:
                toggleCursor(true);
                Cursor.visible = false;
                Time.timeScale = 1;
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        onGameStateChanged?.Invoke(newState);
    }

    private static void toggleCursor(bool toggle)
    {
        if (SAInputs != null)
        {
            SAInputs.cursorInputForLook = toggle;
            SAInputs.cursorLocked = toggle;
        }
    }

    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        AudioListener.volume = soundVolume;
    }

    public void ResetGameState()
    {
        UpdateGameState(GameState.MainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

public enum GameState
{
    MainMenu,
    Pause,
    Playing,
    Victory,
    Lose,
}