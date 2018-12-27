using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    Transform sprite;

    Vector2 startTime;

    public float speed;

    public float wideness;


    public bool shake;

    void Start()
    {
        startTime = new Vector2(Random.value * 5f, Random.value * 5f);
        sprite = GetComponentInChildren<SpriteRenderer>().transform;
        shake = false;
    }

    void Update()
    {
        if (shake)
        {
            sprite.localPosition = new Vector3(Mathf.PerlinNoise(startTime.x + Time.time * speed, 0f), 0, Mathf.PerlinNoise(startTime.y + Time.time * speed, 0f)) * wideness;
        }
    }
}
