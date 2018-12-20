using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { Dirt, Grass, Rock }

[System.Serializable]
public class Tile
{

    public Type type { get; private set; }

    public int x { get; private set; }
    public int y { get; private set; }
    public int z { get; private set; }

    public Tile(int x, int y, int z, Type type)
    {
        this.x = x;
        this.y = y;
        this.z = z;

        this.type = type;
    }

}
