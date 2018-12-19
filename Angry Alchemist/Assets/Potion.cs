using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public AnimationCurve animCurve;

    public Vector3 start,end;

    public float time = 0f;
    public bool launchPotion = false;
    public bool overHand;

    void Update()
    {
        if(launchPotion)
        {
            transform.position = Vector2.Lerp(start, end, time += Time.deltaTime);
        }
        if(overHand)
        {
            transform.localScale = Vector3.one + ((Vector3.one * 2f) * animCurve.Evaluate(time));
        }
    }


    public void SetStartAndEnd(Vector3 end, bool overHand)
    {
        start = transform.position;
        this.end = end;
        launchPotion = true;
        this.overHand = overHand;
    }

}
