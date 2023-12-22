using System;
using System.Collections;
using System.Collections.Generic;
using _Game.WeaponsSelection.Scripts.ScriptAble;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBar : MonoBehaviour
{
    [SerializeField]  private Text upgradeNameText;
    [SerializeField]  private Text currentValueText;
    [SerializeField]  private Button upgradeButton;
    [SerializeField] private Text factorText;
    [SerializeField] private Sprite upgradeSprite, maxSprite;
    [SerializeField] private Slider slider;

    private string _upgradeName;
    private float _currentValue;
    private float _factor;
    private UpgradeDetail _upgradeDetail;
    private bool canUpgrade;
    private void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeWeapon);
    }

    public void SetUpgradeBar(UpgradeDetail upgradeDetail)
    {
        _upgradeDetail = upgradeDetail;
        UpdateValues();
    }

    private void UpdateValues()
    {
        _upgradeName = _upgradeDetail.GetUpgradeName();
        _currentValue = _upgradeDetail.GetCurrentValue();
        _factor = _upgradeDetail.GetFactor();
        upgradeNameText.text = _upgradeName;
        currentValueText.text = $"{_currentValue}";
        factorText.text = $"{_factor}";
        SetUpgradeSlider();
        CheckStatus();
    }

    private void UpgradeWeapon()
    {
        if (canUpgrade)
        {
            _upgradeDetail.UpgradeWeapon();
            WeaponManager.Instance.ResetCurrentWeapon();
        }
    }

    public void SetUpgradeSlider()
    {
        slider.value = _currentValue / _upgradeDetail.GetMaxValue();
    }

    private void CheckStatus()
    {
        canUpgrade = _upgradeDetail.CanUpgrade();
        upgradeButton.image.sprite = canUpgrade ? upgradeSprite : maxSprite;
    }

}