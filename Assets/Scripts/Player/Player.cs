using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<GameObject> _weaponPrefabs;

    private List<WeaponInfo> _weapons = new(); 
    private Transform _shootPoint;

    public int Money { get; private set; }
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    private Weapon _currentWeapon;
    private GameObject _currentWeaponInstance; 
    private WeaponInfo _currentWeaponInfo;
    private int _currentWeaponNumber = 0;
    private int _currentHealth;
    private Animator _animator;

    void Start()
    {     
        if (_weaponPrefabs.Count > 0)
        {
            var firstPrefab = _weaponPrefabs[0];
            var weaponComponent = firstPrefab.GetComponent<Weapon>();
            var weaponInfo = new WeaponInfo(weaponComponent, firstPrefab);

            weaponInfo.IsPurchased = true;
            
            _weapons.Add(weaponInfo);
            EquipWeapon(weaponInfo);
        }

        _currentHealth = _health;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _currentWeapon != null && Time.timeScale != 0)
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
            Destroy(_currentWeaponInstance);
        }
    }

    private Transform GetShootPoint(GameObject weaponPrefab)
    {
        return weaponPrefab.GetComponentInChildren<ShootPoint>().GetShootPoint();
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

    public void BuyWeapon(WeaponInfo weaponInfo)
    {
        Money -= weaponInfo.WeaponComponent.Price;
        _weapons.Add(weaponInfo);
        weaponInfo.IsPurchased = true;
        MoneyChanged?.Invoke(Money);
    }

    public void NextWeapon()
    {
        if (_weapons.Count == 0) return;

        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        EquipWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PrevWeapon()
    {
        if (_weapons.Count == 0) return;

        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        EquipWeapon(_weapons[_currentWeaponNumber]);
    }

    private void EquipWeapon(WeaponInfo weaponInfo)
    {
        if (_currentWeaponInstance != null)
        {
            Destroy(_currentWeaponInstance);
        }

        _currentWeaponInfo = weaponInfo;

        _currentWeaponInstance = Instantiate(weaponInfo.Prefab);
        _currentWeapon = _currentWeaponInstance.GetComponent<Weapon>();

        _shootPoint = GetShootPoint(weaponInfo.Prefab);
    }
}