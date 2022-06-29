using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    [SerializeField] private GameObject _portal;
    [SerializeField] private SFXType _portalSFX;

    private int _currentScene;

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene().buildIndex;
        FortuneWheel.OnOpenPortal += OpenPortal;
        ChestMonster.OnPortalOpen += OpenPortal;
    }

    private void OnDestroy()
    {
        FortuneWheel.OnOpenPortal -= OpenPortal;
        ChestMonster.OnPortalOpen -= OpenPortal;
    }

    private void OpenPortal()
    {
        _portal.SetActive(true);
        AudioManager.PlaySFX(_portalSFX);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(_currentScene + 1);
        }
    }
}
