using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VacuumTrigger : MonoBehaviour
{
    public PolygonCollider2D pc;

    [Range(0.1f, 20)]
    public float suckLength;
    [Range(1, 30)]
    public float suckWidth;

    List<Vector2> points;

    void Start()
    {
        points = new List<Vector2>();


        points.Add(new Vector2(suckLength, suckWidth));
        points.Add(new Vector2(suckLength, -suckWidth));
        points.Add(new Vector2(-2.5f,-1f));
        points.Add(new Vector2(-2.5f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        points[0] = new Vector2(suckLength, suckWidth);
        points[1] = new Vector2(suckLength, -suckWidth);

        pc.SetPath(0, points);
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.blue;

    //    for (int i = 0, j = 0; i < points.Count; i++)
    //    {
    //        if (i == points.Count - 1) j = 0;
    //        else j = i + 1;

    //        Gizmos.DrawLine(transform.position + Vector2ToVector3(points[i]), transform.position + Vector2ToVector3(points[j]));
    //    }
        
    //}

    //Vector3 Vector2ToVector3(Vector2 vec2)
    //{
    //    return new Vector3(vec2.x, vec2.y);
    //}
}



