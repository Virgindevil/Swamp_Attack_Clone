using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public override void Shoot(Transform shootPoint)
    {
        Instantiate(_projectile,shootPoint.position,Quaternion.identity);
    }
}
