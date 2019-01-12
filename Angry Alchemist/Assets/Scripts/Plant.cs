using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[RequireComponent(typeof(AudioSource))]
public class Plant : DamageableParent
{
    public AudioSource source;


    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = AudioManager.instance.GetRandomSound(AudioManager.instance.Splats);
    }

}