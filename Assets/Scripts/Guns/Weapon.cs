using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private bool _isBuy;

    [SerializeField] protected Projectile _projectile;
    [SerializeField] private Transform _shootPoint;


    public abstract void Shoot(Transform shootPoint);
}
