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


    Vector3 previousPos, currentPos, transitionPos;

    public bool generateChunks = false;

    public Transform player;


    public GameObject plant;

    public GameObject mush;

    public GameObject rock;

    public GameObject bush;

    public GameObject enemy;
    public static int size = 10;


    public Transform ChunkContainer;

    System.Type[] components;

    float randomNoise;

    private void Awake()
    {
        // init dictionary
        chunkMap = new Dictionary<Position, Chunk>();
        chunkTransform = new Dictionary<Chunk, Transform>();
    }


    private void Start()
    {
        randomNoise = Random.value * 1000f;
        components = new System.Type[2];
        components[0] = typeof(MeshFilter);
        components[1] = typeof(MeshRenderer);

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
            (previousPos.z < transitionPos.z && currentPos.z > transitionPos.z) ||
            (previousPos.z > transitionPos.z && currentPos.z < transitionPos.z))
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



        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    SaveController.instance.SaveGame();
        //}
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    SaveController.instance.LoadGame();
        //}

    }

    void FindChunksToLoad()
    {
        var xPos = Mathf.RoundToInt(currentPos.x);
        var zPos = Mathf.RoundToInt(currentPos.z);

        for (int i = RoundTo10(xPos - (LoadRange * size)); i < RoundTo10(xPos + (LoadRange * size)); i += size)
        {
            for (int j = RoundTo10(zPos - (LoadRange * size)); j < RoundTo10(zPos + (LoadRange * size)); j += size)
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
    void MakeChunkAt(int x, int z)
    {
        x += size / 2;
        z += size / 2;

        if(!chunkMap.ContainsKey(new Position(x,0,z)))
        {
            GameObject chunkGO = new GameObject("C_" + x + "_" + z, components);
            chunkGO.transform.position = new Vector3(x, 0, z);
            chunkGO.transform.SetParent(ChunkContainer);
            Chunk chunk = new Chunk(x,0,z,size);
            chunkMap.Add(new Position(x, 0, z), chunk);
            chunkTransform.Add(chunk, chunkGO.transform);
            MakeChunkTiles(chunk);
            GenerateMeshes(chunkGO, chunk);
        }
    }

    void DeleteChunks()
    {
            List<Chunk> deleteChunks = new List<Chunk>(chunkMap.Values);

            var xy = from kvp in deleteChunks where Vector2.Distance(player.transform.position, chunkTransform[kvp].position) > LoadRange * size select kvp;
            foreach (var item in xy)
            {
                //Destroy(chunkTransform[item].gameObject);
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
    Vector3 RoundTo10(Vector3 num)
    {
        int tempX = Mathf.RoundToInt(num.x);
        int tempY = Mathf.RoundToInt(num.z);

        int remX = tempX % 10;
        int remY = tempY % 10;

        return new Vector3(
            remX >= 5 ? (tempX - remX + 10) : (tempX - remX),
            num.y,
            remY >= 5 ? (tempY - remY + 10) : (tempY - remY));
    }



    void MakeChunkTiles(Chunk chunk)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                // make a new tile
                var tile = new Tile(new Position(chunk.position.x + i, 0, chunk.position.z + j), chunk);
                // equation for environmental noise
                var noise = Mathf.PerlinNoise(randomNoise + ((chunk.position.x + i) * 0.11f), randomNoise + ((chunk.position.z + j) * 0.11f));
                tile.type = noise <= 0.65f ? Type.Grass : Type.Dirt;
                // save each tile in the chunk
                chunk.tiles[i, j] = tile;
            }
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                //if(j != size-1)
                //    chunk.tiles[i, j].adjacent[(int)Direction.North] = chunk.tiles[i, j + 1];
                //if (i != size-1)
                //    chunk.tiles[i, j].adjacent[(int)Direction.East] = chunk.tiles[i + 1, j];
                //if (j != 0)
                //    chunk.tiles[i, j].adjacent[(int)Direction.South] = chunk.tiles[i, j - 1];
                //if (i != 0)
                //    chunk.tiles[i, j].adjacent[(int)Direction.West] = chunk.tiles[i - 1, j];

                SpawnItems(chunk, i, j);
            }
        }
    }




    void SpawnItems(Chunk chunk,int i, int j)
    {
        if (Random.value < 0.05f)
        {
            var temp3 = Instantiate(plant, new Vector3(chunk.position.x + i + 0.5f, 0, chunk.position.z + j + 0.5f), Quaternion.Euler(90, 0, 0));
            temp3.transform.SetParent(chunkTransform[chunk]);
        }

        if (Random.value < 0.03f)
        {
            var temp4 = Instantiate(mush, new Vector3(chunk.position.x + i + 0.5f, 0, chunk.position.z + j + 0.5f), Quaternion.Euler(90, 0, 0)).GetComponent<Mush>();
            temp4.Init(MushType.Mineral);
            temp4.transform.SetParent(chunkTransform[chunk]);
        }

        if (Random.value < 0.03f)
        {
            //temp.Solid = true;
            var temp5 = Instantiate(rock, new Vector3(chunk.position.x + i + 0.5f, 0, chunk.position.z + j + 0.5f), Quaternion.identity);
            temp5.transform.SetParent(chunkTransform[chunk]);
        }

        if (Random.value < 0.03f)
        {
            //temp.Solid = true;
            var temp5 = Instantiate(bush, new Vector3(chunk.position.x + i + 0.5f, 0, chunk.position.z + j + 0.5f), Quaternion.identity);
            temp5.transform.SetParent(chunkTransform[chunk]);
        }

        if (Random.value < 0.005f)
        {
            //temp.Solid = true;
            var temp5 = Instantiate(enemy, new Vector3(chunk.position.x + i + 0.5f, 0, chunk.position.z + j + 0.5f), Quaternion.identity);
            temp5.transform.SetParent(chunkTransform[chunk]);
        }
    }


    void GenerateMeshes(GameObject go, Chunk chunk)
    {
        Mesh mesh = new Mesh();
        go.GetComponent<MeshFilter>().mesh = mesh;
        MeshRenderer renderer = go.GetComponent<MeshRenderer>();
        MeshGenerator.instance.CreateShape(mesh,renderer,chunk);
    }


}
