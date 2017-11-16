using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelEditor;

public class LevelManager : MonoBehaviour {


    GridBase gridBase;

    public List<GameObject> inSceneGameObjects = new List<GameObject>();
    public List<GameObject> inSceneWalls = new List<GameObject>();
    public List<GameObject> inSceneStackObjects = new List<GameObject>();
    public List<GameObject> inSceneWallObjects = new List<GameObject>();

    [HideInInspector]
    public GameObject wallHolder;
    [HideInInspector]
    public GameObject objHolder;

    private static LevelManager instance = null;
    public static LevelManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;   
    }

     void Start()
    {
        wallHolder = new GameObject();
        wallHolder.name = "Wall Holder";
        objHolder = new GameObject();
        objHolder.name = "Obj Holder";

        gridBase = GridBase.GetInstance();

        //InitLevelObjects();
    }

    void InitLevelObjects()
    {
        if(inSceneGameObjects.Count > 0)
        {
            for (int i =0; i < inSceneGameObjects.Count; i++)
            {
                Level_Object obj = inSceneGameObjects[i].GetComponent<Level_Object>();
                obj.UpdateNode(gridBase.grid);
            }
        }
    }

    public void ClearLevel()
    {
        foreach(GameObject g in inSceneGameObjects)
        {
            Destroy(g);
        }
        foreach (GameObject g in inSceneStackObjects)
        {
            Destroy(g);
        }
        foreach (GameObject g in inSceneWalls)
        {
            Destroy(g);
        }
        foreach (GameObject g in inSceneWallObjects)
        {
            Destroy(g);
        }

        inSceneGameObjects.Clear();
        inSceneStackObjects.Clear();
        inSceneWalls.Clear();
        inSceneWallObjects.Clear();

    }


}
