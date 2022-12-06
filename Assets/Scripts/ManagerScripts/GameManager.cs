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
    public static float soundVolume = 1;
    public static GameState gameState;
    public delegate void GameStateDelegate(GameState state);
    public static StarterAssetsInputs SAInputs;
    private StarterAssetsInputs _SAInputs;
    public AudioClip[] BGM;
    public AudioSource BGMSource;
    public Slider MenuBGMSlider; //This is really bad, I'm sorry
    public Slider PauseBGMSlider;

    private void Start()
    {
        if(SAInputs != null)
            _SAInputs = SAInputs;
        gameState = GameState.MainMenu;
        MenuBGMSlider.value = BGMSource.volume;
        PauseBGMSlider.value = BGMSource.volume;
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
                toggleCursor(false);
                Cursor.visible = true;
                Time.timeScale = 0;
                break;
            case GameState.Lose:
                toggleCursor(false);
                Cursor.visible = true;
                Time.timeScale = 0;
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

    public void BGMSliderChange(Slider thisSlider)
    {
        BGMSource.volume = thisSlider.value;
    }

    public void onBGMVolumeChange()
    {
        //if(!MenuBGMSlider.enabled)
            MenuBGMSlider.value = BGMSource.volume;
        //if(!PauseBGMSlider.enabled)
            PauseBGMSlider.value = BGMSource.volume;
    }

    public void BGMSwap(int trackNum)
    {
        BGMSource.clip = BGM[trackNum];
        BGMSource.Play();
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