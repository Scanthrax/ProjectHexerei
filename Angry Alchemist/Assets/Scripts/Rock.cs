using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : DamageableParent
{
    public AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
}
