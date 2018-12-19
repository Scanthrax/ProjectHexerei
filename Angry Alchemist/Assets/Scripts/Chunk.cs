using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// cannot serialize monobehaviour!

[Serializable]
public class Chunk
{
    public Sprite Empty, Full;

    public static int size = 10;
    Tile[,] tiles;

    public Position position;

    public Transform transform;

    public Chunk(Transform transform)
    {
        this.transform = transform;
        // mark the position of this chunk
        position = new Position(transform);

        //Random.InitState(42);

        // create the grid of tiles
        tiles = new Tile[size, size];



        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                tiles[i, j] = new Tile(i + position.x, j + position.y, 0);
                GameObject tileGO = new GameObject("T_" + (i + position.x) + "_" + (j + position.y));
                tileGO.transform.position = new Vector3(tiles[i, j].x - (size / 2), tiles[i, j].y - (size / 2));
                tileGO.transform.SetParent(transform,true);
                SpriteRenderer spriteRenderer = tileGO.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = UnityEngine.Random.Range(0,6) == 0 ? SpriteManager.instance.GetSprite(SpriteType.Grass) : SpriteManager.instance.GetSprite(SpriteType.Ground);
            }

        }
    }
}
