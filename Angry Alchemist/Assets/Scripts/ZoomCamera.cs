using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{

    public KeyCode CameraZoom;
    Camera cam;
    public float startSize;
    public float increment;
    public Vector2 MinMaxSize;

    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = startSize;
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y != 0 && Input.GetKey(CameraZoom))
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                cam.orthographicSize -= increment;
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                cam.orthographicSize += increment;
            }

            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, MinMaxSize.x, MinMaxSize.y);



        }
    }
}
