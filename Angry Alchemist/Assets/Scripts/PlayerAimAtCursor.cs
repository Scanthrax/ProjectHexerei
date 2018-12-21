using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimAtCursor : MonoBehaviour
{
    void Update()
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var newMouse = new Vector3(mouse.x, 0, mouse.y);
        var offset = new Vector2(newMouse.x - screenPoint.x, newMouse.z - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(90, -angle, 0);
    }
}
