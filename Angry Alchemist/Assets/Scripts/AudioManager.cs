using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;


    public AudioClip[] Splats;
    public AudioClip[] WalkGravel;
    public AudioClip[] SuckMush;
    public AudioClip[] Whoosh;
    public AudioClip LoadPotion;

    public AudioClip GetRandomSound(AudioClip[] library)
    {
        return library[Random.Range(0, library.Length)];
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


}
