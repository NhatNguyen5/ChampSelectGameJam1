using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private Slider volumeSlider;
    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = GameManager.soundVolume;
    }

    private void OnEnable()
    {
        MenuManager.Instance.volumeChanged += Instance_volumeChanged;
    }

    private void OnDisable()
    {
        MenuManager.Instance.volumeChanged -= Instance_volumeChanged;
    }

    public void onVolumeChange()
    {
        GameManager.soundVolume = volumeSlider.value;
        AudioListener.volume = volumeSlider.value;
    }

    private void Instance_volumeChanged(float volume)
    {
        volumeSlider.value = GameManager.soundVolume;
    }
}
