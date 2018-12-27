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
    [SerializeField]
    public Dictionary<Position, Chunk> chunkMap;
    [SerializeField]
    public Dictionary<Chunk, Transform> chunkTransform;
    [SerializeField]
    public Position playerPosition;
}




[Serializable]
public struct Position
{
    public int x { get; private set; }
    public int y { get; private set; }
    public int z { get; private set; }

    public Position(Transform obj)
    {
        x = Mathf.RoundToInt(obj.position.x);
        y = Mathf.RoundToInt(obj.position.y);
        z = Mathf.RoundToInt(obj.position.z);
    }
    public Position(Vector3 obj)
    {
        x = Mathf.RoundToInt(obj.x);
        y = Mathf.RoundToInt(obj.y);
        z = Mathf.RoundToInt(obj.z);
    }
    public Position(float x, float y, float z)
    {
        this.x = Mathf.RoundToInt(x);
        this.y = Mathf.RoundToInt(y);
        this.z = Mathf.RoundToInt(z);
    }
    public Position(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}
