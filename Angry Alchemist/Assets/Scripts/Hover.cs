using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [Tooltip("The values that the wave will oscillate between; X is min, Y is max")]
    /// <summary>
    /// The values that the wave will oscillate between; X is min, Y is max
    /// </summary>
    public Vector2 MinMaxScale;
    [Tooltip("The speed multiplier that adjusts the speed of oscillation")]
    /// <summary>
    /// The speed multiplier that adjusts the speed of oscillation
    /// </summary>
    public float speedMult;

    /// <summary>
    /// The time at which we start oscillating
    /// </summary>
    float startTime;

    public bool isHovering;

    private void Start()
    {
        // capture the start time when the object is instantiated
        startTime = Time.time;

        SetHover(true);
    }

    void Update()
    {

        if (isHovering)
        {
            transform.localScale = Vector3.one * (
                // set the minimum point of the oscillation
                MinMaxScale.x + (((
                // find a point along the sin wave by starting from our startTime and incrementing as time goes on.  the time is multiplied by our speedMult variable
                Mathf.Sin(startTime + (Time.time * speedMult))
                // offset the wave (-1 to 1) by halving it (-0.5 to 0.5) and moving it up (0 to 1)
                * 0.5f) + 0.5f)
                // find the difference between the minimum scale & maximum scale so the sin wave will oscillate between both values
                * (MinMaxScale.y - MinMaxScale.x)));
        }
    }


    public void SetHover(bool hover)
    {
        isHovering = hover;
    }
}
