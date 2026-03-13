using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{   [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private WeaponInfo _weaponInfo;
    private Weapon _weapon;
    public event UnityAction<WeaponInfo, WeaponView> SellWeaponClick;

    private void Start()
    {
        OnButtonClick();
    }

    private void OnEnable()
    {        
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }

    private void TryLockItem()
    {
        if (_weaponInfo.IsPurchased)
        {
            MarkAsPurchased();
        }
    }
    public void MarkAsPurchased()
    {
        _sellButton.interactable = false;
        _price.text = "";
    }

    public void Render(WeaponInfo weaponInfo)
    {
        _weaponInfo = weaponInfo;
        _weapon = weaponInfo.WeaponComponent;
        _label.text = _weapon.Label;
        _price.text = _weapon.Price.ToString();
        _icon.sprite = _weapon.Icon;
        if (_weaponInfo.IsPurchased)
        {
            MarkAsPurchased();
        }
    }

    private void OnButtonClick()
    {
        SellWeaponClick?.Invoke(_weaponInfo,this);
    }
}
