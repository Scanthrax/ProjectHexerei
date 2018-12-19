using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeDamage : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {

        IDamageable testInterface = collision.gameObject.GetComponent<IDamageable>();

        if (testInterface != null)
        {
            testInterface.DealDamage();
        }
    }
}
