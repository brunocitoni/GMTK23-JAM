using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] HeroManager heroManager;
    [SerializeField] PlayerInventory inventory;

    int[] upgradeMaterialIRONWeapon = { 2, 2, 4,4 };

    int[] upgradeMaterialIRON = { 1, 1,2, 2 };
    int[] upgradeMaterialLEATHER = { 1, 1,2, 2 };
    int[] upgradeMaterialSCRAP = { 1, 1,2, 2 };

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

        ironRequiredForWeapon = upgradeMaterialIRONWeapon[heroManager.weaponLevel];
        scrapRequiredForWeapon = upgradeMaterialSCRAP[heroManager.weaponLevel];

        ironRequiredWeapon.text = "x" + ironRequiredForWeapon.ToString();
        scrapsRequiredWeapon.text = "x" + scrapRequiredForWeapon.ToString();

        ironRequiredForArmor = upgradeMaterialIRON[heroManager.armorLevel];
        leatherRequiredForArmor = upgradeMaterialLEATHER[heroManager.armorLevel];
        scrapRequiredForArmor = upgradeMaterialSCRAP[heroManager.armorLevel];

        ironRequiredArmor.text = "x" + ironRequiredForArmor.ToString();
        leatherRequiredArmor.text = "x" + leatherRequiredForArmor.ToString();
        scrapsRequiredArmor.text = "x" + scrapRequiredForArmor.ToString();

        CheckIfUpgradeIsAvailable();

        CheckIfMaxLevel();
    }

    public void CheckIfUpgradeIsAvailable() {

        ironHeld = inventory.itemsHeld.FindAll(item => item.itemName == "Iron").Count;
        scrapHeld = inventory.itemsHeld.FindAll(item => item.itemName == "Scraps").Count;
        leatherHeld = inventory.itemsHeld.FindAll(item => item.itemName == "Leather").Count;
        if (ironHeld >= ironRequiredForWeapon && scrapHeld >= scrapRequiredForWeapon) {

            WeaponUpgradeAvailable(true, "Upgrade!");
        } else {
            WeaponUpgradeAvailable(false, "You lack the required components");
        }

        if (leatherHeld >= leatherRequiredForArmor && ironHeld >= ironRequiredForArmor && scrapHeld >= scrapRequiredForArmor) {
            ArmorUpgradeAvailable(true, "Upgrade!");
        }
        else {
            ArmorUpgradeAvailable(false, "You lack the required components");
        }
    }

    private void CheckIfMaxLevel() {

        if (heroManager.armorLevel == 3) {
            ArmorUpgradeAvailable(false, "You can't upgrade the hero's armor any further");
        }

        if (heroManager.weaponLevel == 3)
        {
            WeaponUpgradeAvailable(false, "You can't upgrade the hero's weapon any further");
        }


    }

    private void WeaponUpgradeAvailable(bool available, string message = "") {
        // enable upgrade button
        upgradeWeaponButton.interactable = available;
        upgradeWeaponButton.GetComponentInChildren<TMP_Text>().text = message;
    }

    private void ArmorUpgradeAvailable(bool available, string message = "")
    {
        // enable upgrade button
        upgradeArmorButton.interactable = available;
        upgradeArmorButton.GetComponentInChildren<TMP_Text>().text = message;

    }

    public void UpgradeWeapon() {
        heroManager.weaponLevel++;
        RemoveItemsFromInventory("Iron", ironRequiredForWeapon);
        RemoveItemsFromInventory("Scrap", scrapRequiredForWeapon);
        UpdateUI();
    }

    public void UpgradeArmor() {

        heroManager.armorLevel++;
        RemoveItemsFromInventory("Iron", ironRequiredForArmor);
        RemoveItemsFromInventory("Scrap", scrapRequiredForArmor);
        RemoveItemsFromInventory("Leather", leatherRequiredForArmor);
        UpdateUI();
    }

    private void RemoveItemsFromInventory(string itemName, int count)
    {
        List<ItemSO> itemsToRemove = inventory.itemsHeld.FindAll(item => item.itemName == itemName);

        for (int i = 0; i < count; i++)
        {
            if (itemsToRemove.Count > 0)
            {
                inventory.itemsHeld.Remove(itemsToRemove[0]);
                itemsToRemove.RemoveAt(0);
            }
            else
            {
                break;
            }
        }

        inventory.RefreshUI();
    }


    public void OnClickInvokeNewWaveStart()
    {
        this.gameObject.SetActive(false);
        WaveManager.InvokeWaveStart();
    }
}
