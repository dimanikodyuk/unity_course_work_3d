using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblin : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMesh;
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _anim;
    [SerializeField] private float _rotationSpeed = 2.0f;
    [SerializeField] private List<Transform> _enemySearchPoint;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();

    private int iCount = 0; // current search enemy point

    private bool _getTarger = false;
    private const string _playerTag = "Player";

    private void Awake()
    {
        _navMesh.updateRotation = true;
    }

    void Update()
    {
        ObjectMove();
        
    }

    private void ObjectMove()
    {
        if (_getTarger && _anim.GetBool("attack") == false)
        {
            _anim.SetBool("run", true);
            //Rotation(_player.transform.position);
            _navMesh.destination = _player.transform.position;
            //Debug.Log($"[_navMesh.remainingDistance]: {_navMesh.remainingDistance}");
        }
        else
        {
            _anim.SetBool("run", true);
            _navMesh.destination = NextEnemySearchPoint();
            //Rotation(_navMesh.destination);
        }
        
        if (_navMesh.remainingDistance <= 4 && _getTarger)
        {
            _navMesh.isStopped = true;
            _anim.SetBool("run", false);
            _anim.SetBool("attack", true);
        }
        else
        {
            _navMesh.isStopped = false;
            _anim.SetBool("attack", false);
        }
    }

    //private void SetPosition(Vector3 position)
    //{
    //    _anim.SetBool("run", true);
    //    if (_navMesh.remainingDistance > 1)
    //    {
    //        _navMesh.isStopped = false;
    //        _navMesh.stoppingDistance = 1.0f;
    //        Rotation(position);
    //        _navMesh.SetDestination(position);
    //    }
    //    else
    //    {
    //        _navMesh.isStopped = true;
    //    }

    //}

    //private void Rotation(Vector3 direction)
    //{
    //    var directionNew = (direction - transform.position).normalized;
    //    directionNew.y = 0f;
    //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(directionNew), 1.2f);
    //}

    //private void Rotation(Vector3 direction)
    //{
    //    var rotation = Quaternion.LookRotation(direction, transform.up);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1);
    //}

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _playerTag)
        {
            _getTarger = true;
            _enemies.Add(_player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == _playerTag)
        {
            _getTarger = false;
            _anim.SetBool("run", false);
        }
    }
}
