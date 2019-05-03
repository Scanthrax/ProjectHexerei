using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fade : MonoBehaviour
{
    Text renderer;
    float newAlpha;

    private void Start()
    {
        renderer = GetComponent<Text>();
        newAlpha = renderer.color.a;
    }


    private void Update()
    {
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, newAlpha -= 0.03f);



        transform.localScale -= Vector3.one * 0.045f;

        if (newAlpha <= 0f || transform.localScale.x <= 0f)
            Destroy(gameObject);

    }
}
