using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : EnemyParent
{
    public AudioSource source;
    Transform player;
    public NavMeshAgent agent;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }



    private void Update()
    {
        if (alive)
        {
            var dist = Vector3.Distance(transform.position, player.position);

            if (dist < 4f)
            {
                agent.SetDestination(player.position);

                if (dist < 2f)
                {
                    if (Time.frameCount % 14 == 0)
                    {
                        Player.instance.DealDamage(1);
                        Player.instance.SetHealthText();
                    }
                }

            }

        }

    }
}
