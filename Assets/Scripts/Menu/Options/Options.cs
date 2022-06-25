using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private Button _backOptionsButton;
    [SerializeField] private Slider _sound;
    [SerializeField] private TMP_Text _soundText;
    [SerializeField] private Slider _sfx;
    [SerializeField] private TMP_Text _sfxText;

    public static Action OnExitOptionsButton;

    public static Action<float> OnSoundChangeVolume;
    public static Action<float> OnSFXChangeVolume;

    // Start is called before the first frame update
    void Start()
    {
        _sound.value = PlayerPrefs.GetFloat("MusicVolume");
        _sfx.value = PlayerPrefs.GetFloat("SFXVolume");
        _soundText.text = (Math.Round(_sound.value, 2) * 100).ToString();
        _sfxText.text = (Math.Round(_sfx.value, 2) * 100).ToString();

        _backOptionsButton.onClick.AddListener(ExitOptions);   
        _sfx.onValueChanged.AddListener(SFXVolumeChanged);
        _sound.onValueChanged.AddListener(SoundVolumeChanged);
    }


    private void ExitOptions()
    {
        OnExitOptionsButton?.Invoke();
    }

    private void SoundVolumeChanged(float musicVolume)
    {
        OnSoundChangeVolume?.Invoke(musicVolume);
    }

    private void SFXVolumeChanged(float sfxVolume)
    {
        OnSFXChangeVolume?.Invoke(sfxVolume);
    }
}
