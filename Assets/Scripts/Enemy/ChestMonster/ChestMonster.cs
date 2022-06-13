using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMonster : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private int _healthCount;
    [SerializeField] private GameObject _diedParticle;
    [SerializeField] private Texture2D _healthTex;
    [SerializeField] private GameObject _coinsPrefab;

    private static string _bulletTag = "Bullet";

    void Update()
    {
        if (_healthCount <= 0)
        {
            StartCoroutine(DiedChest());
        }
    }

    private IEnumerator HitChest()
    {
        _anim.SetBool("hit", true);
        _healthCount=_healthCount-1;
        yield return new WaitForSeconds(0.5f);
        _anim.SetBool("hit", false);
    }


    private IEnumerator DiedChest()
    {
        _anim.SetBool("die", true);
        yield return new WaitForSeconds(1.5f);
        Instantiate(_diedParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            Instantiate(_coinsPrefab, new Vector3(transform.position.x + Random.Range(0, 4), transform.position.y, transform.position.z + Random.Range(0, 4)), Quaternion.identity);
        }
    }

    private void OnGUI()
    {
        if (_healthCount >= 0)
        {
            Vector3 posScr = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z));
            GUI.Box(new Rect(posScr.x - 10, Screen.height - posScr.y - 12, 100, 30), "");
            GUI.DrawTexture(new Rect(posScr.x - 10, Screen.height - posScr.y - 12, 10 * _healthCount, 30), _healthTex);
            //Debug.Log(gameObject.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _bulletTag)
        {
            StartCoroutine(HitChest());
        }

    }

}
