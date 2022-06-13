using System;
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

    [SerializeField] private GameObject _fireBallPrefab;
    [SerializeField] private Transform _firePosition;

    [SerializeField] private int _coinsCount = 0;
    

    private GameObject _nearestObj = null;
    private float _timeBtwShots = 1.0f;
    public static Action OnMagnitedCoins;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyes = GameObject.FindGameObjectsWithTag("Enemy");
        Coins.OnCoinPickup += AddCoins;
        FortuneWheel.OnWinCoins += AddCoins;
    }

    private void OnDestroy()
    {
        Coins.OnCoinPickup -= AddCoins;
        FortuneWheel.OnWinCoins -= AddCoins;
    }

    private void AddCoins(int countCoins)
    {
        _coinsCount += countCoins;
        Debug.Log($"COINT: {_coinsCount}");
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
            if (_nearestObj != null && _anim.GetBool("run") == false)
            {
                var direction = _nearestObj.transform.position - transform.position;
                direction.y = direction.normalized.y;
                Rotation(direction);
                _anim.SetBool("attack", true);
            }

            _anim.SetBool("run", false);
        }

        if (_enemyes.Length == 0)
        {
            OnMagnitedCoins?.Invoke();
        }
    }

    public void Shoot(GameObject bulletPreffab)
    {
        
        //Instantiate(_fireBallPrefab, _firePosition.transform.position, Quaternion.AngleAxis(transform.rotation.y-90.0f, transform.up));
        Instantiate(_fireBallPrefab, _firePosition.transform.position, transform.rotation);
        //Instantiate(_fireBallPrefab, _firePosition.transform.position, Quaternion.AngleAxis(transform.rotation.y+90.0f, transform.up));
        
        _anim.SetBool("attack", false);
    }

    public void StepSFX()
    {
        AudioManager.PlaySFX(_stepSFX);
    }

    public void GetNearestEnemy()
    {
        try
        {
            var nearestDist = float.MaxValue;
            foreach (var enemy in _enemyes)
            {
                if (Vector3.Distance(this.transform.position, enemy.transform.position) < nearestDist)
                {
                    nearestDist = Vector3.Distance(this.transform.position, enemy.transform.position);
                    _nearestObj = enemy;
                    Debug.DrawLine(this.transform.position, _nearestObj.transform.position, Color.red);
                }
            }
        }

        catch
        {
            _enemyes = GameObject.FindGameObjectsWithTag("Enemy");
        }
         
    }

    public void Rotation(Vector3 direction)
    {
        direction.y = 0;
        var rotation = Quaternion.LookRotation(direction, transform.up);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
    }
    //public void Rotation(Vector3 direction)
    //{
    //    direction.y = 0;
    //    Vector3 targetForward = Vector3.RotateTowards(transform.forward, direction.normalized, _rotationSpeed * Time.deltaTime, .1f);
    //    Quaternion newRotate = Quaternion.LookRotation(targetForward);
    //    transform.rotation = newRotate;
    //}
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
            
            transform.rotation = Quaternion.LookRotation(new Vector3(_joystickDetector.Direction.x, 0.0f, _joystickDetector.Direction.y));
        }
            //transform.rotation = Quaternion.LookRotation(_rigid.velocity);
        
    }
}
