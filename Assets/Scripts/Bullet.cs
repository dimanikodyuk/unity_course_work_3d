using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsSolid;
    [SerializeField] GameObject _destroyEffect;

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _distance;

    [SerializeField] private Rigidbody _rb;

    private void Start()
    {
        Invoke(nameof(DestroyBullet), _lifeTime);
    }

    private void Update()
    {
        _rb.velocity = gameObject.transform.forward * _bulletSpeed;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
