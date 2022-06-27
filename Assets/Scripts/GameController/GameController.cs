using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _audioManager;
    [SerializeField] private GameObject _optionsCanvas;
    [SerializeField] private GameObject _gameMenuCanvas;
    [SerializeField] private GameObject _pausedMenuCanvas;

    [SerializeField] private TMP_Text _soundVolumeValue;
    [SerializeField] private TMP_Text _sfxVolumeValue;
    [SerializeField] private SFXType _buttonClickSFX;

    public static Action OnChangeVolumeSetting;
    private bool _isPaused = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(_audioManager);
        DontDestroyOnLoad(_optionsCanvas);
        DontDestroyOnLoad(_gameMenuCanvas);
        DontDestroyOnLoad(_pausedMenuCanvas);
    }


    private void Start()
    {
        MainMenu.OnGameButtonPress += GameButtonPressed;
        MainMenu.OnOptionsButtonPress += OptionsButtonPressed;
        MainMenu.OnExitButtonPress += ExitButtonPressed;

        Options.OnExitOptionsButton += OptionsExit;
        Options.OnSFXChangeVolume += SFXVolumeSetting;
        Options.OnSoundChangeVolume += SoundVolumeSetting;

        PlayerController.OnPause += PausedGame;

        PauseMenu.OnResume += PausedGame;
        PauseMenu.OnOptions += OptionsButtonPressed;
        PauseMenu.OnMainMenu += MainMenuOpen;
        PauseMenu.OnExit += ExitButtonPressed;

    }

    private void OnDestroy()
    {
        MainMenu.OnGameButtonPress -= GameButtonPressed;
        MainMenu.OnOptionsButtonPress -= OptionsButtonPressed;
        MainMenu.OnExitButtonPress -= ExitButtonPressed;
    }


    // -- MAIN MENU BUTTON -- //

    private void PlayButtonCLickSFX()
    {
        AudioManager.PlaySFX(_buttonClickSFX);
    }

    private void GameButtonPressed()
    {
        PlayButtonCLickSFX();
        SceneManager.LoadScene(1);
    }

    private void OptionsButtonPressed()
    {
        PlayButtonCLickSFX();
        _optionsCanvas.SetActive(true);    
    }

    private void ExitButtonPressed()
    {
        PlayButtonCLickSFX();
        Application.Quit();
    }

    // -- EXIT MAIN MENU BUTTON -- //

    
    // -- OPTION MENU BUTTON -- //

    private void OptionsExit()
    {
        PlayButtonCLickSFX();
        _optionsCanvas.SetActive(false);
    }

    private void ApplyVolumeSetting()
    {
        OnChangeVolumeSetting?.Invoke();
    }


    private void SoundVolumeSetting(float musicVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        _soundVolumeValue.text = (Math.Round(musicVolume, 2) * 100).ToString();
        ApplyVolumeSetting();
    }

    private void SFXVolumeSetting(float sfxVolume)
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        _sfxVolumeValue.text = (Math.Round(sfxVolume, 2) * 100).ToString();
        ApplyVolumeSetting();
    }

    // -- EXIT OPTION MENU BUTTON -- //

    // -- PAUSED MENU -- //
    private void PausedGame()
    {
        PlayButtonCLickSFX();
        _isPaused = !_isPaused;
        if (_isPaused == true)
        {
            _pausedMenuCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _pausedMenuCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void MainMenuOpen()
    {
        PlayButtonCLickSFX();
        SceneManager.LoadScene(0);
    }

   

    // -- EXIT PAUSED MENU -- //


    // -- STORAGE DATA -- //
    IEnumerator LoadLevelCoroutine(int levelNum, int scorePoint, int healthCount)
    {
        SceneManager.LoadScene(levelNum);
        yield return new WaitForSeconds(0.25f);
    }

    private void SaveToPrefs(string data)
    {
        PlayerPrefs.SetString("SaveData", data);
    }

    private void LoadGameStorageData()
    {
        var dataRaw = PlayerPrefs.GetString("SaveData");
        var gameStorageData = JsonUtility.FromJson<GameStorageData>(dataRaw);
        StartCoroutine(LoadLevelCoroutine(gameStorageData.LevelNumber, gameStorageData.ScorePoint, gameStorageData.Health));
    }

    private void PrepateGameStorageData(int scorePoint, int health, int coin, int levelNumber)
    {
        var gameStorageData = GetGameStorageData(scorePoint, health, coin, levelNumber);
        var gameStorageRaw = JsonUtility.ToJson(gameStorageData, true);
        SaveToPrefs(gameStorageRaw);
    }

    public static GameStorageData GetGameStorageData(int scorePoint, int health, int coin, int levelNumver)
    {
        return new GameStorageData()
        {
            ScorePoint = scorePoint,
            Health = health,
            Coins = coin,
            LevelNumber = levelNumver
        };
    }

    // -- EXIT STOTAGE DATA -- //
}


public class GameStorageData
{
    public int ScorePoint;
    public int Health;
    public int Coins;
    public int LevelNumber;
}
