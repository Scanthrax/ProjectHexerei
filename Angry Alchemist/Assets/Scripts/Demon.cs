using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Demon : DamageableParent
{
    public AudioSource source;
    Transform player;
    public NavMeshAgent agent;
    public SpriteRenderer sprite;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent.speed = moveSpeed;

        alive = true;

    }

    


    private void Update()
    {
        if (alive)
        {
            var dist = Vector3.Distance(transform.position, player.position);


            agent.SetDestination(player.position);

            if (dist < attackRange)
            {
                if (Time.frameCount % attackSpeed == 0)
                {
                    Player.instance.DealDamage(2);
                    Player.instance.SetHealthText();
                }
            }
        }



    }
}
