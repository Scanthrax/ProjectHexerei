﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[RequireComponent(typeof(AudioSource))]
public class Plant : MonoBehaviour, IDamageable
{
    public AudioSource source;


    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = AudioManager.instance.GetRandomSound(AudioManager.instance.Splats);
    }

    public void DealDamage()
    {
        for (int i = 0; i < 1; i++)
        {
            var range = 1.5f;
            var temp = Instantiate(PlayerResource.instance.mush, transform.position + new Vector3(Random.Range(-range, range), 0f, Random.Range(-range, range)), Quaternion.Euler(90f, 0, 0)).GetComponent<Mush>();
            temp.Init(MushType.Plant);
        }


        foreach (var item in transform.GetComponents(typeof(Component)))
        {
            if (item.GetType() == typeof(AudioSource) || item.GetType() == typeof(Transform))
                continue;
            Destroy(item);
        }

        source.Play();
        Destroy(gameObject, source.clip.length);
    }
}