using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suck : MonoBehaviour
{
    public VacuumTrigger vacuum;

    public Transform vacuumTransform;

    private void OnTriggerStay(Collider collision)
    {
        if (vacuum.isSucking)
        {
            ISuckable testInterface = collision.gameObject.GetComponent<ISuckable>();

            if (testInterface != null)
            {
                print("I should be sucking!");
                testInterface.Suck(vacuumTransform.position, vacuum.vacuumPower);
            }
        }
    }
}
