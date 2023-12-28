using System;
using System.Collections;
using System.Collections.Generic;
using _Game.WeaponsSelection.Scripts.ScriptAble;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsPresenter : MonoBehaviour
{
  public static WeaponsPresenter Instance;
  [SerializeField] private WeaponManager weaponManager;
  [SerializeField] private GameObject upgradePrefab;
  [SerializeField] private GameObject weaponUiPrefab;
  [SerializeField] private Text weaponName;
  [SerializeField] private Image weaponImage;
  [SerializeField] private Transform upgradeGrid;
  [SerializeField] private Transform weaponsGrid;
  [SerializeField] private Button exitButton, equipButton,buyButton;
  private readonly List<GameObject> _uiWeapons= new List<GameObject>();
  private readonly List<GameObject> _upgradeBars= new List<GameObject>();
  public event Action OnUnEquipWeapon;
  public event Action OnUnSelectWeapon;
  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    Init();
    exitButton.onClick.AddListener(Exit);
    equipButton.onClick.AddListener(EquipWeapon);
    buyButton.onClick.AddListener(BuyWeapon);
  }

  private void Init()
  {
    DefaultSelectedWeapon();
    EquipWeapon();
  }
  private void OnEnable()
  {
    weaponManager.OnCreateWeapon += SetWeaponUi;
    weaponManager.OnCreateWeaponBar += SetUpgradeBar;
    weaponManager.OnSelectedWeapon += SelectWeapon;
  }
  
  private void OnDisable()
  {
    weaponManager.OnCreateWeapon -= SetWeaponUi;
    weaponManager.OnCreateWeaponBar -= SetUpgradeBar;
    weaponManager.OnSelectedWeapon -= SelectWeapon;
    
  }
  private void SetWeaponUi(Weapon obj, int id)
  {
    var go = Instantiate(weaponUiPrefab, weaponsGrid);
    _uiWeapons.Add(go);
    go.GetComponent<WeaponDetailUI>().SetWeaponUi(obj,id);
  }

  private void SetUpgradeBar(UpgradeDetail upgradeDetail)
  {
 //   ResetList(upgradeBars);
    var go = Instantiate(upgradePrefab, upgradeGrid);
    go.GetComponent<UpgradeBar>().SetUpgradeBar(upgradeDetail);
    _upgradeBars.Add(go);
  }

  private void ResetList(ICollection<GameObject> list)
  {
    foreach (var variable in list)
    {
      Destroy(variable);
    }
    list.Clear();
  }
  public void ResetSelection()
  {
    OnUnSelectWeapon?.Invoke();
  }
  private void UnEquipAll()
  {
    OnUnEquipWeapon?.Invoke();
  }

  private void DefaultSelectedWeapon()
  { 
   // uiWeapons[SelectedWeapon].GetComponent<WeaponDetailUI>().EquipWeapon();
    _uiWeapons[weaponManager.SelectedWeapon].GetComponent<WeaponDetailUI>().SelectWeapon();
    SetWeaponUi();
  }

  private void SelectWeapon()
  {
    ResetSelection();
    ResetList(_upgradeBars);
    SetWeaponUi();
    CheckBuyStatus();
  }
  
  private void SetWeaponUi()
  {
    weaponName.text = weaponManager.CurrentWeapon.GetName();
    weaponImage.sprite = weaponManager.CurrentWeapon.GetWeaponSprite();
  }
  private void EquipWeapon()
  {
    UnEquipAll();
    Debug.Log("aa"+weaponManager.SelectedWeapon);
    _uiWeapons[weaponManager.SelectedWeapon].GetComponent<WeaponDetailUI>().EquipWeapon();
  }

  private void CheckBuyStatus()
  {
    buyButton.gameObject.SetActive(!weaponManager.CurrentWeapon.CheckWeaponUnLocked());
    equipButton.gameObject.SetActive(weaponManager.CurrentWeapon.CheckWeaponUnLocked());
  }
  private void BuyWeapon()
  {
    weaponManager.BuyWeapon();
  }

  private void Exit()
  {
    
  }

}
