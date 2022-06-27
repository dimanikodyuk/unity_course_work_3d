using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smothSpeed;
    [SerializeField] private int _heightCamera;
    [SerializeField] private int _zPosCamera;
    private Vector3 _offset;

    private void Start()
    {
        
    }


    private void Update()
    {
        _offset = new Vector3(0, _heightCamera, _zPosCamera);
        gameObject.transform.position = Vector3.Lerp(new Vector3(0, transform.position.y, transform.position.z), new Vector3(0, _target.transform.position.y, _target.transform.position.z) + _offset, _smothSpeed * Time.deltaTime);
    }
}
