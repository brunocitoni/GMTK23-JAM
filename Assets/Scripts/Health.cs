using UnityEngine;

public class Health : MonoBehaviour
{
    int currentHealth;
    public int maxHealth = 100; // default to 100, should be overritten;

    public delegate void DeathDelegate();
    public DeathDelegate OnThisDeath; // Event to be invoked on death

    public void Start()
    {
        SetHealth(maxHealth);
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

        if (currentHealth <= 0) {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        OnThisDeath?.Invoke();
    }

}
