using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] private int _health;

    public Enemy myEnemy = new Enemy(1, 0.2f, 0.4f);

    public void GetDamage(int damageValue)
    {
        _health -= damageValue;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Move()
    {
        
    }

    public void Start()
    {
        Debug.Log(myEnemy.healthCount);
    }

}
