using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object.DontDestroyOnLoad example.
//
// This script example manages the playing audio. The GameObject with the
// "music" tag is the BackgroundMusic GameObject. The AudioSource has the
// audio attached to the AudioClip.

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;
    public static event Action<GameState> onGameStateChanged;
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
