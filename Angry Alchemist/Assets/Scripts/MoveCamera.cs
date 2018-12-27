using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;

    public Vector2[] dist = new Vector2[2];


    bool moveCam = false;


    float time = 0f;
    public float AmountOftime;
    Vector3 startingPoint;

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

        if (Input.GetMouseButtonDown(2))
        {
            moveCam = true;
            startingPoint = transform.position;
        }

        if (moveCam)
        {
            time += Time.deltaTime;
            var ratio = Mathf.Min( time / AmountOftime, 1f);

            transform.position = Vector3.Lerp(
                new Vector3(startingPoint.x, startingPoint.y, startingPoint.z),
                new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z),
                ratio);

            

            if (ratio >= 1f)
            {
                time = 0f;
                moveCam = false;
            }
        }

    }
}
