using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spew : MonoBehaviour
{
    public static Spew instance;

    public Transform canvas;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void SpewThings(GameObject go, Vector3 position,int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var temp = Instantiate(go, position, Quaternion.Euler(90,0,0), canvas);
            temp.GetComponent<Rigidbody>().AddForce(Random.value * 100f, 0, 100);
        }
    }
}
