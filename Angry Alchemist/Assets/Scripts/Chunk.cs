using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Sprite Empty, Full;

    public static int size = 10;
    Tile[,] tiles;

    private void Awake()
    {
        //Random.InitState(42);

        tiles = new Tile[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                tiles[i, j] = new Tile(i + Mathf.RoundToInt(transform.position.x), j + Mathf.RoundToInt(transform.position.y), 0);
                GameObject tileGO = new GameObject("T_" + (i + Mathf.RoundToInt(transform.position.x)) + "_" + (j + Mathf.RoundToInt(transform.position.y)));
                tileGO.transform.position = new Vector3(tiles[i, j].x-5, tiles[i, j].y-5,tiles[i,j].z);
                tileGO.transform.SetParent(transform,true);
                SpriteRenderer spriteRenderer = tileGO.AddComponent<SpriteRenderer>();

                

                spriteRenderer.sprite = Random.Range(0,2) == 0 ? Full : Empty;
            }

        }
    }
}
