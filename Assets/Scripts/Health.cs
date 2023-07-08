using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100; // default to 100, should be overritten;

    public bool defencePotion = false;
    public bool attackPotion = false;

    public delegate void DeathDelegate();
    public DeathDelegate OnThisDeath; // Event to be invoked on death

    public InWorldSlider healthbar;
    Material spriteMaterial;

    [HideInInspector]
    public bool hasDied;

    public virtual void Start()
    {
        spriteMaterial = GetComponentInChildren<SpriteRenderer>().material;
        if (gameObject.tag == "Hero") // if this is the health script of the hero
        {
            SetHealth(Data.heroMaxHealth);
        }
    }

    public virtual void SetHealth(int health)
    {
        healthbar = GetComponent<InWorldSlider>();

        maxHealth = health;
        if (healthbar != null)
            healthbar.maxValue = health;
        currentHealth = maxHealth;
        hasDied = false;
    }

    public virtual bool ModifyHealth(int change)
    {
        if (change > 0)
        { // if we are gaining life no other checks should be made, just give the life that was input 
            currentHealth += change;
            if(currentHealth > 0)
            {
                hasDied = false;
            }
        }
        else { // if this entity is TAKING damage

            if (defencePotion) {
                Debug.Log(this.name + " taking less damage because of defence potion");
                currentHealth += change + Data.defPotionBuff; // do less damage TO the hero
            } else if (HeroManager.attackBuffActive && this.gameObject.tag != "Hero")
            {
                Debug.Log("Dealing more damage to" + this.gameObject.name + " because of attack potion");
                currentHealth += change - Data.atkPotionBuff; // take more damage 
            }
            else {
                currentHealth += change; // normal damage if neither potion is active in all cases
            }
        }

        // clamp
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //Update Slider
        if (healthbar != null)
            healthbar.value = currentHealth;

        //Hitflash
        if (change < 0)
        {
            StartCoroutine(HitFlash());
        }

        //Check Death
        if (currentHealth <= 0)
        {
            OnDeath();
            return true;
        }

        return false;
    }

    IEnumerator HitFlash()
    {
        spriteMaterial.SetFloat("_Flash", 1);
        yield return new WaitForSeconds(0.1f);
        spriteMaterial.SetFloat("_Flash", 0);
    }

    public void OnDeath()
    {
        OnThisDeath?.Invoke();
        hasDied = true;
    }

}
