using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;

    public Vector2[] dist = new Vector2[2];

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += Vector3.up * speed;
            transform.position += Vector3.forward * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.position += Vector3.down * speed;
            transform.position += Vector3.back * speed;
        }


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, player.position.x - dist[0].x, player.position.x + dist[0].y), transform.position.y, Mathf.Clamp(transform.position.z, player.position.z - dist[1].x, player.position.z + dist[1].y));

    }
}
