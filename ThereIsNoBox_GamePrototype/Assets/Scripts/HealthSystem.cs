using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnHealthDepleted;

    [SerializeField] int health = 6;
    int maxHealth;

    void Awake()
    {
        maxHealth = health;
    }

    public void DamageHealth(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0)
        {
            health = 0;
        }

        if (health == 0)
        {
            HealthDepleted();
        }

        Debug.Log(health);
    }

    public int GetHealth()
    {
        return health;
    }

    void HealthDepleted()
    {
        OnHealthDepleted?.Invoke(this, EventArgs.Empty);
    }
}
