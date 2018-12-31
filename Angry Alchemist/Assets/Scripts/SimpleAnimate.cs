using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimate : MonoBehaviour
{
    public PotionObject potion;
    public float framesPerSecond = 10;
    SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        int index = Mathf.RoundToInt(Time.time * framesPerSecond) % potion.splash.Length;
        renderer.sprite = potion.splash[index];
    }
}
