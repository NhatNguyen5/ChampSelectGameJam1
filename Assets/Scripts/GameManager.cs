using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Object.DontDestroyOnLoad example.
//
// This script example manages the playing audio. The GameObject with the
// "music" tag is the BackgroundMusic GameObject. The AudioSource has the
// audio attached to the AudioClip.

public class GameManager : MonoBehaviour
{
    public static event Action<GameState> onGameStateChanged;

    public void UpdateGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        onGameStateChanged?.Invoke(newState);
    }

    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

public enum GameState
{
    Playing,
    Victory,
    Lose,
}