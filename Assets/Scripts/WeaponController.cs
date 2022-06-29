using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject _weapon1;
    [SerializeField] private GameObject _weapon2;
    [SerializeField] private GameObject _weapon3;

    public static int demage = 10;
    public static int shootSpeed = 14;

    public static bool woodStaff { get; set; }
    public static bool sorcerersStaff { get; set; }
    public static bool goldStaff { get; set; }

    private void Start()
    {
        CheckWeapon();
    }

    private void Update()
    {

    }

    private void CheckWeapon()
    {
        if (sorcerersStaff == true)
        {
            _weapon1.SetActive(false);
            _weapon2.SetActive(true);
            _weapon3.SetActive(false);

            demage = demage + 5;
            shootSpeed = shootSpeed + 4;


        }
        else if (goldStaff == true)
        {
            _weapon1.SetActive(false);
            _weapon2.SetActive(false);
            _weapon3.SetActive(true);

            demage = demage + 10;
            shootSpeed = shootSpeed + 8;
        }
    }
}
