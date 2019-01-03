using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spew : MonoBehaviour
{

    public static void SpewThings(GameObject go,Transform parent, Vector3 position,int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var temp = Instantiate(go, position, Quaternion.identity, parent);
            temp.GetComponent<Rigidbody>().AddForce(0, 0, 0);
        }
    }
}
