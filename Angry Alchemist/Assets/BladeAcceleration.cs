//Ron
//This script controls the blade of the muschmaschine

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gear { None,First,Second,Max}

public class BladeAcceleration : MonoBehaviour
{
    /// <summary>
    /// The current speed of the blade
    /// </summary>
    public float bladeSpeed;
    /// <summary>
    /// The maximum speed of the blade
    /// </summary>
    public float maxSpeed;


    public float maxRate;

    /// <summary>
    /// Amount of time (seconds) between each shift in gear
    /// </summary>
    public float upshiftTime;
    /// <summary>
    /// Amount of time (seconds) from Max gear to None
    /// </summary>
    public float downshiftTime;
    /// <summary>
    /// The timer responsible for the shifting between gears
    /// </summary>
    public float shiftTimer;
    /// <summary>
    /// Used to identify which gear the muschmaschine is currently in
    /// </summary>
    public Gear gear;

    public bool upshifting;

    float stoppingPoint;

    // Start is called before the first frame update
    void Start()
    {
        // the blade starts idle
        bladeSpeed = 0f;
        gear = Gear.None;
        // initialize the timer
        shiftTimer = 0f;
    }

    void Update()
    {

        if(Input.GetMouseButtonUp(1))
        {
            stoppingPoint = shiftTimer;
            upshifting = false;
        }

        #region Right Mouse Hold
        if (Input.GetMouseButton(1))
        {
            upshifting = true;
            shiftTimer = Mathf.Min(shiftTimer += Time.deltaTime, upshiftTime * 2f);
        }
        #endregion

        #region Right Mouse Up
        else
        {
            var temp = downshiftTime * ((stoppingPoint / (upshiftTime*2)));
            shiftTimer = Mathf.Max(shiftTimer -= Time.deltaTime / temp, 0f);
            upshifting = false;
        }
        #endregion

        if (shiftTimer > 0f && shiftTimer < upshiftTime)
            gear = Gear.First;
        else if (shiftTimer >= upshiftTime && shiftTimer < upshiftTime * 2f)
            gear = Gear.Second;
        else if (shiftTimer >= upshiftTime * 2f)
            gear = Gear.Max;
        else if (shiftTimer <= 0f)
            gear = Gear.None;


        //switch(gear)
        //{
        //    case Gear.None:
        //        break;
        //    case Gear.First:
        //        break;
        //    case Gear.Second:
        //        break;
        //    case Gear.Max:
        //        break;
        //    default:
        //        break;
        //}





        transform.Rotate(0, 0,-bladeSpeed);
    }
}
