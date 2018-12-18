using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;

    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed;
        }
    }
}
