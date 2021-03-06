using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    
    [SerializeField] private float _speed;
    [SerializeField] private bool _isMagnet;
    
    private GameObject _player;
    //private int _coinsWeigh;

    //public static Action<int> OnCoinPickup;

    void Start()
    {
        PlayerController.OnMagnitedCoins += ChangeMagnet;
        _player = GameObject.FindGameObjectWithTag("Player");
        //_coinsWeigh = UnityEngine.Random.Range(40, 400);
    }

    private void PickupCoins()
    {
        //OnCoinPickup?.Invoke(_coinsWeigh);
    }

    private void OnDestroy()
    {
        PlayerController.OnMagnitedCoins -= ChangeMagnet;
    }

    
    private void Update()
    {
        if (_isMagnet == true)
        {
            var dir = _player.transform.position - transform.position;
            var new_dir = dir.normalized;
            CoinMagnet(new_dir);
        }
    }

    private void ChangeMagnet()
    {
        _isMagnet = !_isMagnet;
    }

    private void CoinMagnet(Vector3 direction)
    {
        transform.position += direction * _speed * Time.deltaTime;

        if ((_player.transform.position - gameObject.transform.position).sqrMagnitude < 0.01f)
        {
            //PickupCoins();
            Destroy(gameObject);
        }
    }
}
