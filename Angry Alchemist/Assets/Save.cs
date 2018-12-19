using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;

[Serializable]
public class Save
{
    //[SerializeField]
    //public Dictionary<Vector2, Chunk> chunkMap;

    [SerializeField]
    public Position playerPosition;
}




[Serializable]
public struct Position
{
    public float x;
    public float y;

    public Position(Transform obj)
    {
        x = obj.position.x;
        y = obj.position.y;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(x, y);
    }
}
