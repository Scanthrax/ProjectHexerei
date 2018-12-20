using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[Serializable]
public class Chunk
{
    // each chunk needs a grid that is the appropriate size
    public Tile[,] tiles;

    // each chunk has a position in world space
    public Position position;

    public Chunk(int x, int y, int size)
    {
        // mark the position of this chunk
        position = new Position(x,y);

        //Random.InitState(42);

        // create the grid of tiles
        tiles = new Tile[size, size];
    }
}
