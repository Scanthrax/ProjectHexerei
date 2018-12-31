using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectMush : MonoBehaviour
{

    public VacuumTrigger vacuum;

    private void OnTriggerEnter(Collider collision)
    {
        if (vacuum.vacuumPower > 0.1f)
        {
            ISuckable temp = collision.GetComponent<ISuckable>();
            if (temp != null)
            {
                temp.Consume();
            }
        }
    }
}
