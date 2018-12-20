using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public PotionObject potion;

    public AnimationCurve overhandCurve;
    public AnimationCurve underhandCurve;

    public Vector3 start,end;

    public float time = 0f;
    public bool launchPotion = false;
    public bool overHand;

    public GameObject potionExplode;


    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = potion.image;
    }
    void Update()
    {
        if(launchPotion)
        {
            transform.position = Vector2.Lerp(start, end, time += Time.deltaTime);
        }

        if(overHand)
        {
            transform.localScale = Vector3.one + ((Vector3.one * 2f) * overhandCurve.Evaluate(time));
        }
        else
        {
            transform.localScale = Vector3.one + ((Vector3.one * 2f) * underhandCurve.Evaluate(time));
        }

        if (time >= 1f)
        {
            Explode();
        }
    }


    public void SetStartAndEnd(Vector3 end, bool overHand)
    {
        start = transform.position;
        this.end = end;
        launchPotion = true;
        this.overHand = overHand;
    }



    public void Explode()
    {
        Instantiate(potionExplode, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
