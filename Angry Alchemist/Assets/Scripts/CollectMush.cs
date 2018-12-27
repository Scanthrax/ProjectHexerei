using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMush : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        ISuckable temp = collision.GetComponent<ISuckable>();
        if (temp != null)
        {
            temp.Consume();
        }
    }
}
