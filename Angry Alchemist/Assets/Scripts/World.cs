using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    // keep track of the chunks that have been loaded
    Dictionary<Vector2, Chunk> chunkMap;

    // chunk GameObject
    public Chunk chunkGO;

    // the range of the chunk loading
    public int LoadRange = 2;


    Vector2 prevPos, curPos, transPos;


    private void Awake()
    {
        // init dictionary
        chunkMap = new Dictionary<Vector2, Chunk>();
    }


    private void Start()
    {
        prevPos = transform.position;
        curPos = prevPos;
    }

    private void Update()
    {
        // constantly check for which chunks to load
        FindChunksToLoad();

        // record our previous position so we can compare it to our current position
        prevPos = curPos;
        curPos = transform.position;
        transPos = RoundNum(curPos);

        if(prevPos.x < transPos.x && curPos.x > transPos.x)
        {
            print("crossed transition!");
            transPos.x += 10;
        }
        else if (prevPos.x > transPos.x && curPos.x < transPos.x)
        {
            print("crossed transition!");
            transPos.x -= 10;
        }

        if (prevPos.y < transPos.y && curPos.y > transPos.y)
        {
            print("crossed transition!");
            transPos.y += 10;
        }
        else if (prevPos.y > transPos.y && curPos.y < transPos.y)
        {
            print("crossed transition!");
            transPos.y -= 10;
        }

    }

    /// <summary>
    /// Checks whether or not a chunk can be placed; if so, make & place the chunk
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void MakeChunkAt(int x, int y)
    {
        x = RoundNum((x / Chunk.size) * Chunk.size);
        y = RoundNum((y / Chunk.size) * Chunk.size);

        if(!chunkMap.ContainsKey(new Vector2(x,y)))
        {
            var chunk = Instantiate(chunkGO, new Vector3(x, y, 0), Quaternion.identity);
            chunkMap.Add(new Vector2(x, y), chunk.GetComponent<Chunk>());
        }
    }

    void DeleteChunkAt(int x, int y)
    {

    }

    void FindChunksToLoad()
    {
        var xPos = transform.position.x+5;
        var yPos = transform.position.y+5;

        for (int i =  RoundNum(xPos - (LoadRange * Chunk.size)); i < RoundNum(xPos + (LoadRange * Chunk.size)); i += Chunk.size)
        {
            for (int j = RoundNum(yPos - (LoadRange * Chunk.size)); j < RoundNum(yPos + (LoadRange * Chunk.size)); j += Chunk.size)
            {
                MakeChunkAt(i,j);
            }
        }
    }



    int RoundNum(int num)
    {
        int rem = num % 10;
        return rem >= 5 ? (num - rem + 10) : (num - rem);
    }

    int RoundNum(float num)
    {
        int temp = Mathf.RoundToInt(num);
        int rem = temp % 10;
        return rem >= 5 ? (temp - rem + 10) : (temp - rem);
    }

    Vector2 RoundNum(Vector2 num)
    {
        int tempX = Mathf.RoundToInt(num.x);
        int tempY = Mathf.RoundToInt(num.y);

        int remX = tempX % 10;
        int remY = tempY % 10;

        return new Vector2(remX >= 5 ? (tempX - remX + 10) : (tempX - remX), remY >= 5 ? (tempY - remY + 10) : (tempY - remY));
    }

}
