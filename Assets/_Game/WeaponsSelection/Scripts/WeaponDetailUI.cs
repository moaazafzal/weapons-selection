using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDetailUI : MonoBehaviour
{
    [SerializeField] private Image weaponImage;
    [SerializeField] private Image selectedImage;
    [SerializeField] private Image tickedImage;
    [SerializeField] private Text dpsText;

    public void SetWeaponUi(Sprite weaponSprite, string weaponDps)
    {
        weaponImage.sprite = weaponSprite;
        dpsText.text =$"{("DPS ")}{weaponDps}" ;
        UnSelect();
        UnEquipWeapon();
    }
    public void UnSelect()
    {
        selectedImage.enabled = false;
    }
    public void SelectWeapon()
    {
        selectedImage.enabled = true;
    }
    public void EquipWeapon()
    {
        tickedImage.enabled = true;
    }
    public void UnEquipWeapon()
    {
        tickedImage.enabled = false;
    }
}