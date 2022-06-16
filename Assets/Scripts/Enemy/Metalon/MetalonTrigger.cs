using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalonTrigger : MonoBehaviour
{
    private static string _playerTag = "Player";
    public static Action OnGetTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _playerTag)
        {
            OnGetTarget?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == _playerTag)
        {
            OnGetTarget?.Invoke();
        }
    }
}
