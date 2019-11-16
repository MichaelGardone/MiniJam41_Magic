using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int health;

    [SerializeField] protected int hitPower;
    [SerializeField] protected float walkSpeed;
}
