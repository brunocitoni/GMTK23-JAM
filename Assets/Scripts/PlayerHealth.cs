using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public Slider healthbarUISlider;

    public override void Start()
    {
        base.Start();

        healthbarUISlider = GameObject.Find("PlayerHealthSlider").GetComponent<Slider>();
        SetHealth(maxHealth);

    }

    public override void SetHealth(int health)
    {
        base.SetHealth(health);

        if(healthbarUISlider != null)
        {
            healthbarUISlider.value = currentHealth;
            healthbarUISlider.maxValue = maxHealth;
        }
    }

    public override bool ModifyHealth(int change)
    {
        Debug.Log("Player took DAMAGE!! {" + Time.time);
        bool ret = base.ModifyHealth(change);
        if(healthbarUISlider != null) 
        {
            healthbarUISlider.value = currentHealth;
            healthbarUISlider.maxValue = maxHealth;
        }

        return ret;
    }
}
