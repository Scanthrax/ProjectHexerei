using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    Transform cameraPivot;
    public Vector2[] dist = new Vector2[2];


    bool moveCam = false;


    float time = 0f;
    public float AmountOftime;
    Vector3 startingPoint;

    public Texture2D cursor;

    private void Start()
    {
        //Cursor.SetCursor(cursor, new Vector2(cursor.width/2,cursor.height/2), CursorMode.Auto);

        cameraPivot = transform.parent;
    }

    private void Update()
    {


        cameraPivot.position = new Vector3(Mathf.Clamp(cameraPivot.position.x, player.position.x - dist[0].x, player.position.x + dist[0].y), cameraPivot.position.y, Mathf.Clamp(cameraPivot.position.z, player.position.z - dist[1].x, player.position.z + dist[1].y));

        if (Input.GetMouseButtonDown(2))
        {
            moveCam = true;
            startingPoint = transform.position;
        }

        if (moveCam)
        {
            time += Time.deltaTime;
            var ratio = Mathf.Min( time / AmountOftime, 1f);

            cameraPivot.position = Vector3.Lerp(
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
