using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.WeaponsSelection.Scripts.ScriptAble
{
    [CreateAssetMenu(fileName = "SelectionData", menuName = "ScriptableObjects/CreateWeapon", order = 1)]
    public class Weapon : ScriptableObject
    {

        [SerializeField] private bool weaponUnlocked;
        [SerializeField] private int unlockPrice;
        [SerializeField] private string name;
        [SerializeField] private string weaponDps;
        [SerializeField] public GameObject weaponPrefab;
        [SerializeField] private Sprite weaponImage;
        public List<UpgradeDetail> upgradeWeapons= new List<UpgradeDetail>();
        

        public int GetBuyAmount
        {
            get => unlockPrice;

            set => unlockPrice = value;
        }

        public void Load()
        {
            foreach (var t in upgradeWeapons)
            {
                t.SetUpgrade(name);
            }

            CheckState();
        }

        private void CheckState()
        {
            if (weaponUnlocked|| PlayerPrefs.GetInt(name)==1)
            {
                weaponUnlocked = true;
                PlayerPrefs.SetInt(name,1);
            }
        }

        public bool CheckWeaponUnLocked()
        {
            return weaponUnlocked;
        }

        public bool BuyWeapon(int amount)
        {
            if (amount >= unlockPrice)
            {
                weaponUnlocked = true;
                CheckState();
                return true;
            }

            return false;
        }

        public string GetName()
        {
            return name;
        }
        public string GetDps()
        {
            return weaponDps;
        }
        public Sprite GetWeaponSprite()
        {
            return weaponImage;
        }

       
        
        
    }
    
    
}
[System.Serializable]
public class UpgradeDetail
{
    private string _weaponName;
    [SerializeField]
    private string upgradeName;
    [SerializeField]
    private float maxWeaponValue;
    [SerializeField]
    private float maxValue;
    [SerializeField]
    private float factor;
    private float _currentValue;
    [SerializeField]
    private float staringValue;
    [SerializeField] private int baseUpgradeAmount, incrementAmount;
    public void SetUpgrade(string weaponName)
    {
        _weaponName = weaponName;
        _currentValue= PlayerPrefs.GetFloat($"{upgradeName+_weaponName}",staringValue);
    }
    public void UpgradeWeapon()
    {
        _currentValue += factor;
        PlayerPrefs.SetFloat($"{upgradeName+_weaponName}",_currentValue);
        PlayerPrefs.SetInt("multiplier"+_weaponName+upgradeName,PlayerPrefs.GetInt("multiplier"+_weaponName+upgradeName)+1);
        
    }
    public bool CanUpgrade()
    {
        if (!IsMax())
        {
            
        }

        return false;
    }

    public bool IsMax()
    {
        return maxWeaponValue <= _currentValue;
    }
    public float GetCurrentValue()
    {
        return _currentValue;
    }
    public string GetUpgradeName()
    {
        return upgradeName;
    }
    public float GetMaxWeaponValue()
    {
        return maxWeaponValue;
    }
    public float GetMaxValue()
    {
        return maxValue;
    }
    public float GetFactor()
    {
        return factor;
    }
    public int GetBaseUpgradeAmount()
    {
        return baseUpgradeAmount;
    }
    public int GetCurrentUpgradeAmount()
    {
        return baseUpgradeAmount+(PlayerPrefs.GetInt("multiplier"+_weaponName+upgradeName)*incrementAmount);
    }
    
}