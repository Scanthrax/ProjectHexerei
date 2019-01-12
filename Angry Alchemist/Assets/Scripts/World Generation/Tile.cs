using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { Dirt, Grass, Rock }
public enum Direction { North, East, South, West}
[System.Serializable]
public class Tile
{

    public Type type;

    public Position position { get; private set; }

    public Chunk ParentChunk;

    public Tile[] adjacent = new Tile[4];

    public bool Solid = false;


    public Tile(Position pos, Type type)
    {
        position = pos;
        this.type = type;
    }

    public Tile(Position pos)
    {
        position = pos;
    }
    public Tile(Position pos, Chunk parent)
    {
        position = pos;
        ParentChunk = parent;
    }

}
