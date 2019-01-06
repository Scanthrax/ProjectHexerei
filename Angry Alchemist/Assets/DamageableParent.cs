using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public bool alive;

    public float healthUnitInterval()
    {
        return alive ? (float)health / maxHealth : (float)(maxCorpseHealth + health) / maxCorpseHealth;

    }


    private void Start()
    {
        health = maxHealth;
        corpseHealth = maxCorpseHealth;

    }

    public void DealDamage(int damage)
    {
        health -= damage;
        Spew.instance.SpewThings(UIManager.instance.combatText, transform.position, 1,damage);

        if (health <= 0)
        {

            var agent = GetComponent<NavMeshAgent>();
            if (agent != null)
                Destroy(agent);

            alive = false;

            if (maxCorpseHealth != 0)
            {
                var rend = GetComponent<SpriteRenderer>();
                if(rend == null)
                {
                    rend = GetComponentInChildren<SpriteRenderer>();
                    rend.sprite = SpriteManager.instance.corpse;
                }

            }

            //agent.isStopped = true;
        }

        if (health <= -maxCorpseHealth)
        {
            foreach (var item in transform.GetComponents(typeof(Component)))
            {
                if (item.GetType() == typeof(AudioSource) || item.GetType() == typeof(Transform))
                    continue;
                Destroy(item);
            }

            var source = GetComponent<AudioSource>();
            if (source != null) 
                Destroy(gameObject, source.clip.length);
            else
                Destroy(gameObject);
        }
    }
}
