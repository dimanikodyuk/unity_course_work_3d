using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _gameButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _exitButton;

    public static Action OnGameButtonPress;
    public static Action OnShopButtonPress;
    public static Action OnOptionsButtonPress;
    public static Action OnExitButtonPress;

    void Start()
    {
        _gameButton.onClick.AddListener(GameButton);
        _shopButton.onClick.AddListener(ShopButton);
        _optionsButton.onClick.AddListener(OptionsButton);
        _exitButton.onClick.AddListener(ExitButton);
    }

    private void GameButton()
    {
        OnGameButtonPress?.Invoke();
    }
    private void ShopButton()
    {
        OnShopButtonPress?.Invoke();
    }

    private void OptionsButton()
    {
        OnOptionsButtonPress?.Invoke();
    }

    private void ExitButton()
    {
        OnExitButtonPress?.Invoke();
    }
}
