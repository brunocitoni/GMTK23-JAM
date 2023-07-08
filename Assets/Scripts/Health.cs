using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int currentHealth;
    public int maxHealth = 100; // default to 100, should be overritten;

    public delegate void DeathDelegate();
    public DeathDelegate OnThisDeath; // Event to be invoked on death

    public InWorldSlider healthbar;
    Material spriteMaterial; 

    [HideInInspector]
    public bool hasDied;

    public void Start()
    {
        spriteMaterial = GetComponentInChildren<SpriteRenderer>().material;
        if (gameObject.tag == "Hero" ) // if this is the health script of the hero
        {
            SetHealth(Data.heroMaxHealth);
        }
    }

    public void SetHealth(int health)
    {
        healthbar = GetComponent<InWorldSlider>();

        maxHealth = health;
        healthbar.maxValue = health;
        currentHealth = maxHealth;
        hasDied = false;
    }

    public bool ModifyHealth(int change)
    {
        currentHealth += change;
        // clamp
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthbar.value = currentHealth;
        if(change < 0)
        {
            StartCoroutine(HitFlash());
        }

        if (currentHealth <= 0) {
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
