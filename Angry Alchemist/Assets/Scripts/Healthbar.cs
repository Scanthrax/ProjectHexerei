using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public DamageableParent parent;


    private void Start()
    {
        //print(parent.healthUnitInterval);
    }

    private void Update()
    {
        transform.localPosition = new Vector3((-0.5f * parent.healthUnitInterval), transform.localPosition.y, transform.localPosition.z);
        transform.localScale = new Vector3(parent.healthUnitInterval, transform.localScale.y, transform.localScale.z);
    }
}
