using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] HeroManager heroManager;
    [SerializeField] PlayerInventory inventory;

    int[] upgradeMaterialIRONWeapon = { 2, 4, 4 };

    int[] upgradeMaterialIRON = { 1, 2, 2 };
    int[] upgradeMaterialLEATHER = { 1, 2, 2 };
    int[] upgradeMaterialSCRAP = { 1, 2, 2 };

    [SerializeField] TMP_Text ironRequiredWeapon;
    [SerializeField] TMP_Text scrapsRequiredWeapon; 
    [SerializeField] TMP_Text ironRequiredArmor;
    [SerializeField] TMP_Text leatherRequiredArmor;
    [SerializeField] TMP_Text scrapsRequiredArmor;

    [SerializeField] Button upgradeWeaponButton;
    [SerializeField] Button upgradeArmorButton;

    int ironHeld, scrapHeld, leatherHeld;
    int ironRequiredForWeapon, scrapRequiredForWeapon, ironRequiredForArmor, leatherRequiredForArmor, scrapRequiredForArmor;

    private void OnEnable()
    {
        UpdateUI();
    }

    // display requirements in terms on items and update UI to reflect whether buttons should be enabled
    private void UpdateUI() {

        //check if max level already


        ironRequiredForWeapon = upgradeMaterialIRONWeapon[heroManager.weaponLevel];
        scrapRequiredForWeapon = upgradeMaterialSCRAP[heroManager.weaponLevel];

        ironRequiredWeapon.text = ironRequiredForWeapon.ToString();
        scrapsRequiredWeapon.text = scrapRequiredForWeapon.ToString();

        ironRequiredForArmor = upgradeMaterialIRON[heroManager.armorLevel];
        leatherRequiredForArmor = upgradeMaterialLEATHER[heroManager.armorLevel];
        scrapRequiredForArmor = upgradeMaterialSCRAP[heroManager.armorLevel];

        ironRequiredArmor.text = ironRequiredForArmor.ToString();
        leatherRequiredArmor.text = leatherRequiredForArmor.ToString();
        scrapsRequiredArmor.text = scrapRequiredForArmor.ToString();




    }

    public void CheckIfUpgradeIsAvailable() {

        ironHeld = inventory.itemsHeld.FindAll(item => item.itemName == "Iron").Count;
        scrapHeld = inventory.itemsHeld.FindAll(item => item.itemName == "Scraps").Count;
        leatherHeld = inventory.itemsHeld.FindAll(item => item.itemName == "Leather").Count;
        if (ironHeld >= ironRequiredForWeapon && scrapHeld >= scrapRequiredForWeapon) {

            WeaponUpgradeAvailable(true);
        } else {
            WeaponUpgradeAvailable(false);
        }

        if (leatherHeld >= leatherRequiredForArmor && ironHeld >= ironRequiredForArmor && scrapHeld >= scrapRequiredForArmor) {
            ArmorUpgradeAvailable(true);
        }
        else {
            ArmorUpgradeAvailable(false);
        }
    }

    private void WeaponUpgradeAvailable(bool available) {
        // enable upgrade button
        upgradeWeaponButton.interactable = available;
    }

    private void ArmorUpgradeAvailable(bool available)
    {
        // enable upgrade button
        upgradeArmorButton.interactable = available;

    }

    public void UpgradeWeapon() {
        heroManager.weaponLevel++;
        UpdateUI();
    }

    public void UpgradeArmor() {

        heroManager.armorLevel++;
        UpdateUI();
    }


    public void OnClickInvokeNewWaveStart()
    {
        this.gameObject.SetActive(false);
        WaveManager.InvokeWaveStart();
    }
}
