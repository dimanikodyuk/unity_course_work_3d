using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;

    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _healthBarUI;
    [SerializeField] private TMP_Text _enemyHealthText;   

    void Start()
    {
        _health = _maxHealth;
        _slider.value = CalculateHealth();
        _enemyHealthText.text = _health.ToString();
    }

    private float CalculateHealth()
    {
        return _health / _maxHealth;
    }


    void Update()
    {
        float angle = 0f;
        Vector3 axis;
        transform.rotation.ToAngleAxis(out angle, out axis);
        Debug.Log(angle);
        _healthBarUI.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));


        _slider.value = CalculateHealth();
        _enemyHealthText.text = _health.ToString();
        if (_health < _maxHealth)
        {
            _healthBarUI.SetActive(true);
        }

        if (_health <= 0)
        {
            Destroy(gameObject);
        }

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

}
