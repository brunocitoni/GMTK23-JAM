using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int currentHealth;
    public int maxHealth = 100; // default to 100, should be overritten;

    public delegate void DeathDelegate();
    public DeathDelegate OnThisDeath; // Event to be invoked on death

    Material spriteMaterial; 

    [HideInInspector]
    public bool hasDied;

    public void Start()
    {
        hasDied = false;
        SetHealth(maxHealth);

        spriteMaterial = GetComponent<SpriteRenderer>().material;
    }

    public void OnDestroy()
    {
        
    }

    public void SetHealth(int health)
    {
        maxHealth = health;
    }

    public void ModifyHealth(int change)
    {
        currentHealth += change;
        // clamp
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if(change < 0)
        {
            StartCoroutine(HitFlash());
        }

        if (currentHealth <= 0) {
            OnDeath();
        }
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
