using System;
using System.Collections;
using System.Collections.Generic;
using _Game.WeaponsSelection.Scripts.ScriptAble;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
   public static WeaponManager Instance;
   [SerializeField]
   private List<ScriptableObject> weapons= new List<ScriptableObject>();

   private Weapon _currentWeapon;
   public Weapon CurrentWeapon   // property
   { 
      get { return _currentWeapon; }   // get method
      private set { _currentWeapon = value; }  // set method
   }
   private int _selectedWeapon = 0;

   public int SelectedWeapon
   {
      get => _selectedWeapon;
      set => _selectedWeapon = value;
   }
   private int _equipWeapon = 0;

   public int EquipWeapon
   {
      get => PlayerPrefs.GetInt("selectedWeapon",_equipWeapon);
      set
      {
         _selectedWeapon = value;
         _equipWeapon = _selectedWeapon; 
         PlayerPrefs.SetInt("selectedWeapon",_equipWeapon);
         
      }
   }

   public event Action<Weapon,int> OnCreateWeapon;
   public event Action<UpgradeDetail> OnCreateWeaponBar;
   
   public event Action OnSelectedWeapon;
   public event Action<int,bool> OnSetBuyAmount;
   
   
      



   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      LoadWeapons();
      CreateWeapons();
      SelectedWeapon = EquipWeapon;
   }
   
   private void LoadWeapons()
   {
      for (int i = 0; i < weapons.Count; i++)
      {
         Weapon weapon = (Weapon)weapons[i];
         weapon.Load();
      }
   }

   private void CreateWeapons()
   {
      for (int i = 0; i < weapons.Count; i++)
      {
         OnCreateWeapon?.Invoke((Weapon)weapons[i],i);
      }

      SetCurrentWeapon((Weapon) weapons[0]);
   }
   private void CreateWeaponUpdateBar(Weapon weapon)
   {
      Debug.Log(weapon.upgradeWeapons.Count);
      foreach (var t in weapon.upgradeWeapons)
      {
         OnCreateWeaponBar?.Invoke(t);
      }
   }
   
   public void SetCurrentWeapon(Weapon currentWeapon)
   {
      CurrentWeapon = currentWeapon;
      ResetCurrentWeapon();
      OnSetBuyAmount?.Invoke(CurrentWeapon.GetBuyAmount,CurrentWeapon.CheckWeaponUnLocked());
   }

   public void ResetCurrentWeapon()
   {
      OnSelectedWeapon?.Invoke();
      CreateWeaponUpdateBar(CurrentWeapon);
//      Debug.Log("aaa"+CurrentWeapon.CheckWeaponUnLocked());
   }

   public void BuyWeapon()
   { var bought= CurrentWeapon.BuyWeapon(EconomyManager.Instance.GetCurrentCash());
      if (bought)
      {
         SetCurrentWeapon(CurrentWeapon);
      }
   }


}
