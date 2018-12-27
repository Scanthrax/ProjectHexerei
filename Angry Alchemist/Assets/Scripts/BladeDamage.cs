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
            IDamageable testInterface = collision.gameObject.GetComponent<IDamageable>();

            if (testInterface != null)
            {
                print("dealing damage!");
                testInterface.DealDamage();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Shake shake = other.GetComponent<Shake>();
        if(shake != null)
        {
            shake.shake = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Shake shake = other.GetComponent<Shake>();
        if (shake != null)
        {
            shake.shake = false;
        }
    }
}
