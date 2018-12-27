using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimAtCursor : MonoBehaviour
{


    public float mouseAngle;
    public float currentAngle;
    public float speed;
    private void Start()
    {
        currentAngle = mouseAngle;
    }

    void Update()
    {
        

        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var newMouse = new Vector3(mouse.x, 0, mouse.y);
        var offset = new Vector2(newMouse.x - screenPoint.x, newMouse.z - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        mouseAngle = angle;

        var newAngle = Mathf.MoveTowardsAngle(currentAngle, mouseAngle, Time.deltaTime * speed);
        currentAngle = newAngle;

        transform.rotation = Quaternion.Euler(0, -currentAngle, 0);
    }
}
