using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Vector2 rotationSpeed;
    float rotation;

    private void Start()
    {
        rotation = Random.Range(rotationSpeed.x, rotationSpeed.y);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0,0,transform.rotation.eulerAngles.z + rotation)); 
    }
}
