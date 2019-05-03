using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushSplatter : MonoBehaviour
{
    public AnimationCurve curve;
    public Rigidbody rigidBody;
    public float duration;

    public float intensity;

    float timer;
    

    private void Start()
    {
        Spew(intensity);
    }

    private void Update()
    {
        var time = (timer += Time.deltaTime) / duration;

        if (time >= 1f)
            Destroy(gameObject);
        else
            transform.localScale = Vector3.one * curve.Evaluate(time);


    }


    public void Spew(float intensity)
    {
        Vector2 rand = new Vector2(Random.value * intensity, Random.value * intensity);
        rigidBody.AddForce(rand.x - (intensity * 0.5f), 0f, rand.y - (intensity * 0.5f));
    }


    public void Init(MushType type)
    {
        GetComponent<SpriteRenderer>().sprite = SpriteManager.instance.GetSprite(type);
    }
}
