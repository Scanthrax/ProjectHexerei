using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mush : MonoBehaviour, ISuckable
{
    public void Suck(Vector2 pos, float power)
    {
        print("I am being sucked!");
        transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * power);
    }
}
