using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimate : MonoBehaviour
{
    public float framesPerSecond = 10;
    SpriteRenderer renderer;
    public Sprite[] splash;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        int index = Mathf.RoundToInt(Time.time * framesPerSecond) % splash.Length;
        renderer.sprite = splash[index];
    }
}
