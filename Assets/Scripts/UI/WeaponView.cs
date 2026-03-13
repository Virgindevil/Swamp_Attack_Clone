using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _buyButton;

    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private Weapon _weapon;
    public event UnityAction<Weapon, WeaponView> SellWeaponClick;

    private void Start()
    {
        TryLockItem();
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
        if (_weapon.IsPurchased == true)
        {
            _sellButton.interactable = false;
            _price.text = "";
        }
    }
    public void MarkAsPurchased()
    {
        if (_buyButton != null)
        {
            _buyButton.interactable = false;
        }
        _priceText.text = "КУПЛЕНО";
    }

    public void Render(Weapon weapon)
    {
        _weapon = weapon;
        _label.text = _weapon.Label;
        _price.text = _weapon.Price.ToString();
        _icon.sprite = weapon.Icon;
    }

    private void OnButtonClick()
    {
        SellWeaponClick?.Invoke(_weapon,this);
    }
}
