using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// GameObjects that derive from this class can be damaged and will die at 0 health
/// </summary>
public class DamageableParent : MonoBehaviour
{
    [Header("Initialize")]
    public int maxHealth;

    [HideInInspector]
    public int health;


    public virtual float healthUnitInterval
    {
        get
        {
            return (float)health / maxHealth;
        }
    }

    protected virtual void CheckForDeath()
    {
        // check if we have died
        if (health <= 0)
        {
            //foreach (var item in transform.GetComponents(typeof(Component)))
            //{
            //    if (item.GetType() == typeof(AudioSource) || item.GetType() == typeof(Transform))
            //        continue;
            //    Destroy(item);
            //}

            //var source = GetComponent<AudioSource>();
            //if (source != null)
            //    Destroy(gameObject, source.clip.length);
            //else
            //    Destroy(gameObject);
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        health = maxHealth;
    }

    public virtual void DealDamage(int damage)
    {
        // reduce the health
        health -= damage;
        // spew the floating combat text
        Spew.instance.SpewThings(UIManager.instance.combatText, transform.position, 1,damage,Color.gray);

        CheckForDeath();
    }

    


}
