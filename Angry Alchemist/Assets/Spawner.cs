using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject demon;

    void Spawn()
    {
        Instantiate(demon, transform.position, Quaternion.identity, transform);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Time.frameCount % 150 == 0)
            Spawn();
    }
}
