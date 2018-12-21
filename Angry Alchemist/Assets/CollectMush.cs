using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMush : MonoBehaviour
{
    public PlayerResource playerResource;
    public VacuumTrigger vacuum;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (vacuum.isSucking)
        {
            ISuckable temp = collision.GetComponent<ISuckable>();
            if (temp != null)
            {
                temp.Consume();
            }
        }
    }
}
