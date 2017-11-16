using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using LevelEditor;
using System;
using UnityEngine;

public class Level_SaveLoad : MonoBehaviour {

    public List<String> availableLevels = new List<string>();

    private List<SaveableLevelObject> saveLevelObjects_List = new List<SaveableLevelObject>();
    private List<SaveableLevelObject> saveStackableLevelObjects_List = new List<SaveableLevelObject>();
    private List<NodeObjectSaveable> saveNodeObjectsList = new List<NodeObjectSaveable>();
    private List<WallObjectSaveable> saveWallsList = new List<WallObjectSaveable>();

    public static string saveFolderName = "LevelObjects";

    LevelManager lvlManager;

    void Start()
    {
        lvlManager = LevelManager.GetInstance();
    }

    public void SaveLevelButton(string levelName)
    {
        SaveLevel(levelName);
        InterfaceManager.GetInstance().ReloadFiles();
    }

    public void LoadLevelButton(string levelName)
    {
        LoadLevel(levelName);
    }

    static string SaveLocation(string LevelName, bool save = false)
    {
        string saveLocation = Application.streamingAssetsPath + "/Levels/";

        if(!Directory.Exists(saveLocation))
        {
            Directory.CreateDirectory(saveLocation);
        }

        if (save)
        {
          return saveLocation +"lvl_" + LevelName;
        }
        else
        {
            return saveLocation + LevelName;
        }
        
    }

    void SaveLevel(string saveName)
    {
        Level_Object[] levelObjects = FindObjectsOfType<Level_Object>();

        saveLevelObjects_List.Clear();
        saveWallsList.Clear();
        saveStackableLevelObjects_List.Clear();

        foreach(Level_Object lvlObj in levelObjects)
        {
            if (!lvlObj.isWallObject)
            {
                if (!lvlObj.isStackableObj)
                {
                    saveLevelObjects_List.Add(lvlObj.GetSaveableObject());
                }
                else
                {
                    saveStackableLevelObjects_List.Add(lvlObj.GetSaveableObject());
                }
            }
            else
            {
                WallObjectSaveable w = new WallObjectSaveable();
                w.levelObject = lvlObj.GetSaveableObject();
                w.wallObject = lvlObj.GetComponent<Level_WallObj>().GetSaveable();

                saveWallsList.Add(w);
            }
        }

        NodeObject[] nodeObjects = FindObjectsOfType<NodeObject>();
        saveNodeObjectsList.Clear();
        
        foreach(NodeObject nodeObject in nodeObjects)
        {
            saveNodeObjectsList.Add(nodeObject.GetSaveable());
        }

        LevelSaveable levelSave = new LevelSaveable();
        levelSave.saveLevelObjects_List = saveLevelObjects_List;
        levelSave.saveStackableLevelObjects_List = saveStackableLevelObjects_List;
        levelSave.saveNodeObjectsList = saveNodeObjectsList;
        levelSave.saveWallsList = saveWallsList;

        string saveLocation = SaveLocation(saveName);

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(saveLocation, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, levelSave);
        stream.Close();

        Debug.Log(saveLocation);
    }

    bool LoadLevel(string saveName)
    {
        bool retVal = true;

        string saveFile = SaveLocation(saveName);

        if (!File.Exists(saveFile))
        {
            retVal = false;
        }
        else
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(saveFile, FileMode.Open);

            LevelSaveable save = (LevelSaveable)formatter.Deserialize(stream);

            stream.Close();
            LoadLevelActual(save);
        }
        return retVal;
    }

    void LoadLevelActual(LevelSaveable levelSaveable)
    {

        LevelManager.GetInstance().ClearLevel();

        #region Create Level Objects
        for(int i =0; i < levelSaveable.saveLevelObjects_List.Count; i++)
        {
            SaveableLevelObject s_obj = levelSaveable.saveLevelObjects_List[i];

            Node nodeToPlace = GridBase.GetInstance().grid[s_obj.posX, s_obj.posZ];

            GameObject go = Instantiate(
                ResourcesManager.GetInstance().GetObjBase(s_obj.obj_Id).objPrefab,
                nodeToPlace.vis.transform.position,
                Quaternion.Euler(s_obj.rotX, s_obj.rotY, s_obj.rotZ)) as GameObject;

            nodeToPlace.placedObj = go.GetComponent<Level_Object>();
            nodeToPlace.placedObj.gridPosX = nodeToPlace.nodePosX;
            nodeToPlace.placedObj.gridPosZ = nodeToPlace.nodePosZ;
            nodeToPlace.placedObj.worldRotation = nodeToPlace.placedObj.transform.localEulerAngles;

            lvlManager.inSceneGameObjects.Add(go);
            go.transform.parent = lvlManager.objHolder.transform;
        }
        #endregion

        #region Create Stackable Level Objects
        for (int i = 0; i < levelSaveable.saveStackableLevelObjects_List.Count; i++)
        {
            SaveableLevelObject s_obj = levelSaveable.saveStackableLevelObjects_List[i];

            Node nodeToPlace = GridBase.GetInstance().grid[s_obj.posX, s_obj.posZ];

            GameObject go = Instantiate(
                ResourcesManager.GetInstance().GetStackObjBase(s_obj.obj_Id).objPrefab,
                nodeToPlace.vis.transform.position,
                Quaternion.Euler(s_obj.rotX, s_obj.rotY, s_obj.rotZ)) as GameObject;

            Level_Object stack_obj = go.GetComponent<Level_Object>();
            stack_obj.gridPosX = nodeToPlace.nodePosX;
            stack_obj.gridPosZ = nodeToPlace.nodePosZ;

            nodeToPlace.stackedObjs.Add(stack_obj);

            lvlManager.inSceneStackObjects.Add(go);
            go.transform.parent = lvlManager.objHolder.transform;
        }
        #endregion

        #region Painted Tiles
        for (int i = 0; i < levelSaveable.saveNodeObjectsList.Count; i++)
        {

            // levelSaveable.saveNodeObjectsList[i];

            Node node = GridBase.GetInstance().grid[levelSaveable.saveNodeObjectsList[i].posX, levelSaveable.saveNodeObjectsList[i].posZ];

            node.vis.GetComponent<NodeObject>().UpdateNodeObject(node, levelSaveable.saveNodeObjectsList[i]);
            
        }
        #endregion

        #region Create Walls
        for (int i = 0; i < levelSaveable.saveWallsList.Count; i++)
        {
            WallObjectSaveable s_wall = levelSaveable.saveWallsList[i];

            Node nodeToPlace = GridBase.GetInstance().grid[s_wall.levelObject.posX, s_wall.levelObject.posZ];

            GameObject go = Instantiate(
                ResourcesManager.GetInstance().wallPrefab,nodeToPlace.vis.transform.position,Quaternion.identity) as GameObject;

            Level_Object lvlObj = go.GetComponent<Level_Object>();
            lvlObj.gridPosX = nodeToPlace.nodePosX;
            lvlObj.gridPosZ = nodeToPlace.nodePosZ;

            Level_WallObj wall_Obj = go.GetComponent<Level_WallObj>();

            wall_Obj.UpdateWall(s_wall.wallObject.direction);
            wall_Obj.UpdateCorners(s_wall.wallObject.corner_a, s_wall.wallObject.corner_b, s_wall.wallObject.corner_c);

            lvlManager.inSceneWalls.Add(go);
            go.transform.parent = lvlManager.wallHolder.transform;
        }
        #endregion

    }

    [Serializable]
    public class LevelSaveable
    {
        public List<SaveableLevelObject> saveLevelObjects_List;
        public List<SaveableLevelObject> saveStackableLevelObjects_List;
        public List<NodeObjectSaveable> saveNodeObjectsList;
        public List<WallObjectSaveable> saveWallsList;
    }

    [Serializable]
    public class WallObjectSaveable
    {
        public SaveableLevelObject levelObject;
        public WallObjectSaveableProperties wallObject;
    }

    public void LoadAllFileLevels()
    {
        DirectoryInfo dirInfo = new DirectoryInfo(Application.streamingAssetsPath + "/Levels");
        FileInfo[] fileInfo = dirInfo.GetFiles();

        foreach (FileInfo f in fileInfo)
        {
            string[] readName = f.Name.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

            if(readName.Length  == 2)
            {
                if (string.Equals("lvl", readName[0]))
                {
                    string[] noMeta = readName[1].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);

                    if(noMeta.Length == 1)
                    {
                        availableLevels.Add(f.Name);
                    }
                }
            }
        }
    }

    public static Level_SaveLoad instance;
    public static Level_SaveLoad GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        LoadAllFileLevels();
    }


}

