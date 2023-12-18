using System;
using System.Collections;
using System.Collections.Generic;
using _Game.WeaponsSelection.Scripts.ScriptAble;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsPresenter : MonoBehaviour
{
  [SerializeField] private WeaponManager weaponManager;
  [SerializeField] private GameObject upgradePrefab;
  [SerializeField] private GameObject weaponUiPrefab;
  [SerializeField] private Text weaponName;
  [SerializeField] private Image weaponImage;
  [SerializeField] private Transform upgradeGrid;
  [SerializeField] private Transform weaponsGrid;
  [SerializeField] private Button exitButton, equipButton;
  private List<GameObject> uiGuns= new List<GameObject>();
  private int _selectedWeapon = 0;

  private void Start()
  {
    Init();
  }

  private void Init()
  {
    _selectedWeapon = PlayerPrefs.GetInt("selectedWeapon");
    DefaultSelectedGun();

  }
  private void OnEnable()
  {
    weaponManager.OnCreateWeapon += SetWeaponUI;
  }
  
  private void OnDisable()
  {
    weaponManager.OnCreateWeapon -= SetWeaponUI;
  }
  private void SetWeaponUI(Weapon obj)
  {
    GameObject go = Instantiate(weaponUiPrefab, weaponsGrid);
    uiGuns.Add(go);
    go.GetComponent<WeaponDetailUI>().SetWeaponUi(obj.GetWeaponSprite(),obj.GetDps());
  }

  private void DefaultSelectedGun()
  {
    uiGuns[_selectedWeapon].GetComponent<WeaponDetailUI>().EquipWeapon();
    uiGuns[_selectedWeapon].GetComponent<WeaponDetailUI>().SelectWeapon();
  }
 
}
