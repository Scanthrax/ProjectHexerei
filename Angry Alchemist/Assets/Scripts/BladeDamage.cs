using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeDamage : MonoBehaviour
{

    public BladeAcceleration ba;

    private void OnTriggerStay(Collider collision)
    {
        if (VacuumTrigger.instance.vacuumPower > 0.1f && ba.DamageTick)
        {
            DamageableParent testInterface = collision.gameObject.GetComponent<DamageableParent>();

            if (testInterface != null)
            {
                print("dealing damage!");
                testInterface.DealDamage(1);
            }

            ChopMush chop = collision.GetComponent<ChopMush>();
            if(chop != null)
            {
                if (Random.value * 100f <= chop.percent)
                {
                    var temp = Instantiate(PlayerResource.instance.mush, transform.position + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)), Quaternion.Euler(90f, 0, 0)).GetComponent<Mush>();
                    temp.Init(chop.mush);
                }
            }


        }

        Shake shake = collision.GetComponent<Shake>();
        AudioSource source = collision.GetComponent<AudioSource>();

        if (VacuumTrigger.instance.vacuumPower > 0.1f)
        {
            if (shake != null)
            {
                if (shake.shake != true)
                    shake.shake = true;
            }

            if ( source != null)
            {
                if (!source.isPlaying)
                    source.Play();

            }
        }
        else
        {
            if (source != null)
            {
                if (source.isPlaying)
                    source.Stop();

            }

            if (shake != null)
            {
                if (shake.shake != false)
                    shake.shake = false;
            }
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    Shake shake = other.GetComponent<Shake>();
    //    Rock rock = other.GetComponent<Rock>();
    //    if (shake != null)
    //    {
    //        shake.shake = true;
    //    }

    //    if(rock != null)
    //    {
    //        rock.source.Play();
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        Shake shake = other.GetComponent<Shake>();
        if (shake != null)
        {
            shake.shake = false;
        }

        AudioSource source = other.GetComponent<AudioSource>();
        if (source != null)
        {
            source.Stop();
        }
    }
}
