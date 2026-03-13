using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _sprite;

    [SerializeField] protected Projectile _projectile;
    [SerializeField] private Transform _shootPoint;


    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _sprite;

    public abstract void Shoot(Transform shootPoint);

}
