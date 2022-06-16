using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] private int _healthCount;
    [SerializeField] private Texture2D _healthTex;
    [SerializeField] private GameObject _diedParticle;
    [SerializeField] private GameObject _coinsPrefab;
    
    
    private int iCount = 0;
    private bool _getTarget = false;
    private static string _playerTag = "Player";
    public static Action<int> OnDamaged;


    private void Start()
    {
        _navMesh.updateRotation = false;
        MetalonTrigger.OnGetTarget += CheckTarget;
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

        if (_healthCount <= 0)
        {
            StartCoroutine(DiedChest());
        }

        //if (_navMesh.remainingDistance <= 4 && _getTarget)
        //{
        //    _navMesh.isStopped = true;
        //    _anim.SetBool("run", false);
        //    _anim.SetBool("attack", true);
        //}
        //else
        //{
        //    _navMesh.isStopped = false;
        //    _anim.SetBool("attack", false);
        //}
    }

    public void Damaged(int damage)
    {
        OnDamaged?.Invoke(damage);
    }

    private void OnGUI()
    {
        if (_healthCount >= 0)
        {
            Vector3 posScr = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z));
            GUI.Box(new Rect(posScr.x - 10, Screen.height - posScr.y - 12, 50, 30), "");
            GUI.DrawTexture(new Rect(posScr.x - 10, Screen.height - posScr.y - 12, 10 * _healthCount, 30), _healthTex);
        }
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
        _healthCount--;
        yield return new WaitForSeconds(0.5f);
        _anim.SetBool("hit", false);
    }

    private IEnumerator DiedChest()
    {
        _anim.SetBool("die", true);
        yield return new WaitForSeconds(1.5f);
        Instantiate(_diedParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
        for (int i = 0; i < UnityEngine.Random.Range(5, 10); i++)
        {
            Instantiate(_coinsPrefab, new Vector3(transform.position.x + UnityEngine.Random.Range(0, 4), transform.position.y, transform.position.z + UnityEngine.Random.Range(0, 4)), Quaternion.identity);
        }
    }


}
