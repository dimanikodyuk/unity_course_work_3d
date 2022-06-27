using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private JoystickDetector _joystickDetector;
    [SerializeField] private float _speed = 12f; // Швидкість
    [SerializeField] private Transform _targer;  // 
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _anim;     // Аніматор
    [SerializeField] private Rigidbody _rigid;   

    [SerializeField] private SFXType _stepSFX;   // Звук кроків

    [SerializeField] private GameObject[] _enemyes; // Противники
    [SerializeField] private float _rotationSpeed;  // Швидкість повороту

    [SerializeField] private GameObject _runParticle;

    [SerializeField] private GameObject _fireBallPrefab;
    [SerializeField] private Transform _firePosition;
    [SerializeField] private Transform _firePositionL;
    [SerializeField] private Transform _firePositionR;

    [SerializeField] private int _coinsCount = 0;

    [SerializeField] private GameObject _weapon1;
    [SerializeField] private GameObject _weapon2;
    [SerializeField] private GameObject _weapon3;

    [SerializeField] private TMP_Text _coinsValueText;
    [SerializeField] private Button _pauseButton;

    // -- HEALTH && EXP BAR -- //
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Slider _sliderHealth;
    [SerializeField] private GameObject _healthBarUI;
    [SerializeField] private TMP_Text _playerHealthText;

    [SerializeField] private float _exp;
    [SerializeField] private float _maxEXP;
    [SerializeField] private Slider _sliderEXP;
    [SerializeField] private TMP_Text _playerEXPText;

    
    
    // -- END HEALTH && EXP BAR -- //

    private GameObject _nearestObj = null;
    private float _timeBtwShots = 1.0f;
    private bool _isDead = false;
    public static Action OnMagnitedCoins;
    public static Action OnPause;
    public bool _isTripleShoot;
    public static int _angleTripleShoot = 90;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyes = GameObject.FindGameObjectsWithTag("Enemy");
        FortuneWheel.OnWinCoins += CoinsChange;
        Metalon.OnDamaged += TakeDamage;
        Metalon.OnDead += TakeEXP;
        _pauseButton.onClick.AddListener(Paused);
        CheckWeapon();

        _health = _maxHealth;
        _sliderHealth.value = CalculateHealth();
        _playerHealthText.text = _health.ToString();
    }

    private void OnDestroy()
    {
        //Coins.OnCoinPickup -= CoinsChange;
        FortuneWheel.OnWinCoins -= CoinsChange;
        Metalon.OnDamaged -= TakeDamage;
        Metalon.OnDead -= TakeEXP;
    }

    private void Paused()
    {
        OnPause?.Invoke();
    }

    private float CalculateHealth()
    {
        return _health / _maxHealth;
    }

    private float CalculateExp()
    {
        return _exp / _maxEXP;
    }

    private void HealthCheck()
    {
        float angle = 0f;
        Vector3 axis;
        transform.rotation.ToAngleAxis(out angle, out axis);
        Debug.Log(angle);
        _healthBarUI.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));


        _sliderHealth.value = CalculateHealth();
        _playerHealthText.text = _health.ToString();
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
            //StartCoroutine(DiedChest());
            Destroy(gameObject);
        }
    }

    private void ExpCheck()
    {
        _sliderEXP.value = CalculateExp();
        _playerEXPText.text = _exp.ToString();
        if (_exp > _maxEXP)
        {
            _exp = _maxEXP;
        }
    }

    private void CheckWeapon()
    {
        if (WeaponController.weapon2 == true)
        {
            _weapon1.SetActive(false);
            _weapon2.SetActive(true);
            _weapon3.SetActive(false);


        }
        else if (WeaponController.weapon3 == true)
        {
            _weapon1.SetActive(false);
            _weapon2.SetActive(false);
            _weapon3.SetActive(true);
        }

    }


    // Update is called once per frame
    void Update()
    {
        GetNearestEnemy();
        HealthCheck();
        ExpCheck();

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


        if (_health <= 0)
        {
            _isDead = true;
            _anim.SetBool("die", true);
        }
    }

    private void TakeDamage(int damage)
    {
        _anim.SetBool("hit", true);
        _health -= damage;
        Debug.Log($"HEALTH: {_health}");
    }

    private void TakeEXP(int exp)
    {
        _exp = _exp + exp;
        _playerEXPText.text = _exp.ToString();
    }

    private void CoinsChange(int coins)
    {
        Debug.Log($"+{coins}");
        _coinsValueText.text = (_coinsCount + coins).ToString();
    }

    public void RunParticle()
    {
        Instantiate(_runParticle, new Vector3(transform.position.x, -0.05f, transform.position.z), Quaternion.identity);
    }

    public void Shoot(GameObject bulletPreffab)
    {
        _anim.SetBool("run", false);
        if (_isTripleShoot == true)
        {
            float angle = 0f;
            Vector3 axis;
            transform.rotation.ToAngleAxis(out angle, out axis);
            Instantiate(_fireBallPrefab, _firePositionL.transform.position, Quaternion.AngleAxis(angle - _angleTripleShoot, axis));
            Instantiate(_fireBallPrefab, _firePosition.transform.position, transform.rotation);
            Instantiate(_fireBallPrefab, _firePositionR.transform.position, Quaternion.AngleAxis(angle + _angleTripleShoot, axis));
            _anim.SetBool("hit", false);
            _anim.SetBool("attack", false);
        }
        else
        {
            Instantiate(_fireBallPrefab, _firePosition.transform.position, transform.rotation);
            _anim.SetBool("hit", false);
            _anim.SetBool("attack", false);
        }
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
        _anim.SetBool("attack", false);
        direction.y = 0;
        var rotation = Quaternion.LookRotation(direction, transform.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
    }

    private void Movement()
    {
        _anim.SetBool("attack", false);
        _anim.SetBool("run", true);
        _anim.SetBool("hit", false);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            _coinsCount = _coinsCount + UnityEngine.Random.Range(10, 100);
            _coinsValueText.text = _coinsCount.ToString();
        }
    }
}
