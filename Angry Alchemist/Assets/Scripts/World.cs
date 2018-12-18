using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour
{
    // keep track of the chunks that have been loaded
    Dictionary<Vector2, Chunk> chunkMap;

    // chunk GameObject
    public Chunk chunkGO;

    // the range of the chunk loading
    public int LoadRange = 4;


    Vector2 previousPos, currentPos, transitionPos;

    public bool generateChunks = false;

    private void Awake()
    {
        // init dictionary
        chunkMap = new Dictionary<Vector2, Chunk>();
    }


    private void Start()
    {
        previousPos = transform.position;
        currentPos = previousPos;

        FindChunksToLoad();

    }

    private void Update()
    {

        // record our previous position so we can compare it to our current position
        previousPos = currentPos;
        currentPos = transform.position;
        transitionPos = RoundTo10(currentPos);

        #region detect when we cross a transition line
        if(
            (previousPos.x < transitionPos.x && currentPos.x > transitionPos.x) ||
            (previousPos.x > transitionPos.x && currentPos.x < transitionPos.x) ||
            (previousPos.y < transitionPos.y && currentPos.y > transitionPos.y) ||
            (previousPos.y > transitionPos.y && currentPos.y < transitionPos.y))
        {
            generateChunks = true;
        }
        #endregion

        if(generateChunks)
        {
            FindChunksToLoad();
            DeleteChunks();
            generateChunks = false;
        }

    }

    /// <summary>
    /// Checks whether or not a chunk can be placed; if so, make & place the chunk
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void MakeChunkAt(int x, int y)
    {
        x += 5;
        y += 5;

        if(!chunkMap.ContainsKey(new Vector2(x,y)))
        {
            var chunk = Instantiate(chunkGO, new Vector3(x, y, 0), Quaternion.identity);
            chunkMap.Add(new Vector2(x, y), chunk.GetComponent<Chunk>());
        }
    }

    void DeleteChunks()
    {
        List<Chunk> deleteChunks = new List<Chunk>(chunkMap.Values);

            var xy = from kvp in deleteChunks where Vector2.Distance(transform.position, kvp.transform.position) > LoadRange * Chunk.size select kvp;
            foreach (var item in xy)
            {
                chunkMap.Remove(item.transform.position);
                Destroy(item.gameObject);
            }
    }

    void FindChunksToLoad()
    {
        var xPos = Mathf.RoundToInt(transform.position.x);
        var yPos = Mathf.RoundToInt(transform.position.y);

        for (int i =  RoundTo10(xPos - (LoadRange * Chunk.size)); i < RoundTo10(xPos + (LoadRange * Chunk.size)); i += Chunk.size)
        {
            for (int j = RoundTo10(yPos - (LoadRange * Chunk.size)); j < RoundTo10(yPos + (LoadRange * Chunk.size)); j += Chunk.size)
            {
                MakeChunkAt(i,j);
                print("X: " + i + "   Y: " + j);
            }
        }
    }



    int RoundTo10(int num)
    {
        int rem = num % 10;
        return rem >= 5 ? (num - rem + 10) : (num - rem);
    }

    int RoundTo10(float num)
    {
        int temp = Mathf.RoundToInt(num);
        int rem = temp % 10;
        return rem >= 5 ? (temp - rem + 10) : (temp - rem);
    }

    /// <summary>
    /// Round the values to the nearest multiple of 10
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    Vector2 RoundTo10(Vector2 num)
    {
        int tempX = Mathf.RoundToInt(num.x);
        int tempY = Mathf.RoundToInt(num.y);

        int remX = tempX % 10;
        int remY = tempY % 10;

        return new Vector2(
            remX >= 5 ? (tempX - remX + 10) : (tempX - remX),
            remY >= 5 ? (tempY - remY + 10) : (tempY - remY));
    }

}
