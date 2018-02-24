using System.Collections;
using System.Collections.Generic;
//using EpPathFinding.cs;
using UnityEngine;

//Creates The Grid Base for the Level(all the cubes)
public class GridBase : MonoBehaviour {

    public GameObject nodePrefab;
    public GameObject grassPrefab;
    public int sizeX;
    public int sizeZ;

    //Offset changed depending on scale of cubes
    public int offset = 1;

    public Node[,] grid;
    

    private static GridBase instance = null;
    public static GridBase GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        CreateGrid();
        CreateMouseCollision();
    }

    //Generates an Array of Objects to use as the floor of the Level
    void CreateGrid()
    {
        grid = new Node[sizeX, sizeZ];

        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {

                float posX = x * offset;
                float posZ = z * offset;

                GameObject go = Instantiate(nodePrefab, new Vector3(posX, 0, posZ), Quaternion.identity) as GameObject;
                go.transform.parent = transform.GetChild(1).transform;

                NodeObject nodeObj = go.GetComponent<NodeObject>();
                nodeObj.posX = x;
                nodeObj.posZ = z;

                Node node = new Node();
                node.vis = go;
                node.tileRenderer = node.vis.GetComponentInChildren<MeshRenderer>();
                node.isWalkable = true;
                node.nodePosX = x;
                node.nodePosZ = z;
                grid[x, z] = node;
            }
        }

        //spawning grass blades
        /*for (int s = 0; s < 2; s++)
        {
            for (int x = 0; x < sizeX - 10; x++)
            {
                for (int z = 0; z < sizeZ - 10; z++)
                {

                    float posXx = x * offset;
                    float posZz = z * offset;

                    int[] randomRot = new int[]{ 0, 0,90,90 };
                    GameObject grassBlades = Instantiate(grassPrefab, new Vector3(Random.Range(0, sizeX * offset), 0, Random.Range(0, sizeZ * offset)), Quaternion.Euler(0, randomRot[Random.Range(0, randomRot.Length)], 0)) as GameObject;
                }
            }
        }*/
    }

    //Creates a box collider on top of the Cubes as generating a collider on each cube would cause more "stress"
    void CreateMouseCollision()
    {
        GameObject go = new GameObject();
        go.AddComponent<BoxCollider>();
        go.GetComponent<BoxCollider>().size = new Vector3(sizeX * offset, 0.1f, sizeZ * offset);
        go.transform.position = new Vector3((sizeX * offset) / 2 - 1, 0, (sizeZ * offset) / 2 - 1);
    }

    //Finds Location of each Cube, to be used for finding mousepositions
    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float worldX = worldPosition.x;
        float worldZ = worldPosition.z;

        worldX /= offset;
        worldZ /= offset;

        int x = Mathf.RoundToInt(worldX);
        int z = Mathf.RoundToInt(worldZ);

        if (x > sizeX)
        {
            x = sizeX;
        }
        if (z > sizeZ)
        {
            z = sizeZ;
        }
        if (x < 0)
        {
            x = 0;
        }
        if (z < 0)
        {
            z = 0;
        }

        return grid[x, z];

    }

}
