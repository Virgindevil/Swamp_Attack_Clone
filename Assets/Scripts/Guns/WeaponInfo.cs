using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponInfo 
{
    public Weapon WeaponComponent;
    public GameObject Prefab;
    public bool IsPurchased;

    public WeaponInfo(Weapon weapon, GameObject prefab)
    {
        WeaponComponent = weapon;
        Prefab = prefab;
        IsPurchased = false;
    }
}
