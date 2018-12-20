﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IDamageable
{
    public GameObject mush;


    public void DealDamage()
    {
        print("I am being hit!");
        for (int i = 0; i < 6; i++)
        {
            Instantiate(mush, transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f)),Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public void Suck(Vector2 pos, float power)
    {
        print("I am being sucked!");
        transform.position = Vector2.MoveTowards(transform.position, pos, Time.deltaTime * power);
    }
}