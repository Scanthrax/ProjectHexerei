using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimate : MonoBehaviour
{
    public Sprite[] frames;
    public float framesPerSecond = 10;
    SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        int index = Mathf.RoundToInt(Time.time * framesPerSecond) % frames.Length;
        renderer.sprite = frames[index];
    }
}
