using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Metalon : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private NavMeshAgent _navMesh;
    [SerializeField] private GameObject _player;
    [SerializeField] private List<Transform> _enemySearchPoint;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private GameObject _diedParticle;
    [SerializeField] private GameObject _coinsPrefab;

    // -- HEALTH BAR -- //
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;

    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _healthBarUI;
    [SerializeField] private TMP_Text _enemyHealthText;
    // -- END HEALTH BAR -- //
    
    private int iCount = 0;
    private bool _getTarget = false;
    private static int _exp = 10;
    private static string _playerTag = "Player";
    public static Action<int> OnDamaged;
    public static Action<int> OnDead;

    

    private void Start()
    {
        _navMesh.updateRotation = false;
        MetalonTrigger.OnGetTarget += CheckTarget;

        _health = _maxHealth;
        _slider.value = CalculateHealth();
        _enemyHealthText.text = _health.ToString();
    }

    private void OnDestroy()
    {
        MetalonTrigger.OnGetTarget -= CheckTarget;
    }

    private void CheckTarget()
    {
        _getTarget = !_getTarget;
        _enemies.Add(_player);

        if (_getTarget == false)
        {
            _anim.SetBool("run", false);
        }
    }

    private float CalculateHealth()
    {
        return _health / _maxHealth;
    }


    private void Update()
    {
        if (_getTarget && _anim.GetBool("attack") == false)
        {
            _anim.SetBool("run", true);
            _navMesh.destination = _player.transform.position;
            Rotation(_player.transform.position);
        }
        else
        {
            _anim.SetBool("run", true);
            _navMesh.destination = NextEnemySearchPoint();
            Rotation(_navMesh.destination);
        }

        var heading = _player.transform.position - gameObject.transform.position;
        var distance = heading.magnitude;
        if (distance < 2.5f)
        {
            Rotation(_player.transform.position);
            _navMesh.isStopped = true;
            _anim.SetBool("run", false);
            _anim.SetBool("attack", true);
        }
        else
        {
            _navMesh.isStopped = false;
            _anim.SetBool("attack", false);
        }


        HealthCheck();


    }

    private void HealthCheck()
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

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }

        if (_health <= 0)
        {
            StartCoroutine(DiedChest());
        }
    }

    public void Damaged(int damage)
    {
        OnDamaged?.Invoke(damage);
    }

    private void Rotation(Vector3 direction)
    {
        var directionNew = (direction - transform.position).normalized;
        directionNew.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(directionNew), _rotateSpeed);
    }

    public Vector3 NextEnemySearchPoint()
    {
        if (_navMesh.remainingDistance < 1)
        {
            iCount++;
            if (iCount == _enemySearchPoint.Count)
            {
                iCount = 0;
            }
        }
        return _enemySearchPoint[iCount].transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(HitMetalon());
        }
    }

    private IEnumerator HitMetalon()
    {
        _anim.SetBool("hit", true);
        _health = _health - WeaponController.demage;
        yield return new WaitForSeconds(0.5f);
        _anim.SetBool("hit", false);
    }

    private IEnumerator DiedChest()
    {
        OnDead?.Invoke(_exp);
        _healthBarUI.SetActive(false);
        _anim.SetBool("die", true);
        yield return new WaitForSeconds(1.5f);
        Instantiate(_diedParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        for (int i = 0; i < UnityEngine.Random.Range(5, 10); i++)
        {
            Instantiate(_coinsPrefab, new Vector3(transform.position.x + UnityEngine.Random.Range(0, 4), transform.position.y, transform.position.z + UnityEngine.Random.Range(0, 4)), Quaternion.AngleAxis(UnityEngine.Random.Range(0, 180), transform.up));
        }
    }


}
