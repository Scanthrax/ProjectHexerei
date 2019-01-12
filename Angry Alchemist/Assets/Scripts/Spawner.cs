using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject demon;
    public int secondsBetweenSpawns;


    void Spawn()
    {
        Instantiate(demon, transform.position, Quaternion.identity, transform);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Time.frameCount % (secondsBetweenSpawns * 60) == 0)
            Spawn();
    }
}
