using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _exitButton;

    public static Action OnResume;
    public static Action OnOptions;
    public static Action OnMainMenu;
    public static Action OnExit;

    void Start()
    {
        _resumeButton.onClick.AddListener(Resume);
        _optionsButton.onClick.AddListener(Options);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _exitButton.onClick.AddListener(Exit);
    }

    private void Resume()
    {
        OnResume?.Invoke();
    }

    private void Options()
    {
        OnOptions?.Invoke();
    }

    private void MainMenu()
    {
        OnMainMenu?.Invoke();
    }

    private void Exit()
    {
        OnExit?.Invoke();
    }

}
