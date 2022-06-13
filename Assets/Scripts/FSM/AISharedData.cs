using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISharedData : MonoBehaviour
{
    [SerializeField] private VitalitySystem _vitalitySystem;
    [SerializeField] private WeaponSystem _weaponSystem;
    [SerializeField] private NavigationSystem _navigationSystem;
    [SerializeField] private MapHelperSystem _mapHelperSystem;

    public VitalitySystem Vitality => _vitalitySystem;
    public WeaponSystem Weapon => _weaponSystem;
    public NavigationSystem Navigation => _navigationSystem;
    public MapHelperSystem MapHelper => _mapHelperSystem;

}
