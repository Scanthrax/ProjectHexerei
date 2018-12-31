using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDamage : MonoBehaviour
{
    SpriteRenderer renderer;
    float newAlpha;
    private void OnTriggerStay(Collider other)
    {
        IDamageable temp = other.GetComponent<IDamageable>();
        if(temp != null)
        {
            temp.DealDamage();
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
