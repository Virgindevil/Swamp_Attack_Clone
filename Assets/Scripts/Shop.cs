using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weaponPrefabs;
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _template;
    [SerializeField] private GameObject _itemContainer;

    private List<WeaponInfo> _weapons = new List<WeaponInfo>();

    private void Start()
    {
        foreach (var prefab in _weaponPrefabs)
        {
            var weaponComponent = prefab.GetComponent<Weapon>();
            var weaponInfo = new WeaponInfo(weaponComponent, prefab);
            _weapons.Add(weaponInfo);
        }

        foreach (var weaponInfo in _weapons)
        {
            AddItem(weaponInfo);
        }
    }

    private void AddItem(WeaponInfo weaponInfo)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellWeaponClick += OnSellButtonClick;
        view.Render(weaponInfo.WeaponComponent);
    }

    private void OnSellButtonClick(Weapon weapon, WeaponView weaponView)
    {
        TrySellWeapon(weapon, weaponView);
    }

    private void TrySellWeapon(Weapon weapon, WeaponView weaponView)
    {
       //if (weaponInfo.IsPurchased) 
        //      return;

        if (weapon.Price <= _player.Money)
        {
            var weaponInfo = _weapons.Find(w => w.WeaponComponent == weapon);

            if (weaponInfo != null)
            {
                _player.BuyWeapon(weaponInfo);
                weapon.IsPurchased == true;
                weaponView.SellWeaponClick -= OnSellButtonClick;
            }
        }
    }
}