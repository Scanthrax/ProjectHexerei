using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, ISuckable
{
    public void Suck()
    {
        print("I am being sucked!");
    }
}
