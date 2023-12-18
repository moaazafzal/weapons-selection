using System.Collections.Generic;
using UnityEngine;

namespace _Game.WeaponsSelection.Scripts.ScriptAble
{
    [CreateAssetMenu(fileName = "SelectionData", menuName = "ScriptableObjects/CreateWeapon", order = 1)]
    public class Weapon : ScriptableObject
    {

        [SerializeField] private string name;
        [SerializeField] private string weaponDps;
        [SerializeField] public GameObject weaponPrefab;
        [SerializeField] private Sprite weaponImage;
        public List<UpgradeDetail> upgradeWeapons= new List<UpgradeDetail>();
        public void Load()
        {
            foreach (var t in upgradeWeapons)
            {
                t.SetUpgrade(name);
            }
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
    private float maxValue;
    [SerializeField]
    private float factor;
    [SerializeField]
    private float currentValue;

    public void SetUpgrade(string weaponName)
    {
        _weaponName = weaponName;
        currentValue= PlayerPrefs.GetInt($"{upgradeName+_weaponName}");
    }
    public void UpgradeWeapon()
    {
        currentValue += factor;
        PlayerPrefs.SetFloat($"{upgradeName+_weaponName}",currentValue);
    }
    public bool CanUpgrade()
    {
        return maxValue > currentValue;
    }
    public float GetCurrentValue()
    {
        return currentValue;
    }
    public string GetUpgradeName()
    {
        return upgradeName;
    }
    public float GetMaxValue()
    {
        return maxValue;
    }
    public float GetFactor()
    {
        return factor;
    }
    
}