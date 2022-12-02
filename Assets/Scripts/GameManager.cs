using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object.DontDestroyOnLoad example.
//
// This script example manages the playing audio. The GameObject with the
// "music" tag is the BackgroundMusic GameObject. The AudioSource has the
// audio attached to the AudioClip.

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static event Action<GameState> onGameStateChanged; 
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpodateGameState(GameState newState)
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
}

public enum GameState
{
    Victory,
    Lose,
}