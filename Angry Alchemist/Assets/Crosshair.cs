using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    Transform MouseCrosshair;
    public Transform LimitedCrosshair;
    public Transform player;
    public float range;

    public static Crosshair instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        MouseCrosshair = transform;
    }

    void Update()
    {
        var MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosition = new Vector3(MousePosition.x, 0, MousePosition.z);

        MouseCrosshair.position = MousePosition;

        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //var allowedPos = mousePos - initialPos;
        //allowedPos = Vector3.ClampMagnitude(allowedPos, 2.0);
        //transform.position = initialPos + allowedPos;

        var allowedPos = MousePosition - player.position;
        allowedPos = Vector3.ClampMagnitude(allowedPos, range);

        LimitedCrosshair.transform.position = player.position + allowedPos;


    }
}
