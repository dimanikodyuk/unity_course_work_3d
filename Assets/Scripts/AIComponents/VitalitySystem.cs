using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalitySystem : MonoBehaviour
{
    public bool IsDead => _healthPoint <= 0;
    [SerializeField] private int _healthPoint = 100;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
