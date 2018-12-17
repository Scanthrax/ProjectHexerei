using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public enum Type { Empty, Full}
    Type type;

    public int x { get; private set; }
    public int y { get; private set; }
    public int z { get; private set; }

    public Tile(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;

        type = Type.Full;

    }

}
