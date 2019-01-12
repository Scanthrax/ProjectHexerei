using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyParent : DamageableParent
{
    public int maxCorpseHealth;
    public int damage;
    public float attackSpeed;
    public float attackRange;
    public float moveSpeed;

    public int corpseHealth;
    public bool alive;


    public override float healthUnitInterval
    {
        get
        {
            return alive ? (float)health / maxHealth : (float)(maxCorpseHealth + health) / maxCorpseHealth;
        }
    }


    protected override void CheckForDeath()
    {
        if (alive)
        {
            if (health <= 0)
            {
                var agent = GetComponent<NavMeshAgent>();
                if (agent != null)
                    Destroy(agent);

                alive = false;

                if (maxCorpseHealth != 0)
                {
                    var rend = GetComponent<SpriteRenderer>();
                    if (rend == null)
                    {
                        rend = GetComponentInChildren<SpriteRenderer>();
                        rend.sprite = SpriteManager.instance.corpse;
                    }

                }

                //agent.isStopped = true;
            }
        }
        else
        {
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

}
