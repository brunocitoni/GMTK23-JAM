using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    [SerializeField] Health heroHealth;
    [SerializeField] HeroAI heroAI;
    [SerializeField] Timer activeEffectTimer;
    public static bool attackBuffActive = false;
    public static bool defenceBuffActive = false;

    public int armorLevel = 0;
    public int weaponLevel = 0;

    public int attackModifier;
    public int defenceModifier;

    private void Start()
    {
        PlayerInventory.OnHealthPotionGiven += HealthPotion;
        PlayerInventory.OnAttackPotionGiven += AttackPotion;
        PlayerInventory.OnDefencePotionGiven += DefencePotion;
    }

    private void OnDestroy()
    {
        PlayerInventory.OnHealthPotionGiven -= HealthPotion;
        PlayerInventory.OnAttackPotionGiven -= AttackPotion;
        PlayerInventory.OnDefencePotionGiven -= DefencePotion;
    }


    private void HealthPotion() {

        Debug.Log("Resoving health potion");
        heroHealth.ModifyHealth(heroHealth.maxHealth / 2);
    }

    private void AttackPotion() {
        Debug.Log("Resolving attack potion");
        activeEffectTimer.SetDuration(Data.atkPotionDuration);
        // activate buff
        attackBuffActive = true;
        activeEffectTimer.TimerElapsed += () => attackBuffActive = false;
        activeEffectTimer.RestartTimer();

    }

    private void DefencePotion() {
        Debug.Log("Resolving defence potion");
        activeEffectTimer.SetDuration(Data.defPotionDuration);
        defenceBuffActive = true;
        heroHealth.defencePotion = true;
        activeEffectTimer.TimerElapsed += () => { heroHealth.defencePotion = false; defenceBuffActive = false; };
        activeEffectTimer.RestartTimer();
    }

    private void CalculateAttackModifier() {

        attackModifier = (weaponLevel * heroAI.heldWeapon.damage);
    }

    private void CalculateDefenceModifier() {
        defenceModifier = armorLevel * 2;
    }
}
