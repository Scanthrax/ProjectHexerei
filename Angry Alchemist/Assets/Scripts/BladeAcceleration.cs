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

    float stoppingPoint;

    void Start()
    {
        // the blade starts idle
        bladeSpeed = 0f;

        // set the previous & current gears to Zeroth gear
        previousGear = Gear.Zero;
        currentGear = previousGear;

        // initialize the timer
        shiftTimer = 0f;

    }

    void Update()
    {
        #region Right Mouse Release Release
        if(Input.GetMouseButtonUp(1))
        {
            stoppingPoint = shiftTimer;
            upshifting = false;
        }
        #endregion

        var temp = downshiftTime * ((stoppingPoint / (upshiftTime * 2)));

        #region Right Mouse Hold
        if (Input.GetMouseButton(1))
        {
            upshifting = true;
            shiftTimer = Mathf.Min(shiftTimer += Time.fixedDeltaTime, upshiftTime * 2f);
        }
        #endregion
        #region Right Mouse Up
        else
        {
            
            shiftTimer = Mathf.Max(shiftTimer -= Time.fixedDeltaTime / temp, 0f);
            upshifting = false;
        }
        #endregion

        #region Determine our current gear
        if (shiftTimer > 0f && shiftTimer < upshiftTime)
            currentGear = Gear.First;
        else if (shiftTimer >= upshiftTime && shiftTimer < upshiftTime * 2f)
            currentGear = Gear.Second;
        else if (shiftTimer >= upshiftTime * 2f)
            currentGear = Gear.Max;
        else if (shiftTimer <= 0f)
            currentGear = Gear.Zero;
        else
        {
            currentGear = Gear.Null;
            print("Current Gear is at a null value!");
        }
        #endregion

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

        // use the switch statement to change the blade speed upon shifting gears
        switch (ShiftGear())
        {
            case Gear.Zero:
                bladeSpeed = 0;
                break;
            case Gear.First:
                bladeSpeed = maxSpeed * (0.4f);
                break;
            case Gear.Second:
                bladeSpeed = maxSpeed * (0.75f);
                break;
            case Gear.Max:
                bladeSpeed = maxSpeed;
                break;
            default:
                break;
        }



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
