using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Slider healthbarUISlider;

    public delegate void PlayerDeath();
    public static event PlayerDeath OnPlayerDeath;

    public override void Start()
    {
        base.Start();

        healthbarUISlider = GameObject.Find("PlayerHealthSlider").GetComponent<Slider>();
        SetHealth(maxHealth);

    }

    public override void SetHealth(float health)
    {
        base.SetHealth(health);

        if(healthbarUISlider != null)
        {
            healthbarUISlider.value = currentHealth;
            healthbarUISlider.maxValue = maxHealth;
        }
    }

    public override bool ModifyHealth(float change)
    {
        Debug.Log("Player took DAMAGE!! {" + Time.time);
        bool ret = base.ModifyHealth(change);
        if(healthbarUISlider != null) 
        {
            healthbarUISlider.value = currentHealth;
            healthbarUISlider.maxValue = maxHealth;
        }

        if (currentHealth <= 0) {
            Debug.Log("Invoking on player death in playerhealth");
            OnPlayerDeath?.Invoke();
        }

        return ret;
    }
}
