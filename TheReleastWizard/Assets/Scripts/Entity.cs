using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int health;

    [SerializeField] protected int hitPower;
    [SerializeField] protected float walkSpeed;

    protected List<MagicMod> modifiers = new List<MagicMod>();

    public void ModifyHealth(int damage)
    {
        health += damage;

        if (health < 0)
            health = 0;
    }

    public void ApplyModifiers(MagicMod mod)
    {

    }

    public float GetHealthAsPercent()
    {
        return ((float)health) / maxHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }
}
