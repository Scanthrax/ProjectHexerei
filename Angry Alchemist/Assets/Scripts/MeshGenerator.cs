using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(MeshFilter))]
//[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    //Vector3[] vertices;
    //int[] triangles;
    //Vector2[] uv;


    //Mesh mesh;
    //new MeshRenderer renderer;
    public Texture2D[] tileTexture;
    public int xSize = 10;
    public int zSize = 10;

    public int pixelsPerUnit = 64;

    float randomNoise;

    public static MeshGenerator instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);


        randomNoise = Random.value * 100f;
    }


    void Start () {
        //mesh = new Mesh();
        //GetComponent<MeshFilter>().mesh = mesh;
        //renderer = GetComponent<MeshRenderer>();
        //CreateShape();


        

    }


    public void CreateShape(Mesh mesh, MeshRenderer renderer, Chunk chunk)
    {
        #region Vertices
        Vector3[] vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int z = 0, i = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }
        #endregion

        #region Triangles
        int vert = 0;
        int tris = 0;
        int[] triangles = new int[xSize * zSize* 6];
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }
        #endregion

        #region UV
        Vector2[] uv = new Vector2[vertices.Length];
        for (int z = 0, i = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                uv[i] = new Vector2((float)x / xSize, (float)z / zSize);
                i++;
            }
        }
        #endregion

        #region Generate the texture

        Texture2D texture = new Texture2D(pixelsPerUnit * xSize,pixelsPerUnit * zSize);

        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                //var temp = Mathf.PerlinNoise(randomNoise + ((renderer.transform.position.x + x) * 0.12f), randomNoise + ((renderer.transform.position.z + z) * 0.12f));
                //var temp2 = temp <= 0.35f ? tileTexture[0] : tileTexture[1];

                var temp3 = SpriteManager.instance.GetSprite(chunk.tiles[x, z].type).texture;

                texture.SetPixels(x*pixelsPerUnit, z*pixelsPerUnit, pixelsPerUnit, pixelsPerUnit, temp3.GetPixels());
            }
        }
        

        texture.filterMode = FilterMode.Point;
        texture.Apply();
        renderer.materials[0].mainTexture = texture;
        #endregion


        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();


    }

}
