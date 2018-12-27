//Ron
//This script controls the blade of the muschmaschine

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to represent the different gears of the blade
/// </summary>
public enum Gear
{
    /// <summary>
    /// Null value
    /// </summary>
    Null,
    /// <summary>
    /// Zeroth gear; the blade is at a stop
    /// </summary>
    Zero,
    /// <summary>
    /// First gear
    /// </summary>
    First,
    /// <summary>
    /// Second gear
    /// </summary>
    Second,
    /// <summary>
    /// Third gear
    /// </summary>
    Third,
    /// <summary>
    /// Max gear; the blade is at full speed
    /// </summary>
    Max
}

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
    /// <summary>
    /// Amount of time (seconds) between each ascending gear
    /// </summary>
    public float upshiftTime;
    /// <summary>
    /// Amount of time (seconds) between each descending gear
    /// </summary>
    public float downshiftTime;
    /// <summary>
    /// The variable responsible for the time between shifting gears
    /// </summary>
    public float shiftTimer;
    /// <summary>
    /// Used to identify which gear the muschmaschine is currently in
    /// </summary>
    public Gear currentGear;
    /// <summary>
    /// Used to identify which gear the muschmaschine was in during the previous frame
    /// </summary>
    public Gear previousGear;

    /// <summary>
    /// Are we currently upshifting? Otherwise we're downshifting
    /// </summary>
    public bool upshifting;

    public GameObject Blade;


    public AnimationCurve animCurve;
    public float animInterp;

    public CircleCollider2D cc;


    public float tickTimer, tickTime;

    public bool DamageTick;

    public AudioSource sawIdle;
    public AudioSource sawActive;



    void Start()
    {
        // the blade starts idle
        bladeSpeed = 0f;

        // set the previous & current gears to Zeroth gear
        previousGear = Gear.Zero;
        currentGear = previousGear;

        // initialize the timer
        shiftTimer = 0f;


        cc = GetComponent<CircleCollider2D>();

    }

    void Update()
    {
        


        #region Right Mouse Release Release
        if (Input.GetMouseButtonUp(1))
        {
            upshifting = false;
        }
        #endregion
        
        #region Right Mouse Hold
        if (Input.GetMouseButton(1))
        {
            upshifting = true;
            
        }
        #endregion
        #region Right Mouse Up
        else
        {
            upshifting = false;
        }
        #endregion

        shiftTimer = Mathf.Clamp(
            shiftTimer += upshifting? Time.deltaTime / (upshiftTime * 3): -Time.deltaTime /(downshiftTime * 3),
            0f,upshiftTime * 3);

        animInterp = animCurve.Evaluate(shiftTimer);

        if (tickTimer >= tickTime)
        {
            print("tick!");
            tickTimer -= tickTime;
            DamageTick = true;
        }
        else
        {
            tickTimer += (Time.deltaTime * animInterp);
            DamageTick = false;
        }



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





        if (animInterp <= 0.1f)
        {
            sawIdle.enabled = true;
            sawActive.enabled = false;
        }
        else
        {
            sawIdle.enabled = false;
            sawActive.enabled = true;
            sawActive.pitch = 0.7f + (0.3f * animInterp);
        }





        bladeSpeed = animInterp * 30f;

        // rotate the blade
        Blade.transform.Rotate(0, 0,-bladeSpeed);

        // lastly, we set the previous gear to our current gear
        previousGear = currentGear;
    }

    /// <summary>
    /// Compares the previous gear with the current gear to detect when we shift gear.  The result is the gear we transition to
    /// </summary>
    /// <returns></returns>
    Gear ShiftGear()
    {
        // if the gears are not equal, that means we have changed gears.  Return the current gear
        if (previousGear != currentGear)
            return currentGear;
        // otherwise there is no change in gears.  Return a null enum
        else
            return Gear.Null;
    }
}
