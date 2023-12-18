using System;
using System.Collections;
using System.Collections.Generic;
using _Game.WeaponsSelection.Scripts.ScriptAble;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
   public List<ScriptableObject> weapons= new List<ScriptableObject>();
   public event Action<Weapon> OnCreateWeapon;

   private void Start()
   {
      CreateWeapons();
   }

   private void CreateWeapons()
   {
      for (int i = 0; i < weapons.Count; i++)
      {
         OnCreateWeapon?.Invoke((Weapon)weapons[i]);
      }
   }
   
}
