using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDealer : MonoBehaviour
{
    [SerializeField] private GameObject _fortuneWheel;
    [SerializeField] private GameObject _snockWave;

    private const string _playerTag = "Player";

    private void Wave()
    {
        Instantiate(_snockWave, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _playerTag)
        {
            _fortuneWheel.SetActive(true);
        }
    }

}
