using System;
using System.Collections;
using System.Collections.Generic;
using _Game.WeaponsSelection.Scripts.ScriptAble;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDetailUI : MonoBehaviour
{
    private int _weaponId;
    [SerializeField] private Image weaponImage;
    [SerializeField] private Image selectedImage;
    [SerializeField] private Image equipImage;
    [SerializeField] private Text dpsText;
    [SerializeField] private Button selectButton;
    private Weapon _weapon;

    private void OnEnable()
    {
        WeaponsPresenter.Instance.OnUnEquipWeapon += UnEquipWeapon;
        WeaponsPresenter.Instance.OnUnSelectWeapon += UnSelect;
        
    }
    private void OnDisable()
    {
        WeaponsPresenter.Instance.OnUnEquipWeapon -= UnEquipWeapon;
        WeaponsPresenter.Instance.OnUnSelectWeapon -= UnSelect;
    }

    private void Start()
    {
        selectButton.onClick.AddListener(SelectWeapon);
    }

    public void SetWeaponUi(Weapon weapon, int Id)
    {
        _weaponId = Id;
        _weapon = weapon;
        weaponImage.sprite = _weapon.GetWeaponSprite();
        dpsText.text =$"{("DPS ")}{_weapon.GetDps()}";
       // UnSelect();
       // UnEquipWeapon();
    }
    private void UnSelect()
    {
        selectedImage.enabled = false;
    }
    public void SelectWeapon()
    {
        WeaponManager.Instance.OnSelectedWeaponButton = SelectWeapon;
        WeaponManager.Instance.SetCurrentWeapon(_weapon);
        WeaponManager.Instance.SelectedWeapon = _weaponId;
        selectedImage.enabled = true;
    }
    public void EquipWeapon()
    {
        equipImage.enabled = true;
        WeaponManager.Instance.EquipWeapon = _weaponId;
        Debug.Log("equip"+ _weaponId);
    }

    private void UnEquipWeapon()
    {
        equipImage.enabled = false;
    }

   
}