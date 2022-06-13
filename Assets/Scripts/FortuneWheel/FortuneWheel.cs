using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheel : MonoBehaviour
{
    [SerializeField] private int _numberOfTurns; 
    [SerializeField] private int _whatWeWin;
    [SerializeField] private float _speedWheel;
    [SerializeField] private float _smoothSpeedWheel;
    [SerializeField] private Button _turnWheel;
    [SerializeField] private GameObject _canvasWheel;
    [SerializeField] private GameObject _dealer;
    [SerializeField] TMP_Text winningText;
    
    private bool _turned;
    private int _coinsCount;
    public static Action<int> OnWinCoins;
    

    // Start is called before the first frame update
    void Start()
    {
        _turned = false;
        _turnWheel.onClick.AddListener(Turn);
        
    }

    private void Turn()
    {
        _turnWheel.gameObject.SetActive(false);
        StartCoroutine(TurnWheel());
    }


    private IEnumerator TurnWheel()
    {
        _turned = true;
        _numberOfTurns = UnityEngine.Random.Range(8, 32);
        //Debug.Log(_numberOfTurns);
        _speedWheel = 0.1f;
        for (int i = 0; i < _numberOfTurns; i++)
        {
            transform.Rotate(0, 0, 22.5f);

            if (i > Mathf.RoundToInt(_numberOfTurns * 0.5f))
            {
                _speedWheel = 0.1f;
            }

            if (i > Mathf.RoundToInt(_numberOfTurns * 0.95f))
            {
                _speedWheel = 0.2f;
            }

            if (i > Mathf.RoundToInt(_numberOfTurns * 0.0f))
            {
                _speedWheel = 0.3f;
            }

            yield return new WaitForSeconds(_speedWheel);
        }

        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0)
        {
            transform.Rotate(0, 0, 22.5f);
        }

        _whatWeWin = Mathf.RoundToInt(transform.eulerAngles.z);

        switch (_whatWeWin)
        {
            case 0:
                winningText.text = "YOU HAVE WON: 1200";
                _coinsCount = 1200;
                break;
            case 45:
                winningText.text = "YOU HAVE WON: 900";
                _coinsCount = 900;
                break;
            case 90:
                winningText.text = "YOU HAVE WON: 900";
                _coinsCount = 900;
                break;
            case 135:
                winningText.text = "YOU HAVE WON: 1800";
                _coinsCount = 1800;
                break;
            case 180:
                winningText.text = "YOU HAVE WON: 900";
                _coinsCount = 900;
                break;
            case 225:
                winningText.text = "YOU HAVE WON: 1200";
                _coinsCount = 1200;
                break;
            case 270:
                winningText.text = "YOU HAVE WON: 1200";
                _coinsCount = 1200;
                break;
            case 315:
                winningText.text = "YOU HAVE WON: 900";
                _coinsCount = 900;
                break;
        }

        OnWinCoins(_coinsCount);

        _turned = false;
        yield return new WaitForSeconds(1);
        Destroy(_canvasWheel);
        Destroy(_dealer);
        
    }
}
