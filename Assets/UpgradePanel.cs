using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private void OnEnable()
    {
        UpdateUI();
    }

    // display requirements in terms on items and update UI to reflect whether buttons should be enabled
    private void UpdateUI() {

        //check if max level already

        ironRequiredWeapon.text = upgradeMaterialIRONWeapon[heroManager.weaponLevel].ToString();
        scrapsRequiredWeapon.text = upgradeMaterialSCRAP[heroManager.weaponLevel].ToString();

        ironRequiredArmor.text = upgradeMaterialIRON[heroManager.armorLevel].ToString();
        leatherRequiredArmor.text = upgradeMaterialLEATHER[heroManager.armorLevel].ToString();
        scrapsRequiredArmor.text = upgradeMaterialSCRAP[heroManager.armorLevel].ToString();




    }

    public void CheckIfUpgradeIsAvailable() {

        if (heroManager.armorLevel == 3) {

        }
    }

    public void UpgradeWeapon() {
        heroManager.weaponLevel++;
        UpdateUI();
    }

    public void UpgradeArmor() {

        heroManager.armorLevel++;
        UpdateUI()
    }


    public void OnClickInvokeNewWaveStart()
    {
        this.gameObject.SetActive(false);
        WaveManager.InvokeWaveStart();
    }
}
