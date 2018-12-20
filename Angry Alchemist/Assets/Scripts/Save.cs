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
    public int x;
    public int y;

    public Position(Transform obj)
    {
        x = Mathf.RoundToInt(obj.position.x);
        y = Mathf.RoundToInt(obj.position.y);
    }
    public Position(float x, float y)
    {
        this.x = Mathf.RoundToInt(x);
        this.y = Mathf.RoundToInt(y);
    }
    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(x, y);
    }
}
