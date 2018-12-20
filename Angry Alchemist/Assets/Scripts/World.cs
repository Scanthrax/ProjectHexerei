using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class World : MonoBehaviour
{
    // keep track of the chunks that have been loaded
    public Dictionary<Position, Chunk> chunkMap;

    public Dictionary<Chunk, Transform> chunkTransform;

    // the range of the chunk loading
    public int LoadRange = 4;


    Vector2 previousPos, currentPos, transitionPos;

    public bool generateChunks = false;

    public Transform player;


    public static int size = 10;

    private void Awake()
    {
        // init dictionary
        chunkMap = new Dictionary<Position, Chunk>();
        chunkTransform = new Dictionary<Chunk, Transform>();
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

    void FindChunksToLoad()
    {
        var xPos = Mathf.RoundToInt(player.transform.position.x);
        var yPos = Mathf.RoundToInt(player.transform.position.y);

        for (int i = RoundTo10(xPos - (LoadRange * size)); i < RoundTo10(xPos + (LoadRange * size)); i += size)
        {
            for (int j = RoundTo10(yPos - (LoadRange * size)); j < RoundTo10(yPos + (LoadRange * size)); j += size)
            {
                MakeChunkAt(i, j);
            }
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
            Chunk chunk = new Chunk(x,y,size);
            chunkMap.Add(new Position(x, y), chunk);
            chunkTransform.Add(chunk, chunkGO.transform);
            MakeChunkTiles(chunk);
        }
    }

    void DeleteChunks()
    {
            List<Chunk> deleteChunks = new List<Chunk>(chunkMap.Values);

            var xy = from kvp in deleteChunks where Vector2.Distance(player.transform.position, chunkTransform[kvp].position) > LoadRange * size select kvp;
            foreach (var item in xy)
            {
                Destroy(chunkTransform[item].gameObject);
                //chunkMap.Remove(item.position);
                //chunkTransform.Remove(item);
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



    void MakeChunkTiles(Chunk chunk)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                chunk.tiles[i, j] = new Tile(i + chunk.position.x, j + chunk.position.y, 0);
                GameObject tileGO = new GameObject("T_" + (i + chunk.position.x) + "_" + (j + chunk.position.y));
                tileGO.transform.position = new Vector3(chunk.tiles[i, j].x - (size / 2), chunk.tiles[i, j].y - (size / 2));
                tileGO.transform.SetParent(transform, true);
                SpriteRenderer spriteRenderer = tileGO.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = UnityEngine.Random.Range(0, 6) == 0 ? SpriteManager.instance.GetSprite(SpriteType.Grass) : SpriteManager.instance.GetSprite(SpriteType.Ground);
            }

        }
    }
}
