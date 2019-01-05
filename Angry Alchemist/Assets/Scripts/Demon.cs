using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Demon : DamageableParent, IDamageable
{
    public AudioSource source;
    Transform player;
    public NavMeshAgent agent;
    public bool alive;
    public SpriteRenderer sprite;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent.speed = moveSpeed;

        alive = true;

    }

    public void DealDamage()
    {
        health--;
        source.Play();
        //if (Random.Range(0, 3) == 0)
        //{
        //    var temp = Instantiate(PlayerResource.instance.mush, transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)), Quaternion.Euler(90f, 0, 0)).GetComponent<Mush>();
        //    temp.Init(MushType.Creature);
        //}


        if (health <= 0)
        {

            //foreach (var item in transform.GetComponents(typeof(Component)))
            //{
            //    if (item.GetType() == typeof(AudioSource) || item.GetType() == typeof(Transform))
            //        continue;
            //    Destroy(item);
            //}
            //Destroy(gameObject, source.clip.length);

            Destroy(agent);
            Player.instance.ReduceRage(3f);

            alive = false;
            sprite.sprite = SpriteManager.instance.corpse;
            //agent.isStopped = true;
        }

        if(health <= -maxCorpseHealth)
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
