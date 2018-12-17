using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gear { None,First,Second,Max}

public class BladeAcceleration : MonoBehaviour
{
    float bladeSpeed;
    public float maxSpeed;
    public float shiftTime;
    public Gear gear;

    // Start is called before the first frame update
    void Start()
    {
        bladeSpeed = 0f;
        gear = Gear.None;
    }

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            float speed;

            if (gear == Gear.None)
                gear = Gear.First;

            switch(gear)
            {
                case Gear.First:
                    speed = maxSpeed * (1 / 3);
                    break;
                case Gear.Second:
                    speed = maxSpeed * (2 / 3);
                    break;
                case Gear.Max:
                    speed = maxSpeed;
                    break;
                default:
                    speed = 0;
                    break;
            }

            bladeSpeed += speed;

            Mathf.Min(bladeSpeed, maxSpeed);
        }






        transform.Rotate(0, 0,-bladeSpeed);
    }
}
