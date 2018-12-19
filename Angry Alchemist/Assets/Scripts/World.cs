using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour
{
    // keep track of the chunks that have been loaded
    public Dictionary<Position, Chunk> chunkMap;

    // the range of the chunk loading
    public int LoadRange = 4;


    Vector2 previousPos, currentPos, transitionPos;

    public bool generateChunks = false;

    public Transform player;

    private void Awake()
    {
        // init dictionary
        chunkMap = new Dictionary<Position, Chunk>();
    }


    private void Start()
    {
        previousPos = player.transform.position;
        currentPos = previousPos;

        FindChunksToLoad();
    }

    private void Update()
    {

        // record our previous position so we can compare it to our current position
        previousPos = currentPos;
        currentPos = player.transform.position;
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



        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveController.instance.SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveController.instance.LoadGame();
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

        if(!chunkMap.ContainsKey(new Position(x,y)))
        {
            var chunkGO = Instantiate(new GameObject(), new Vector3(x, y, 0), Quaternion.identity);
            Chunk chunk = new Chunk(chunkGO.transform);
            chunkMap.Add(new Position(x, y), chunk);
        }
    }

    void DeleteChunks()
    {
        List<Chunk> deleteChunks = new List<Chunk>(chunkMap.Values);

            var xy = from kvp in deleteChunks where Vector2.Distance(player.transform.position, kvp.transform.position) > LoadRange * Chunk.size select kvp;
            foreach (var item in xy)
            {
                chunkMap.Remove(item.position);
                Destroy(item.transform.gameObject);
            }
    }

    void FindChunksToLoad()
    {
        var xPos = Mathf.RoundToInt(player.transform.position.x);
        var yPos = Mathf.RoundToInt(player.transform.position.y);

        for (int i =  RoundTo10(xPos - (LoadRange * Chunk.size)); i < RoundTo10(xPos + (LoadRange * Chunk.size)); i += Chunk.size)
        {
            for (int j = RoundTo10(yPos - (LoadRange * Chunk.size)); j < RoundTo10(yPos + (LoadRange * Chunk.size)); j += Chunk.size)
            {
                MakeChunkAt(i,j);
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
