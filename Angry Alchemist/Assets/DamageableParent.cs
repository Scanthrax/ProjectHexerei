using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableParent : MonoBehaviour
{
    [Header("Initialize")]
    public int maxHealth;
    public int maxCorpseHealth;
    public int damage;
    public float attackSpeed;
    public float attackRange;
    public float moveSpeed;

    [Header("Non-Initialize")]
    public int health;
    public int corpseHealth;

    public void DealDamage(int damage)
    {
        health -= damage;
    }
}
