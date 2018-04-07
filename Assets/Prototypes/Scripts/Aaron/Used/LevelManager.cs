using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelEditor;

public class LevelManager : MonoBehaviour {


    GridBase gridBase;

    public List<GameObject> inSceneObject = new List<GameObject>();
    public List<GameObject> inSceneBuilding = new List<GameObject>();
    public List<GameObject> inSceneFences = new List<GameObject>();
    public List<GameObject> inSceneAnimals = new List<GameObject>();

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
        gridBase = GridBase.GetInstance();

        InitLevelObjects();
    }

    //Initialse pre placed objects
    void InitLevelObjects()
    {
        if(inSceneFences.Count > 0)
        {
            for (int i =0; i < inSceneFences.Count; i++)
            {
                Level_Object obj = inSceneFences[i].GetComponent<Level_Object>();
                obj.UpdateNode(gridBase.grid);
            }
        }
    }

    public void ClearLevel()
    {
        foreach (GameObject g in inSceneObject)
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
        

        inSceneObject.Clear();
        inSceneFences.Clear();
        inSceneAnimals.Clear();
    }


}
