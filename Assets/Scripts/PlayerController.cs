using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private JoystickDetector _joystickDetector;
    [SerializeField] private float _speed = 12f; // Швидкість
    [SerializeField] private Transform _targer;  // 
    [SerializeField] private Animator _anim;     // Аніматор
    [SerializeField] private Rigidbody _rigid;   

    [SerializeField] private SFXType _stepSFX;   // Звук кроків

    [SerializeField] private GameObject[] _enemyes; // Противники
    [SerializeField] private float _rotationSpeed;  // Швидкість повороту

    private GameObject _nearestObj = null;

    // Start is called before the first frame update
    void Start()
    {
        _enemyes = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

        GetNearestEnemy();

        if (_joystickDetector.IsMoved)
        {
            Movement();
        }
        else
        {
            if (_nearestObj != null)
            {
                var direction = _nearestObj.transform.position - transform.position;
                direction = direction.normalized;
                Rotation(direction);
            }
            _anim.SetBool("run", false);
        }
    }

    public void StepSFX()
    {
        AudioManager.PlaySFX(_stepSFX);
    }


    public void GetNearestEnemy()
    {
        var nearestDist = float.MaxValue;

        foreach (var enemy in _enemyes)
        {
            if (Vector3.Distance(this.transform.position, enemy.transform.position) < nearestDist)
            {
                nearestDist = Vector3.Distance(this.transform.position, enemy.transform.position);
                _nearestObj = enemy;
            }
        }
        Debug.DrawLine(this.transform.position, _nearestObj.transform.position, Color.red);
    }

    public void Rotation(Vector3 direction)
    {
        var rotation = Quaternion.LookRotation(direction, transform.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
    }

    private void Movement()
    {
        _anim.SetBool("run", true);
        _targer.position += new Vector3(_joystickDetector.Direction.x, 0f, _joystickDetector.Direction.y) * _speed * Time.deltaTime;
        if (_joystickDetector.Direction.x != 0 || _joystickDetector.Direction.y != 0)
        {
            var x = Mathf.Abs(_joystickDetector.Direction.x);
            var y = Mathf.Abs(_joystickDetector.Direction.y);

            if (x > 0f &&  x > y)
            {
                _anim.SetFloat("speed", Mathf.Abs(_joystickDetector.Direction.x));
            }
            else if (y > 0f && y > x)
            {
                _anim.SetFloat("speed", Mathf.Abs(_joystickDetector.Direction.y));
            }
            
            transform.rotation = Quaternion.LookRotation(new Vector3(_joystickDetector.Direction.x, 0f, _joystickDetector.Direction.y));
        }
            //transform.rotation = Quaternion.LookRotation(_rigid.velocity);
        
    }
}
