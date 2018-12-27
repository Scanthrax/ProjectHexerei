using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public int health;
    public AudioSource source;
    public Transform player;
    public NavMeshAgent agent;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void DealDamage()
    {
        health--;
        source.Play();
        if (Random.Range(0, 3) == 0)
        {
            var temp = Instantiate(PlayerResource.instance.mush, transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)), Quaternion.Euler(90f, 0, 0)).GetComponent<Mush>();
            temp.Init(MushType.Creature);
        }


        if (health <= 0)
        {

            foreach (var item in transform.GetComponents(typeof(Component)))
            {
                if (item.GetType() == typeof(AudioSource) || item.GetType() == typeof(Transform))
                    continue;
                Destroy(item);
            }
            Destroy(gameObject, source.clip.length);
        }
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position,player.position) < 3f)
        {
            agent.SetDestination(player.position);
        }
    }
}
