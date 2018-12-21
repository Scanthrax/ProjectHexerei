using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeDamage : MonoBehaviour
{
    public VacuumTrigger vacuum;

    private void OnTriggerStay(Collider collision)
    {
        if (vacuum.isSucking)
        {
            IDamageable testInterface = collision.gameObject.GetComponent<IDamageable>();

            if (testInterface != null)
            {
                testInterface.DealDamage();
            }
        }
    }
}
