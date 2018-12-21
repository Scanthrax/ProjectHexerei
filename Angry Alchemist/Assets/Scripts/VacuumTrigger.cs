﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VacuumTrigger : MonoBehaviour
{
    public PolygonCollider2D pc;
    public BladeAcceleration ba;

    [Range(0.1f, 20)]
    public float suckLength;
    [Range(1, 30)]
    public float suckWidth;

    float modifiedLength;

    public float vacuumPower;

    List<Vector2> points;

    public bool isSucking = false;


    public Transform vacuum;

    void Start()
    {
        //points = new List<Vector2>();
        //points.Add(new Vector2(suckLength, suckWidth));
        //points.Add(new Vector2(suckLength, -suckWidth));
        //points.Add(new Vector2(-2.5f,-1f));
        //points.Add(new Vector2(-2.5f, 1f));


        vacuumPower = 0f;


    }

    // Update is called once per frame
    void Update()
    {
        //modifiedLength = suckLength * ba.animInterp;
        vacuumPower = ba.animInterp * 5f;

        //#region Update the vacuum's collider to match inspector variables
        //points[0] = new Vector2(modifiedLength, suckWidth);
        //points[1] = new Vector2(modifiedLength, -suckWidth);
        //pc.SetPath(0, points);
        //#endregion

        if(Input.GetMouseButton(1))
        {
            isSucking = true;
        }
        else
        {
            isSucking = false;
        }



        //List<Collider2D> list = new List<Collider2D>();
        //ContactFilter2D filter = new ContactFilter2D();
        //if(pc.OverlapCollider(filter, list) > 0)
        //{
        //    print(pc.OverlapCollider(filter, list));
        //}


    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{

    //        ISuckable testInterface = collision.gameObject.GetComponent<ISuckable>();

    //        if (testInterface != null)
    //        {
    //            testInterface.Suck(vacuum.position, vacuumPower);
    //        }
    //}

    #region Unused Gizmo
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
    #endregion
}



