using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationSystem : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMesh;
    [SerializeField] private EnemyComponent _player;
    [SerializeField] Animator _anim;

    private static string _playerTag = "Player";

    public bool IsAnyEnemyDetected => _enemies.Count > 0;
    [SerializeField] private List<EnemyComponent> _enemies = new List<EnemyComponent>();

    public bool IsTargetAchieved { get; private set; }
    public bool IsMoveToTarger { get; private set; }

    [SerializeField] private float _distanceThreshold = 0.3f;

    public void MoveToTarger(Transform target)
    {
        _anim.SetBool("run", true);
        _navMesh.SetDestination(target.transform.position);
        Rotation(target.transform.position);
        Debug.DrawLine(this.transform.position, target.transform.position, Color.blue);
    }

        private void Rotation(Vector3 direction)
    {
        //direction.y = 0;
        //var rotation = Quaternion.LookRotation(direction, transform.up);
        //gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.2f);

        var directionNew = (direction - transform.position).normalized;
        directionNew.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(directionNew), 4.2f);
    }

    private void Awake()
    {
        _navMesh.updateRotation = true;
    }

    void Update()
    {
        if (IsAnyEnemyDetected)
        {
            IsMoveToTarger = true;
            MoveToTarger(_player.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _playerTag)
        {
            _enemies.Add(_player);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == _playerTag)
        {
            IsMoveToTarger = false;
            _enemies.Remove(_player);
            
        }
    }
}
