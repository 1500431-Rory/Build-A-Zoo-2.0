using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelEditor;

public class LevelManager : MonoBehaviour {


    GridBase gridBase;

    public List<GameObject> inSceneFoliage = new List<GameObject>();
    public List<GameObject> inSceneEnrichment = new List<GameObject>();
    public List<GameObject> inSceneFences = new List<GameObject>();
    public List<GameObject> inSceneAnimals = new List<GameObject>();
    public List<GameObject> inSceneCare = new List<GameObject>();


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

    //Initialse pre placed objects
    /*void InitLevelObjects()
    {
        if(inSceneGameObjects.Count > 0)
        {
            for (int i =0; i < inSceneGameObjects.Count; i++)
            {
                Level_Object obj = inSceneGameObjects[i].GetComponent<Level_Object>();
                obj.UpdateNode(gridBase.grid);
            }
        }
    }*/

    public void ClearLevel()
    {
        foreach(GameObject g in inSceneFoliage)
        {
            Destroy(g);
        }
        foreach (GameObject g in inSceneEnrichment)
        {
            Destroy(g);
        }
        foreach (GameObject g in inSceneFences)
        {
            Destroy(g);
        }
        foreach (GameObject g in inSceneAnimals)
        {
            Destroy(g);
        }
        foreach (GameObject g in inSceneCare)
        {
            Destroy(g);
        }

        inSceneFoliage.Clear();
        inSceneEnrichment.Clear();
        inSceneFences.Clear();
        inSceneAnimals.Clear();
        inSceneCare.Clear();

    }


}
