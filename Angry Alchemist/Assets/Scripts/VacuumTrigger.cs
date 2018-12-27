using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class VacuumTrigger : MonoBehaviour
{
    public CapsuleCollider cc;
    public BladeAcceleration ba;

    [Range(0.1f, 20)]
    public float suckLength;


    float modifiedLength;

    public float vacuumPower;

    List<Vector2> points;

    public bool isSucking = false;


    public static VacuumTrigger instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


    void Start()
    {

        vacuumPower = 0f;


    }

    // Update is called once per frame
    void Update()
    {
        //modifiedLength = suckLength * ba.animInterp;
        vacuumPower = ba.animInterp * 5f;


        if (Input.GetMouseButton(1))
        {
            isSucking = true;
        }
        else
        {
            isSucking = false;
        }

    }


}



