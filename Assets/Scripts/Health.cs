using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth = 100;
    private UI ui; 

    // Use this for initialization
    void Start()
    {
        CurrentHealth = MaxHealth;
        ui = FindObjectOfType<UI>();
    }

    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public bool AddHealth(int healthAmount)
    {
        if (CurrentHealth >= MaxHealth)
            return false;

        if (CurrentHealth + healthAmount > MaxHealth)
            CurrentHealth += MaxHealth - CurrentHealth;
        else        
            CurrentHealth += healthAmount;

        ui.UpdateHealth(CurrentHealth);
        return true;
    }

    //applies damage to health and returns current health
    public int TakeDamage(int damageAmount)
    {        
        return CurrentHealth -= damageAmount;
    }
}
