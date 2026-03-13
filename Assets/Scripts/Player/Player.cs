using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;    
    [SerializeField] private List<GameObject> _weaponPrefabs;

    private List<GameObject> _visibleWeapons = new();
    private List<Weapon> _weapons = new();

    private Animator _animator;
    private Transform _shootPoint;
    private Weapon _currentWeapon;
    private GameObject _currentWeaponPrefab;
    private GameObject _InstantiateWeaponPrefab;
    private int _currentWeaponNumber = 0;
    private int _currentHealth;
   

    public int Money { get; private set; }
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    void Start()
    {
        _currentWeaponPrefab = _weaponPrefabs[0];

        _InstantiateWeaponPrefab = Instantiate(_currentWeaponPrefab);
        _visibleWeapons.Add(_InstantiateWeaponPrefab);

        _weapons.Add(GetWeapon(_currentWeaponPrefab));
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        _currentWeapon = _weapons[0];
        _shootPoint = GetShootPoint(_currentWeaponPrefab);

        _currentHealth = _health;
        _animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            _currentWeapon.Shoot(_shootPoint);
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnEnemyDied(int reward)
    {
        Money += reward;
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    { 
        Money -= weapon.Price;
        _weapons.Add(weapon);
        MoneyChanged?.Invoke(Money);
    }

    public void PickCurrentWeapon()
    { 
        
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count -1)
            _currentWeaponNumber = 0;
        else 
            _currentWeaponNumber++;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
        ChangeWeaponPrefab(_weaponPrefabs[_currentWeaponNumber]);
    }

    public void PrevWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
        ChangeWeaponPrefab(_weaponPrefabs[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    { 
        _currentWeapon = weapon;
    }

    private void ChangeWeaponPrefab(GameObject weaponPrefab)
    {
        _currentWeaponPrefab = weaponPrefab;
        _shootPoint = GetShootPoint(_currentWeaponPrefab);
    }

    private void DisableAllPrefabs()
    {
        foreach (var prefab in _visibleWeapons)
        {
            prefab.SetActive(false);
        }
    }

    private void EnableCurrentWeaponPrefab(int index)
    {
        _visibleWeapons[index].SetActive(true);
    }    

    private Transform GetShootPoint(GameObject weaponPrefab)
    {
        return weaponPrefab.GetComponentInChildren<ShootPoint>().GetShootPoint();
    }

    private Weapon GetWeapon(GameObject weaponPrefab)
    {
        return weaponPrefab.GetComponent<Weapon>();
    }
}
