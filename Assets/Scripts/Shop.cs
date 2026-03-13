using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weaponPrefabs;    
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _template;
    [SerializeField] private GameObject _itemContainer;

    private List<Weapon> _weapons = new();

    private void Start()
    {
        foreach (var weapon in _weaponPrefabs)
        {
            _weapons.Add(weapon.GetComponent<Weapon>());
        }

        for (int i = 0; i < _weaponPrefabs.Count; i++)
        {
            AddItem(_weapons[i]);
        }
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellWeaponClick += OnSellButtonClick;
        view.Render(weapon);
    }

    private void OnSellButtonClick(Weapon weapon, WeaponView weaponView)
    {
        TrySellWeapon(weapon, weaponView);
    }

    private void TrySellWeapon(Weapon weapon, WeaponView weaponView)
    {
        if (weapon.Price <= _player.Money)
        { 
            _player.BuyWeapon(weapon);
            weapon.Buy();
            weaponView.SellWeaponClick -= OnSellButtonClick;
        }
    }
}
