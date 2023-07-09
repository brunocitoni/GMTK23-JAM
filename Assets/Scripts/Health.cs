using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100; // default to 100, should be overritten;

    public bool defencePotion = false;
    public bool attackPotion = false;

    public int attackModifier = 0;
    public int defenceModifier = 0;

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
        if (change > 0) // this entity is GAINING life
        { 
            currentHealth += change;
            if (currentHealth > 0)
            {
                hasDied = false;
            }
        }
        else // if this entity is TAKING damage
        {
            if (this.gameObject.tag == "Player") // if this is the player taking damage, no modifiers should apply
            {
                currentHealth += change;
            }
            else
            {
                if (this.gameObject.tag == "Hero") //if the hero is taking damage
                {
                    change = CalculateModifiedDefenceDamage(change); // modify damage based on defence rating
                    if (HeroManager.defenceBuffActive) // if this is the hero and the defence buff is active
                    {
                        Debug.Log(this.name + " taking less damage because of defence potion");
                        currentHealth += change + Data.defPotionBuff; // do less damage TO the hero
                    }
                    else
                    {
                        Debug.Log(this.name + " taking normal damage (after armor modifier)");
                        currentHealth += change;
                    }
                }
                else // damage us being received by anything else other than player and hero (i.e. enemies)
                {
                    change = CalculateModifiedAttackDamage(change); // modify damage based on attack rating
                    if (HeroManager.attackBuffActive)
                    {
                        Debug.Log(this.name + " dealing even more damage because of attack potion");
                        currentHealth += change - Data.atkPotionBuff; // do less damage TO the hero
                    }
                    else
                    {
                        Debug.Log(this.name + " dealing normal damage (after weapon modifier)");
                        currentHealth += change;
                    }
                }
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

    private int CalculateModifiedAttackDamage(int change)
    {
        return change - HeroManager.attackModifier; // modify damage based on attack rating
    }

    private int CalculateModifiedDefenceDamage(int change)
    {
        return change + HeroManager.defenceModifier; // modify damage based on defence rating
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
