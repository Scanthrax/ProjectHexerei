using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDamage : MonoBehaviour
{
    SpriteRenderer renderer;
    float newAlpha;
    private void OnTriggerStay(Collider other)
    {
        DamageableParent temp = other.GetComponent<DamageableParent>();
        if(temp != null)
        {
            temp.DealDamage(1);
        }
    }

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        newAlpha = renderer.color.a;
    }


    private void Update()
    {
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, newAlpha -= 0.01f);

        if (newAlpha <= 0.2f)
            Destroy(gameObject);
    }

}
