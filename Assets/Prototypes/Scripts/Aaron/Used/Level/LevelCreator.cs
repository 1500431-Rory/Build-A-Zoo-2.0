using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{

    //Code for All Level Creation/Editing
    public class LevelCreator : MonoBehaviour
    {
        LevelManager manager;
        GridBase gridBase;
        InterfaceManager ui;



        public float totalMoney;

        //place obj variables
        bool hasObj;
        GameObject objToPlace;
        GameObject cloneObj;
        Level_Object objProperties;
        Vector3 mousePosition;
        Vector3 worldPosition;
        bool deleteObj;

        //place obj variables
        bool hasWallObj;
        GameObject wallObjToPlace;
        GameObject cloneWallObj;
        Level_Object wallObjProperties;
        bool deleteWallObj;


        //Terrain variable
        bool hasMaterial;
        bool paintTile;
        public Material matToPlace;
        Node previousNode;
        Material prevMaterial;
        Quaternion targetRot;
        Quaternion prevRotation;

        //place stack objs variables
        bool placeStackObj;
        GameObject stackObjToPlace;
        GameObject stackCloneObj;
        Level_Object stackObjProperties;
        bool deleteStackObj;

        /*//Wall creator variables
        bool createWall;
        
        GameObject wallObject;
        Node startNode_Wall;
        Node endNodeWall;
        // public Material[] wallPlacementMat;
        bool deleteWall;
        */
        //place ecnlsoure variables
        bool hasEnclosure;
        GameObject enclosureToPlace;
        GameObject cloneEnclosure;
        Level_Object enclosureProperties;
        bool deleteEnclosure;
        public GameObject enclosureObject;
        public GameObject[] enclosure;
        int totalPlaced;
        GameObject actualWallPlaced;
        GameObject actualCornerPlaced;
        public GameObject wallPrefab;
        public GameObject wallCornerPrefab;

        Vector3 nodePosN;
        Vector3 nodePosS;
        Vector3 nodePosE;
        Vector3 nodePosW;

        Vector3 nodePosNE;
        Vector3 nodePosSE;
        Vector3 nodePosNW;
        Vector3 nodePosSW;

        bool cornerCheck;

        

        RaycastHit hit;

        void Start()
        {
            gridBase = GridBase.GetInstance();
            manager = LevelManager.GetInstance();
            ui = InterfaceManager.GetInstance();

            PaintAll();


            /*for (int i = 0; i < 9; i++)
            {
                wallObject = Instantiate(wallPrefab, wallPos[i], Quaternion.identity) as GameObject;
            }*/
        }

        void Update()
        {
            PlaceObject();
            PaintTile();
            DeleteObjs();
            PlaceStackedObj();
            //CreateWall();
            DeleteStackedObjs();
            //DeleteWallsActual();
            PlaceWallObject();
            DeleteWallObjs();
            PlaceEnclosure();
            DeleteEnclosures();

        }

        void UpdateMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                mousePosition = hit.point;

            }
        }

        #region Place Objects
        void PlaceObject()
        {
            if (hasObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                worldPosition = curNode.vis.transform.position;

                if (cloneObj == null)
                {
                    cloneObj = Instantiate(objToPlace, worldPosition, Quaternion.identity) as GameObject;
                    objProperties = cloneObj.GetComponent<Level_Object>();
                }
                else
                {
                    cloneObj.transform.position = worldPosition;
                    if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                    {
                        if (curNode.placedObj != null)
                        {
                            manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                            Destroy(curNode.placedObj.gameObject);
                            curNode.placedObj = null;
                        }

                        GameObject actualObjPlaced = Instantiate(objToPlace, worldPosition, cloneObj.transform.rotation) as GameObject;
                        Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();

                        placedObjProperties.gridPosX = curNode.nodePosX;
                        placedObjProperties.gridPosZ = curNode.nodePosZ;
                        curNode.placedObj = placedObjProperties;
                        manager.inSceneGameObjects.Add(actualObjPlaced);
                        totalMoney -= placedObjProperties.price;
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        objProperties.ChangeRotation();
                    }
                }
            }
            else
            {
                if (cloneObj != null)
                {
                    Destroy(cloneObj);
                }
            }
        }

        public void PassGameObjectToPlace(string objId)
        {
            if (cloneObj != null)
            {
                Destroy(cloneObj);
            }

            CloseAll();
            hasObj = true;
            cloneObj = null;
            objToPlace = ResourcesManager.GetInstance().GetObjBase(objId).objPrefab;
        }
        void DeleteObjs()
        {
            if (deleteObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.placedObj != null)
                    {
                        if (manager.inSceneGameObjects.Contains(curNode.placedObj.gameObject))
                        {
                            manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                            Destroy(curNode.placedObj.gameObject);
                            totalMoney += curNode.placedObj.price;
                        }
                        curNode.placedObj = null;
                    }
                }
            }
        }
        public void DeleteObj()
        {
            CloseAll();
            deleteObj = true;
        }
        #endregion

        #region Stack Objects

        public void PassStackedObjectToPlace(string objId)
        {
            if (stackCloneObj != null)
            {
                Destroy(stackCloneObj);
            }

            CloseAll();
            placeStackObj = true;
            stackCloneObj = null;
            stackObjToPlace = ResourcesManager.GetInstance().GetStackObjBase(objId).objPrefab;
        }

        void PlaceStackedObj()
        {
            if (placeStackObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                worldPosition = curNode.vis.transform.position;

                if (stackCloneObj == null)
                {
                    stackCloneObj = Instantiate(stackObjToPlace, worldPosition, Quaternion.identity) as GameObject;
                    stackObjProperties = stackCloneObj.GetComponent<Level_Object>();
                }
                else
                {
                    stackCloneObj.transform.position = worldPosition;

                    if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                    {
                        GameObject actualObjPlaced = Instantiate(stackObjToPlace, worldPosition, stackCloneObj.transform.rotation) as GameObject;
                        Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();

                        placedObjProperties.gridPosX = curNode.nodePosX;
                        placedObjProperties.gridPosZ = curNode.nodePosZ;
                        curNode.stackedObjs.Add(placedObjProperties);
                        manager.inSceneStackObjects.Add(actualObjPlaced);
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        stackObjProperties.ChangeRotation();
                    }
                }
            }
            else
            {
                if (stackCloneObj != null)
                {
                    Destroy(stackCloneObj);
                }
            }
        }

        void DeleteStackedObjs()
        {
            if (deleteStackObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.stackedObjs.Count > 0)
                    {
                        for (int i = 0; i < curNode.stackedObjs.Count; i++)
                        {
                            if (manager.inSceneStackObjects.Contains(curNode.stackedObjs[i].gameObject))
                            {
                                manager.inSceneStackObjects.Remove(curNode.stackedObjs[i].gameObject);
                                Destroy(curNode.stackedObjs[i].gameObject);
                            }
                        }
                        curNode.stackedObjs.Clear();
                    }
                }
            }
        }
        public void DeleteStackedObj()
        {
            CloseAll();
            deleteStackObj = true;
        }
        #endregion

        #region Tile Painting

        void PaintTile()
        {
            if (hasMaterial)
            {
                UpdateMousePosition();
                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (previousNode == null)
                {
                    previousNode = curNode;
                    prevMaterial = previousNode.tileRenderer.material;
                    prevRotation = previousNode.vis.transform.rotation;
                }
                else
                {
                    if (previousNode != curNode)
                    {
                        if (paintTile)
                        {
                            int matId = ResourcesManager.GetInstance().GetMaterialID(matToPlace);
                            curNode.vis.GetComponent<NodeObject>().textureid = matId;
                            paintTile = false;
                        }
                        else
                        {
                            previousNode.tileRenderer.material = prevMaterial;
                            previousNode.vis.transform.rotation = prevRotation;
                        }

                        previousNode = curNode;
                        prevMaterial = curNode.tileRenderer.material;
                        prevRotation = curNode.vis.transform.rotation;
                    }
                }

                curNode.tileRenderer.material = matToPlace;
                curNode.vis.transform.localRotation = targetRot;

                if (Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                {
                    paintTile = true;
                }

                if (Input.GetMouseButtonUp(1))
                {
                    Vector3 eulerAngles = curNode.vis.transform.eulerAngles;
                    eulerAngles += new Vector3(0, 90, 0);
                    targetRot = Quaternion.Euler(eulerAngles);
                }
            }
        }

        public void PassMaterialToPaint(int matId)
        {
            deleteObj = false;
            placeStackObj = false;
            hasObj = false;
            matToPlace = ResourcesManager.GetInstance().GetMaterial(matId);
            hasMaterial = true;
        }

        public void PaintAll()
        {
            for (int x = 0; x < gridBase.sizeX; x++)
            {
                for (int z = 0; z < gridBase.sizeZ; z++)
                {
                    gridBase.grid[x, z].tileRenderer.material = matToPlace;
                    int matId = ResourcesManager.GetInstance().GetMaterialID(matToPlace);
                    gridBase.grid[x, z].vis.GetComponent<NodeObject>().textureid = matId;
                }
            }
            previousNode = null;
        }

        #endregion

        /*#region Wall Creator

        public void OpenWallCreation()
        {
            CloseAll();
            createWall = true;
        }

        void CreateWall()
        {
            if (createWall)
            {
                UpdateMousePosition();
                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
                worldPosition = curNode.vis.transform.position;

                if (startNode_Wall == null)
                {
                    if (Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                    {
                        startNode_Wall = curNode;
                    }
                }
                else
                {
                    if (Input.GetMouseButtonUp(0) && !ui.mouseOverUIElement)
                    {
                        endNodeWall = curNode;
                    }
                }

                if (startNode_Wall != null && endNodeWall != null)
                {
                    int difX = endNodeWall.nodePosX - startNode_Wall.nodePosX;
                    int difZ = endNodeWall.nodePosZ - startNode_Wall.nodePosZ;

                    CreateWallInNode(startNode_Wall.nodePosX, startNode_Wall.nodePosZ, Level_WallObj.WallDirection.ab);

                    Node finalXNode = null;
                    Node finalZNode = null;

                    //if (difX == 0 && difZ == 0)
                    //{
                    //    CreateWallOrUpdateNode(endNodeWall, Level_WallObj.WallDirection.all);
                    //    UpdateWallCorners(endNodeWall, true, true, true);
                    //}

                    if (difX != 0)
                    {
                        bool xHigher = (difX > 0);

                        for (int i = 1; i < Mathf.Abs(difX) + 1; i++)
                        {
                            int offset = xHigher ? i : -i;
                            int posX = startNode_Wall.nodePosX + offset;
                            int posZ = startNode_Wall.nodePosZ;

                            if (posX < 0)
                            {
                                posX = 0;
                            }
                            if (posX > gridBase.sizeX)
                            {
                                posX = gridBase.sizeX;
                            }
                            if (posZ < 0)
                            {
                                posZ = 0;
                            }
                            if (posZ > gridBase.sizeZ)
                            {
                                posZ = gridBase.sizeZ;
                            }

                            finalXNode = gridBase.grid[posX, posZ];
                            Level_WallObj.WallDirection targetDir = Level_WallObj.WallDirection.ab;
                            CreateWallInNode(posX, posZ, targetDir);
                        }
                        UpdateWallCorners(xHigher ? endNodeWall : startNode_Wall, true, false, false);
                        UpdateWallCorners(xHigher ? startNode_Wall : endNodeWall, false, true, false);

                    }
                    if (difZ != 0)
                    {
                        bool zHigher = (difZ > 0);

                        for (int i = 1; i < Mathf.Abs(difZ) + 1; i++)
                        {
                            int offset = zHigher ? i : -i;
                            int posX = startNode_Wall.nodePosX;
                            int posZ = startNode_Wall.nodePosZ + offset;

                            if (posX < 0)
                            {
                                posX = 0;
                            }
                            if (posX > gridBase.sizeX)
                            {
                                posX = gridBase.sizeX;
                            }
                            if (posZ < 0)
                            {
                                posZ = 0;
                            }
                            if (posZ > gridBase.sizeZ)
                            {
                                posZ = gridBase.sizeZ;
                            }

                            Level_WallObj.WallDirection targetDir = Level_WallObj.WallDirection.bc;

                            finalZNode = gridBase.grid[posX, posZ];
                            CreateWallInNode(posX, posZ, targetDir);
                        }
                        UpdateWallNode(startNode_Wall, Level_WallObj.WallDirection.bc);

                        UpdateWallCorners(zHigher ? startNode_Wall : finalZNode, false, true, false);
                        UpdateWallCorners(zHigher ? finalZNode : startNode_Wall, false, false, true);
                    }
                    if (difZ != 0 && difX != 0)
                    {
                        bool xHigher = (difX > 0);
                        bool zHigher = (difZ > 0);

                        for (int i = 1; i < Mathf.Abs(difX) + 1; i++)
                        {
                            int offset = xHigher ? i : -i;
                            int posX = startNode_Wall.nodePosX + offset;
                            int posZ = endNodeWall.nodePosZ;

                            if (posX < 0)
                            {
                                posX = 0;
                            }
                            if (posX > gridBase.sizeX)
                            {
                                posX = gridBase.sizeX;
                            }
                            if (posZ < 0)
                            {
                                posZ = 0;
                            }
                            if (posZ > gridBase.sizeZ)
                            {
                                posZ = gridBase.sizeZ;
                            }

                            Level_WallObj.WallDirection targetDir = Level_WallObj.WallDirection.ab;
                            CreateWallInNode(posX, posZ, targetDir);
                        }
                        for (int i = 1; i < Mathf.Abs(difZ) + 1; i++)
                        {
                            int offset = zHigher ? i : -i;
                            int posX = endNodeWall.nodePosX;
                            int posZ = startNode_Wall.nodePosZ + offset;

                            if (posX < 0)
                            {
                                posX = 0;
                            }
                            if (posX > gridBase.sizeX)
                            {
                                posX = gridBase.sizeX;
                            }
                            if (posZ < 0)
                            {
                                posZ = 0;
                            }
                            if (posZ > gridBase.sizeZ)
                            {
                                posZ = gridBase.sizeZ;
                            }

                            Level_WallObj.WallDirection targetDir = Level_WallObj.WallDirection.bc;
                            CreateWallInNode(posX, posZ, targetDir);
                        }
                        if (startNode_Wall.nodePosZ > endNodeWall.nodePosZ)
                        {
                            #region Up to down
                            manager.inSceneWalls.Remove(finalXNode.wallObj.gameObject);
                            Destroy(finalXNode.wallObj.gameObject);
                            finalXNode.wallObj = null;

                            UpdateWallNode(finalZNode, Level_WallObj.WallDirection.all);
                            UpdateWallNode(endNodeWall, Level_WallObj.WallDirection.bc);

                            if (startNode_Wall.nodePosX > endNodeWall.nodePosX)
                            {
                                #region End node is SW of Start
                                //the furthest node on the x
                                CreateWallOrUpdateNode(finalXNode, Level_WallObj.WallDirection.ab);
                                UpdateWallCorners(finalXNode, false, true, false);
                                //the end furthest to south
                                CreateWallOrUpdateNode(finalZNode, Level_WallObj.WallDirection.bc);
                                UpdateWallCorners(finalZNode, false, true, false);
                                //first node clicked
                                //destroy node and get on next to it
                                Node nextToStartNode = DestroyCurrentNodeAndGetPrevious(startNode_Wall, true);
                                UpdateWallCorners(nextToStartNode, true, false, false);
                                //the end node clicked
                                CreateWallOrUpdateNode(endNodeWall, Level_WallObj.WallDirection.all);
                                UpdateWallCorners(endNodeWall, false, true, false);
                                #endregion
                            }
                            else
                            {
                                #region End node is SE of start
                                //the furthest node on the x
                                Node beforeFinalX = DestroyCurrentNodeAndGetPrevious(finalXNode, true);
                                UpdateWallCorners(beforeFinalX, true, false, false);
                                //the end node furthest south
                                CreateWallOrUpdateNode(finalZNode, Level_WallObj.WallDirection.all);
                                UpdateWallCorners(finalZNode, false, true, false);
                                //first node clicked
                                //destroy and get on next to it
                                CreateWallOrUpdateNode(startNode_Wall, Level_WallObj.WallDirection.ab);
                                UpdateWallCorners(startNode_Wall, false, true, false);
                                //the end node
                                CreateWallOrUpdateNode(endNodeWall, Level_WallObj.WallDirection.bc);
                                UpdateWallCorners(endNodeWall, false, true, false);
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region Down to up
                            if (startNode_Wall.nodePosX > endNodeWall.nodePosX)
                            {
                                #region End node is NW of Start
                                //the furthest node on the north east
                                Node northWestNode = DestroyCurrentNodeAndGetPrevious(finalZNode, true);
                                UpdateWallCorners(northWestNode, true, false, false);
                                //the end furthest to south west
                                CreateWallOrUpdateNode(finalXNode, Level_WallObj.WallDirection.all);
                                UpdateWallCorners(finalXNode, false, true, false);
                                //first node clicked
                                //destroy node and get on next to it
                                CreateWallOrUpdateNode(startNode_Wall, Level_WallObj.WallDirection.bc);
                                UpdateWallCorners(startNode_Wall, false, true, false);
                                //the end node clicked
                                CreateWallOrUpdateNode(endNodeWall, Level_WallObj.WallDirection.ab);
                                UpdateWallCorners(endNodeWall, false, true, false);
                                #endregion
                            }
                            else
                            {
                                #region End node is NE of start
                                //the furthest node on the north west
                                CreateWallOrUpdateNode(finalZNode, Level_WallObj.WallDirection.ab);
                                UpdateWallCorners(finalZNode, false, true, false);
                                //the end node furthest south east
                                CreateWallOrUpdateNode(finalXNode, Level_WallObj.WallDirection.bc);
                                UpdateWallCorners(finalXNode, false, true, false);
                                //first node clicked
                                CreateWallOrUpdateNode(startNode_Wall, Level_WallObj.WallDirection.all);
                                UpdateWallCorners(startNode_Wall, false, true, false);
                                //the end node
                                Node nextToEndNode = DestroyCurrentNodeAndGetPrevious(endNodeWall, true);
                                UpdateWallCorners(nextToEndNode, true, false, false);
                                #endregion
                            }
                            #endregion
                        }
                    }
                    startNode_Wall = null;
                    endNodeWall = null;

                }
            }
        }

        void CreateWallOrUpdateNode(Node getNode, Level_WallObj.WallDirection direction)
        {
            if (getNode.wallObj == null)
            {
                CreateWallInNode(getNode.nodePosX, getNode.nodePosZ, direction);
            }
            else
            {
                UpdateWallNode(getNode, direction);
            }
        }

        Node DestroyCurrentNodeAndGetPrevious(Node curNode, bool positive)
        {
            int i = (positive) ? 1 : -1;

            Node beforeCurNode = gridBase.grid[curNode.nodePosX - i, curNode.nodePosZ];

            if (curNode.wallObj != null)
            {
                if (manager.inSceneWalls.Contains(curNode.wallObj.gameObject))
                {
                    manager.inSceneWalls.Remove(curNode.wallObj.gameObject);
                    Destroy(curNode.wallObj.gameObject);
                    curNode.wallObj = null;
                }
            }
            return beforeCurNode;
        }

        void CreateWallInNode(int posX, int posZ, Level_WallObj.WallDirection direction)
        {
            Node getNode = gridBase.grid[posX, posZ];
            Vector3 wallPosition = getNode.vis.transform.position;
            if (getNode.wallObj == null)
            {
                GameObject actualObjPlaced = Instantiate(wallPrefab, wallPosition, Quaternion.identity) as GameObject;
                actualObjPlaced.transform.parent = manager.wallHolder.transform;

                Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();
                Level_WallObj placedWallProperties = actualObjPlaced.GetComponent<Level_WallObj>();

                placedObjProperties.gridPosX = posX;
                placedObjProperties.gridPosZ = posZ;
                manager.inSceneWalls.Add(actualObjPlaced);
                getNode.wallObj = placedWallProperties;

                UpdateWallNode(getNode, direction);
            }
            else
            {
                UpdateWallNode(getNode, direction);
            }
            UpdateWallCorners(getNode, false, false, false);
        }

        void UpdateWallNode(Node getNode, Level_WallObj.WallDirection direction)
        {
            // for loop not neccessary??
            //for (int i = 0; i<getNode.wallObj.wallsList.Count;i++)
            // {
            getNode.wallObj.UpdateWall(direction);
            // }
        }

        void UpdateWallCorners(Node getNode, bool a, bool b, bool c)
        {
            if (getNode.wallObj != null)
            {
                getNode.wallObj.UpdateCorners(a, b, c);
            }
        }

        public void DeleteWall()
        {
            CloseAll();
            deleteWall = true;
        }

        void DeleteWallsActual()
        {
            if (deleteWall)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.wallObj != null)
                    {
                        if (manager.inSceneWalls.Contains(curNode.wallObj.gameObject))
                        {
                            manager.inSceneWalls.Remove(curNode.wallObj.gameObject);
                            Destroy(curNode.wallObj.gameObject);
                        }
                        curNode.wallObj = null;
                    }
                }
            }
        }

        #endregion*/

        #region Wall Object Placers
        void PlaceWallObject()
        {
            if (hasWallObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                worldPosition = curNode.vis.transform.position;




                if (cloneWallObj == null)
                {
                    cloneWallObj = Instantiate(wallObjToPlace, worldPosition, Quaternion.identity) as GameObject;
                    wallObjProperties = cloneWallObj.GetComponent<Level_Object>();
                }
                else
                {
                    cloneWallObj.transform.position = worldPosition;

                    if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                    {

                        if (hit.collider.tag == "Wall")
                        {
                            GameObject actualWallObjPlaced = Instantiate(wallObjToPlace, worldPosition, cloneWallObj.transform.rotation) as GameObject;
                            Level_Object placedWallObjProperties = actualWallObjPlaced.GetComponent<Level_Object>();

                            placedWallObjProperties.gridPosX = curNode.nodePosX;
                            placedWallObjProperties.gridPosZ = curNode.nodePosZ;
                            curNode.placedObj = placedWallObjProperties;
                            manager.inSceneGameObjects.Add(actualWallObjPlaced);

                            totalMoney -= placedWallObjProperties.price;
                        }
                        else
                        {
                            Debug.Log("Cannot");
                        }
                    }
                }


            }
            else
            {
                if (cloneWallObj != null)
                {
                    Destroy(cloneWallObj);
                }
            }
        }


        public void PassWallObjectToPlace(string wallObjId)
        {
            if (cloneWallObj != null)
            {
                Destroy(cloneWallObj);
            }

            CloseAll();
            hasWallObj = true;
            cloneWallObj = null;
            wallObjToPlace = ResourcesManager.GetInstance().GetWallObjBase(wallObjId).objPrefab;
        }
        void DeleteWallObjs()
        {
            if (deleteWallObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.placedObj != null)
                    {
                        if (manager.inSceneGameObjects.Contains(curNode.placedObj.gameObject))
                        {
                            manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                            Destroy(curNode.placedObj.gameObject);
                            totalMoney += curNode.placedObj.price;
                        }
                        curNode.placedObj = null;
                    }
                }
            }
        }
        public void DeleteWallObj()
        {
            CloseAll();
            deleteWallObj = true;
        }
        #endregion
        void CloseAll()
        {
            hasObj = false;
            deleteObj = false;
            paintTile = false;
            placeStackObj = false;
           // createWall = false;
            hasMaterial = false;
            deleteStackObj = false;
            //deleteWall = false;
            hasWallObj = false;
            deleteWallObj = false;
            deleteEnclosure = false;
            hasEnclosure = false;
        }


        #region Place Enclosure
        void PlaceEnclosure()
        {
            if (hasEnclosure)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                worldPosition = curNode.vis.transform.position;

                Node nodeE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z));
                Node nodeW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z));
                Node nodeN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (1 * gridBase.offset)));
                Node nodeS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (1 * gridBase.offset)));

                Node nodeEE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (2 * gridBase.offset), 0, mousePosition.z));
                Node nodeWW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (2 * gridBase.offset), 0, mousePosition.z));
                Node nodeNN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (2 * gridBase.offset)));
                Node nodeSS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (2 * gridBase.offset)));

                Node nodeNE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
                Node nodeSE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));
                Node nodeNW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
                Node nodeSW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));


                bool north = true;
                bool east = true;
                bool west = true;
                bool south = true;

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                {


                    if (curNode.placedObj != null)
                    {
                        manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                        Destroy(curNode.placedObj.gameObject);
                        curNode.placedObj = null;
                    }


                    GameObject actualObjPlaced = Instantiate(enclosureToPlace, worldPosition, Quaternion.identity) as GameObject;
                    Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();
                    placedObjProperties.gridPosX = curNode.nodePosX;
                    placedObjProperties.gridPosZ = curNode.nodePosZ;
                    curNode.placedObj = placedObjProperties;
                    manager.inSceneWalls.Add(actualObjPlaced);


                    BuildWallsAroundEnclosure();
                    /*
                     GameObject actualObjPlaced = Instantiate(enclosureToPlace, worldPosition, Quaternion.identity) as GameObject;
                    Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();
                    Level_Wall placedWallProperties = actualObjPlaced.GetComponent<Level_Wall>();

                    placedObjProperties.gridPosX = curNode.nodePosX;
                    placedObjProperties.gridPosZ = curNode.nodePosZ;
                    curNode.placedObj = placedObjProperties;
                    manager.inSceneWalls.Add(actualObjPlaced);
                    curNode.wall = placedWallProperties;


                    if (nodeN.placedObj != null)
                    {
                        if (nodeN.placedObj.isEnclosureObject == true)
                        {

                            nodeN.wall.UpdateCorners(north, east, west, false);
                            north = false;
                        }
                        if (nodeNN.placedObj != null)
                        {
                            if (nodeNN.placedObj.isEnclosureObject == true)
                            {

                                nodeN.wall.UpdateCorners(false, east, west, false);
                                north = false;
                            }
                        }
                    }
                    if (nodeE.placedObj != null)
                    {
                        if (nodeE.placedObj.isEnclosureObject == true)
                        {

                            
                            nodeE.wall.UpdateCorners(north, east, false, south);
                            east = false;

                        }
                        if (nodeEE.placedObj != null)
                        {
                            if (nodeEE.placedObj.isEnclosureObject == true)
                            {


                                nodeE.wall.UpdateCorners(north, false, false, south);
                                east = false;

                            }
                        }
                    }
                    if (nodeW.placedObj != null)
                    {
                        if (nodeW.placedObj.isEnclosureObject == true)
                        {

                            
                            nodeW.wall.UpdateCorners(north, false, west, south);
                            west = false;

                        }
                        if (nodeWW.placedObj != null)
                        {
                            if (nodeWW.placedObj.isEnclosureObject == true)
                            {
                                nodeW.wall.UpdateCorners(north, false, false, south);
                                west = false;
                            }
                        }
                    }
                    if (nodeS.placedObj != null)
                    { 
                        if (nodeS.placedObj.isEnclosureObject == true)
                        {
                            
                            nodeS.wall.UpdateCorners(false, east, west, south);
                            south = false;
                        }
                        if (nodeSS.placedObj != null)
                        {
                            if (nodeSS.placedObj.isEnclosureObject == true)
                            {
                                nodeS.wall.UpdateCorners(false, east, west, false);
                                south = false;
                            }
                        }
                    }
                 
                    if (nodeE.placedObj != null && nodeN.placedObj != null)
                    {
                        if (nodeE.placedObj.isEnclosureObject == true && nodeN.placedObj.isEnclosureObject == true)
                        {
                            north = true;
                            east = true;
                            south = false;
                            west = true;

                            nodeN.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = true;
                            south = true;
                            west = false;

                            nodeE.wall.UpdateCorners(north, east, west, south);

                            north = false;
                            east = false;
                            south = true;
                            west = true;
                        }
                    }
                    if (nodeE.placedObj != null && nodeS.placedObj != null)
                    {
                        if (nodeE.placedObj.isEnclosureObject == true && nodeS.placedObj.isEnclosureObject == true)
                        {
                            north = false;
                            east = true;
                            south = true;
                            west = true;

                            nodeS.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = true;
                            south = true;
                            west = false;

                            nodeE.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = false;
                            south = false;
                            west = true;
                        }
                    }
                    if (nodeN.placedObj != null && nodeW.placedObj != null)
                    {
                        if (nodeN.placedObj.isEnclosureObject == true && nodeW.placedObj.isEnclosureObject == true)
                        {
                            north = true;
                            east = true;
                            south = false;
                            west = true;

                            nodeN.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = false;
                            south = true;
                            west = true;

                            nodeW.wall.UpdateCorners(north, east, west, south);

                            north = false;
                            east = true;
                            south = true;
                            west = false;

                        }
                    }
                    if (nodeS.placedObj != null && nodeW.placedObj != null)
                    {
                        if (nodeS.placedObj.isEnclosureObject == true && nodeW.placedObj.isEnclosureObject == true)
                        {
                            north = false;
                            east = true;
                            south = true;
                            west = true;

                            nodeS.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = false;
                            south = true;
                            west = true;

                            nodeW.wall.UpdateCorners(north, east, west, south);


                        }
                    }
                    if (nodeS.placedObj != null && nodeW.placedObj != null && nodeE.placedObj != null && nodeN.placedObj != null)
                    {
                        if (nodeS.placedObj.isEnclosureObject == true && nodeW.placedObj.isEnclosureObject == true&& nodeE.placedObj.isEnclosureObject == true && nodeN.placedObj.isEnclosureObject == true)
                        {
                            north = true;
                            east = true;
                            south = false;
                            west = true;

                            nodeN.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = true;
                            south = true;
                            west = false;

                            nodeE.wall.UpdateCorners(north, east, west, south);

                            north = true;
                            east = false;
                            south = true;
                            west = true;

                            nodeW.wall.UpdateCorners(north, east, west, south);

                            north = false;
                            east = true;
                            south = true;
                            west = true;

                            nodeS.wall.UpdateCorners(north, east, west, south);

                            north = false;
                            east = false;
                            south = false;
                            west = false;
                        }
                    }

                    curNode.wall.UpdateCorners(north, east, west, south);*/








                }
                else
                {
                    if (cloneEnclosure != null)
                    {
                        Destroy(cloneEnclosure);
                    }
                }
            }
        }

        void UpdateWalls(Node node, bool a, bool b, bool c,bool d)
        {
             UpdateMousePosition();
            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
            Node nodeE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (1 * gridBase.offset)));

            if (curNode.placedObj != null)
            {
                curNode.wall.UpdateCorners(a, b, c, d);
            }
            if (nodeN.placedObj != null)
            {
                curNode.wall.UpdateCorners(a, b, c, d);
                nodeN.wall.UpdateCorners(a, b, c, d);
            }
            if (nodeE.placedObj != null)
            {
                curNode.wall.UpdateCorners(a, b, c, d);
                nodeE.wall.UpdateCorners(a, b, c, d);
            }
            if (nodeW.placedObj != null)
            {
                curNode.wall.UpdateCorners(a, b, c, d);
                nodeW.wall.UpdateCorners(a, b, c, d);
            }
            if (nodeS.placedObj != null)
            {
                curNode.wall.UpdateCorners(a, b, c, d);
                nodeS.wall.UpdateCorners(a, b, c, d);
            }
        }

        public void PassEnclosureObjectToPlace()
        {
            if (cloneEnclosure != null)
            {
                Destroy(cloneEnclosure);
            }

            CloseAll();
            hasEnclosure = true;
            cloneEnclosure = null;
            enclosureToPlace = enclosureObject;
        }
        void DeleteEnclosures()
        {
            if (deleteEnclosure)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.placedObj != null)
                    {
                        if (manager.inSceneGameObjects.Contains(curNode.placedObj.gameObject))
                        {
                            manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                            Destroy(curNode.placedObj.gameObject);
                            totalMoney += curNode.placedObj.price;
                        }
                        curNode.placedObj = null;
                    }
                }
            }
        }
        public void DeleteEnclosure()
        {
            CloseAll();
            deleteEnclosure = true;
        }

        public void DoneBuildingEnclosure()
        {
            CloseAll();
            BuildWallsAroundEnclosure();
        }
        
        void BuildWallsAroundEnclosureV2()
        {
            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
            worldPosition = curNode.vis.transform.position;

            Node nodeE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (1 * gridBase.offset)));

            Node nodeNE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeSE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));
            Node nodeNW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeSW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));


            actualWallPlaced = Instantiate(wallPrefab, new Vector3(worldPosition.x + (0.5f*gridBase.offset),0,worldPosition.z), Quaternion.Euler(0, 90, 0)) as GameObject;
            Level_Object placedNodeProperties = actualWallPlaced.GetComponent<Level_Object>();
            placedNodeProperties.gridPosX = curNode.nodePosX;
            placedNodeProperties.gridPosZ = curNode.nodePosZ;
            curNode.placedObj = placedNodeProperties;
            manager.inSceneGameObjects.Add(actualWallPlaced);

            GameObject actualWallPlaced2 = Instantiate(wallPrefab, new Vector3(worldPosition.x - (0.5f * gridBase.offset), 0, worldPosition.z), Quaternion.Euler(0, 90, 0)) as GameObject;
            Level_Object placedNode1Properties = actualWallPlaced2.GetComponent<Level_Object>();
            placedNode1Properties.gridPosX = curNode.nodePosX;
            placedNode1Properties.gridPosZ = curNode.nodePosZ;
            curNode.placedObj = placedNode1Properties;
            manager.inSceneGameObjects.Add(actualWallPlaced2);

            GameObject actualWallPlaced3 = Instantiate(wallPrefab, new Vector3(worldPosition.x, 0, worldPosition.z - (0.5f * gridBase.offset)), Quaternion.Euler(0, 0, 0)) as GameObject;
            Level_Object placedNode2Properties = actualWallPlaced3.GetComponent<Level_Object>();
            placedNode2Properties.gridPosX = curNode.nodePosX;
            placedNode2Properties.gridPosZ = curNode.nodePosZ;
            curNode.placedObj = placedNode2Properties;
            manager.inSceneGameObjects.Add(actualWallPlaced3);

            GameObject actualWallPlaced4 = Instantiate(wallPrefab, new Vector3(worldPosition.x, 0, worldPosition.z + (0.5f * gridBase.offset)), Quaternion.Euler(0, 0, 0)) as GameObject;
            Level_Object placedNode3Properties = actualWallPlaced4.GetComponent<Level_Object>();
            placedNode3Properties.gridPosX = curNode.nodePosX;
            placedNode3Properties.gridPosZ = curNode.nodePosZ;
            curNode.placedObj = placedNode3Properties;
            manager.inSceneGameObjects.Add(actualWallPlaced4);

           

        }

        void BuildWallsAroundEnclosure()
        {



            Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

            Node nodeE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z));
            Node nodeN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (1 * gridBase.offset)));

            Node nodeNE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeSE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));
            Node nodeNW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z + (1 * gridBase.offset)));
            Node nodeSW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (1 * gridBase.offset), 0, mousePosition.z - (1 * gridBase.offset)));

            Node nodeEE = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x + (2 * gridBase.offset), 0, mousePosition.z));
            Node nodeWW = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x - (2 * gridBase.offset), 0, mousePosition.z));
            Node nodeNN = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z + (2 * gridBase.offset)));
            Node nodeSS = gridBase.NodeFromWorldPosition(new Vector3(mousePosition.x, 0, mousePosition.z - (2 * gridBase.offset)));

            worldPosition = curNode.vis.transform.position;

            nodePosN = nodeN.vis.transform.position;
            nodePosS = nodeS.vis.transform.position;
            nodePosE = nodeE.vis.transform.position;
            nodePosW = nodeW.vis.transform.position;

            nodePosNE = nodeNE.vis.transform.position;
            nodePosSE = nodeSE.vis.transform.position;
            nodePosNW = nodeNW.vis.transform.position;
            nodePosSW = nodeSW.vis.transform.position;

                if (nodeN.placedObj == null)
            {


                actualWallPlaced = Instantiate(wallPrefab, nodePosN, Quaternion.Euler(0, 0, 0)) as GameObject;

                Level_Object placedNodeNProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeNProperties.gridPosX = nodeN.nodePosX;
                placedNodeNProperties.gridPosZ = nodeN.nodePosZ;
                nodeN.placedObj = placedNodeNProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);
            }
            if (nodeN.placedObj.isCornerObject == true)
            {

                manager.inSceneGameObjects.Remove(nodeN.placedObj.gameObject);
                Destroy(nodeN.placedObj.gameObject);

                actualWallPlaced = Instantiate(wallPrefab, nodePosN, Quaternion.Euler(0, 0, 0)) as GameObject;

                Level_Object placedNodeNProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeNProperties.gridPosX = nodeN.nodePosX;
                placedNodeNProperties.gridPosZ = nodeN.nodePosZ;
                nodeN.placedObj = placedNodeNProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

                Debug.Log("not null");

            }

            if (nodeNE.placedObj == null)
            {
                GameObject actualCornerPlaced = Instantiate(wallCornerPrefab, nodePosNE, Quaternion.Euler(0, 180, 0)) as GameObject;
                Level_Object placedNodeNEProperties = actualCornerPlaced.GetComponent<Level_Object>();

                placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                nodeNE.placedObj = placedNodeNEProperties;
                manager.inSceneGameObjects.Add(actualCornerPlaced);

            }

            if (nodeNW.placedObj == null)
            {
                GameObject actualCornerPlaced = Instantiate(wallCornerPrefab, nodePosNW, Quaternion.Euler(0, 90, 0)) as GameObject;
                Level_Object placedNodeNWProperties = actualCornerPlaced.GetComponent<Level_Object>();

                placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                nodeNW.placedObj = placedNodeNWProperties;
                manager.inSceneGameObjects.Add(actualCornerPlaced);

            }

            if (nodeS.placedObj == null)
            {
                actualWallPlaced = Instantiate(wallPrefab, nodePosS, Quaternion.Euler(0, 0, 0)) as GameObject;
                Level_Object placedNodeSProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeSProperties.gridPosX = nodeS.nodePosX;
                placedNodeSProperties.gridPosZ = nodeS.nodePosZ;
                nodeS.placedObj = placedNodeSProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

            }
            if (nodeS.placedObj.isCornerObject == true)
            {

                manager.inSceneGameObjects.Remove(nodeS.placedObj.gameObject);
                Destroy(nodeS.placedObj.gameObject);

                actualWallPlaced = Instantiate(wallPrefab, nodePosS, Quaternion.Euler(0, 0, 0)) as GameObject;

                Level_Object placedNodeSProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeSProperties.gridPosX = nodeS.nodePosX;
                placedNodeSProperties.gridPosZ = nodeS.nodePosZ;
                nodeS.placedObj = placedNodeSProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

                Debug.Log("not null");

            }
            if (nodeSE.placedObj == null)
            {
                GameObject actualCornerPlaced = Instantiate(wallCornerPrefab, nodePosSE, Quaternion.Euler(0, 270, 0)) as GameObject;
                Level_Object placedNodeSEProperties = actualCornerPlaced.GetComponent<Level_Object>();

                placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                nodeSE.placedObj = placedNodeSEProperties;
                manager.inSceneGameObjects.Add(actualCornerPlaced);

            }


            if (nodeSW.placedObj == null)
            {
                GameObject actualCornerPlaced = Instantiate(wallCornerPrefab, nodePosSW, Quaternion.Euler(0, 0, 0)) as GameObject;
                Level_Object placedNodeSWProperties = actualCornerPlaced.GetComponent<Level_Object>();

                placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                nodeSW.placedObj = placedNodeSWProperties;
                manager.inSceneGameObjects.Add(actualCornerPlaced);
            }

            if (nodeE.placedObj == null)
            {
                actualWallPlaced = Instantiate(wallPrefab, nodePosE, Quaternion.Euler(0, 90, 0)) as GameObject;
                Level_Object placedNodeEProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeEProperties.gridPosX = nodeE.nodePosX;
                placedNodeEProperties.gridPosZ = nodeE.nodePosZ;
                nodeE.placedObj = placedNodeEProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

            }
            if (nodeE.placedObj.isCornerObject == true)
            {

                manager.inSceneGameObjects.Remove(nodeE.placedObj.gameObject);
                Destroy(nodeE.placedObj.gameObject);

                actualWallPlaced = Instantiate(wallPrefab, nodePosE, Quaternion.Euler(0, 90, 0)) as GameObject;

                Level_Object placedNodeEProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeEProperties.gridPosX = nodeE.nodePosX;
                placedNodeEProperties.gridPosZ = nodeE.nodePosZ;
                nodeE.placedObj = placedNodeEProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

                Debug.Log("not null");

            }
            if (nodeW.placedObj == null)
            {
                actualWallPlaced = Instantiate(wallPrefab, nodePosW, Quaternion.Euler(0, 90, 0)) as GameObject;
                Level_Object placedNodeWProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeWProperties.gridPosX = nodeW.nodePosX;
                placedNodeWProperties.gridPosZ = nodeW.nodePosZ;
                nodeW.placedObj = placedNodeWProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

            }
            if (nodeW.placedObj.isCornerObject == true)
            {

                manager.inSceneGameObjects.Remove(nodeW.placedObj.gameObject);
                Destroy(nodeW.placedObj.gameObject);

                actualWallPlaced = Instantiate(wallPrefab, nodePosW, Quaternion.Euler(0, 90, 0)) as GameObject;

                Level_Object placedNodeWProperties = actualWallPlaced.GetComponent<Level_Object>();

                placedNodeWProperties.gridPosX = nodeW.nodePosX;
                placedNodeWProperties.gridPosZ = nodeW.nodePosZ;
                nodeW.placedObj = placedNodeWProperties;
                manager.inSceneGameObjects.Add(actualWallPlaced);

                Debug.Log("not null");

            }



            if (nodeSW.placedObj.isEnclosureObject == true)
            {
                if(nodeS.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeS.placedObj.gameObject);
                    Destroy(nodeS.placedObj.gameObject);
                    actualWallPlaced = Instantiate(wallCornerPrefab, nodePosS, Quaternion.Euler(0, 90, 0)) as GameObject;
                    Level_Object placedNodeSProperties = actualWallPlaced.GetComponent<Level_Object>();

                    placedNodeSProperties.gridPosX = nodeS.nodePosX;
                    placedNodeSProperties.gridPosZ = nodeS.nodePosZ;
                    nodeS.placedObj = placedNodeSProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced);
                }
                if (nodeW.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeW.placedObj.gameObject);
                    Destroy(nodeW.placedObj.gameObject);
                    GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosW, Quaternion.Euler(0, 270, 0)) as GameObject;
                    Level_Object placedNodeWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                    placedNodeWProperties.gridPosX = nodeW.nodePosX;
                    placedNodeWProperties.gridPosZ = nodeW.nodePosZ;
                    nodeW.placedObj = placedNodeWProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced2);
                }
                Debug.Log("not null");
            }
            if (nodeSE.placedObj.isEnclosureObject == true)
            {
                if (nodeS.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeS.placedObj.gameObject);
                    Destroy(nodeS.placedObj.gameObject);


                    actualWallPlaced = Instantiate(wallCornerPrefab, nodePosS, Quaternion.Euler(0, 180, 0)) as GameObject;
                    Level_Object placedNodeSProperties = actualWallPlaced.GetComponent<Level_Object>();

                    placedNodeSProperties.gridPosX = nodeS.nodePosX;
                    placedNodeSProperties.gridPosZ = nodeS.nodePosZ;
                    nodeS.placedObj = placedNodeSProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced);
                }
                if (nodeE.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeE.placedObj.gameObject);
                    Destroy(nodeE.placedObj.gameObject);
                    GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosE, Quaternion.Euler(0, 0, 0)) as GameObject;
                    Level_Object placedNodeEProperties = actualWallPlaced2.GetComponent<Level_Object>();
                    placedNodeEProperties.gridPosX = nodeE.nodePosX;
                    placedNodeEProperties.gridPosZ = nodeE.nodePosZ;
                    nodeE.placedObj = placedNodeEProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced2);
                }
                Debug.Log("not null");
            }

            if (nodeNW.placedObj.isEnclosureObject == true)
            {
                if (nodeN.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeN.placedObj.gameObject);
                    Destroy(nodeN.placedObj.gameObject);

                    actualWallPlaced = Instantiate(wallCornerPrefab, nodePosN, Quaternion.Euler(0, 0, 0)) as GameObject;

                    Level_Object placedNodeNProperties = actualWallPlaced.GetComponent<Level_Object>();

                    placedNodeNProperties.gridPosX = nodeN.nodePosX;
                    placedNodeNProperties.gridPosZ = nodeN.nodePosZ;
                    nodeN.placedObj = placedNodeNProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced);
                }
                if (nodeW.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeW.placedObj.gameObject);
                    Destroy(nodeW.placedObj.gameObject);

                    GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosW, Quaternion.Euler(0, 180, 0)) as GameObject;
                    Level_Object placedNodeWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                    placedNodeWProperties.gridPosX = nodeW.nodePosX;
                    placedNodeWProperties.gridPosZ = nodeW.nodePosZ;
                    nodeW.placedObj = placedNodeWProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced2);
                }
                

                Debug.Log("not null");

            }

            if (nodeNE.placedObj.isEnclosureObject == true)
            {
                if (nodeN.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeN.placedObj.gameObject);
                    Destroy(nodeN.placedObj.gameObject);

                    actualWallPlaced = Instantiate(wallCornerPrefab, nodePosN, Quaternion.Euler(0, 270, 0)) as GameObject;

                    Level_Object placedNodeNProperties = actualWallPlaced.GetComponent<Level_Object>();

                    placedNodeNProperties.gridPosX = nodeN.nodePosX;
                    placedNodeNProperties.gridPosZ = nodeN.nodePosZ;
                    nodeN.placedObj = placedNodeNProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced);
                }
                if (nodeE.placedObj.isEnclosureObject == false)
                {
                    manager.inSceneGameObjects.Remove(nodeE.placedObj.gameObject);
                    Destroy(nodeE.placedObj.gameObject);
                    GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosE, Quaternion.Euler(0, 90, 0)) as GameObject;
                    Level_Object placedNodeEProperties = actualWallPlaced2.GetComponent<Level_Object>();

                    placedNodeEProperties.gridPosX = nodeE.nodePosX;
                    placedNodeEProperties.gridPosZ = nodeE.nodePosZ;
                    nodeE.placedObj = placedNodeEProperties;
                    manager.inSceneGameObjects.Add(actualWallPlaced2);
                }
                Debug.Log("not null");
            }
            if (nodeEE.placedObj != null)
            {
                if (nodeE.placedObj.isEnclosureObject == false && nodeEE.placedObj.isEnclosureObject == true)
                {
                    if (nodeNE.placedObj.isWallObject == true && nodeSE.placedObj.isWallObject == true)
                    {
                        actualWallPlaced = Instantiate(wallCornerPrefab, nodePosNE, Quaternion.Euler(0, 270, 0)) as GameObject;

                        Level_Object placedNodeNEProperties = actualWallPlaced.GetComponent<Level_Object>();

                        placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                        placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                        nodeNE.placedObj = placedNodeNEProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced);

                        GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosSE, Quaternion.Euler(0, 180, 0)) as GameObject;

                        Level_Object placedNodeSEProperties = actualWallPlaced2.GetComponent<Level_Object>();

                        placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                        placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                        nodeSE.placedObj = placedNodeSEProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced2);
                    }
                    else if (nodeNE.placedObj.isCornerObject == true && nodeSE.placedObj.isCornerObject == true)
                    {
                        actualWallPlaced = Instantiate(wallPrefab, nodePosNE, Quaternion.Euler(0, 0, 0)) as GameObject;

                        Level_Object placedNodeNEProperties = actualWallPlaced.GetComponent<Level_Object>();

                        placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                        placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                        nodeNE.placedObj = placedNodeNEProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced);

                        GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosSE, Quaternion.Euler(0, 0, 0)) as GameObject;

                        Level_Object placedNodeSEProperties = actualWallPlaced2.GetComponent<Level_Object>();

                        placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                        placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                        nodeSE.placedObj = placedNodeSEProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced2);
                    }
                }
            }
                if (nodeNN.placedObj != null)
                {
                    if (nodeN.placedObj.isEnclosureObject == false && nodeNN.placedObj.isEnclosureObject == true)
                    {
                        if (nodeNE.placedObj.isWallObject == true && nodeNW.placedObj.isWallObject == true)
                        {
                            actualWallPlaced = Instantiate(wallCornerPrefab, nodePosNE, Quaternion.Euler(0, 180, 0)) as GameObject;

                            Level_Object placedNodeNEProperties = actualWallPlaced.GetComponent<Level_Object>();

                            placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                            placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                            nodeNE.placedObj = placedNodeNEProperties;
                            manager.inSceneGameObjects.Add(actualWallPlaced);

                            GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosNW, Quaternion.Euler(0, 90, 0)) as GameObject;

                            Level_Object placedNodeNWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                            placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                            placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                            nodeNW.placedObj = placedNodeNWProperties;
                            manager.inSceneGameObjects.Add(actualWallPlaced2);
                        }
                        else if (nodeNE.placedObj.isCornerObject == true && nodeNW.placedObj.isCornerObject == true)
                        {
                            actualWallPlaced = Instantiate(wallPrefab, nodePosNE, Quaternion.Euler(0, 90, 0)) as GameObject;

                            Level_Object placedNodeNEProperties = actualWallPlaced.GetComponent<Level_Object>();

                            placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                            placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                            nodeNE.placedObj = placedNodeNEProperties;
                            manager.inSceneGameObjects.Add(actualWallPlaced);

                            GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosNW, Quaternion.Euler(0, 90, 0)) as GameObject;

                            Level_Object placedNodeNWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                            placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                            placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                            nodeNW.placedObj = placedNodeNWProperties;
                            manager.inSceneGameObjects.Add(actualWallPlaced2);
                        }
                    }
                }
            if (nodeWW.placedObj != null)
            {
                if (nodeW.placedObj.isEnclosureObject == false && nodeWW.placedObj.isEnclosureObject == true)
                {
                    if (nodeNW.placedObj.isWallObject == true && nodeSW.placedObj.isWallObject == true)
                    {
                        actualWallPlaced = Instantiate(wallCornerPrefab, nodePosNW, Quaternion.Euler(0, 90, 0)) as GameObject;

                        Level_Object placedNodeNWProperties = actualWallPlaced.GetComponent<Level_Object>();

                        placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                        placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                        nodeNW.placedObj = placedNodeNWProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced);

                        GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosSW, Quaternion.Euler(0, 0, 0)) as GameObject;

                        Level_Object placedNodeSWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                        placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                        placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                        nodeSW.placedObj = placedNodeSWProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced2);
                    }
                    else if (nodeNW.placedObj.isCornerObject == true && nodeSW.placedObj.isCornerObject == true)
                    {
                        actualWallPlaced = Instantiate(wallPrefab, nodePosNW, Quaternion.Euler(0, 0, 0)) as GameObject;

                        Level_Object placedNodeNWProperties = actualWallPlaced.GetComponent<Level_Object>();

                        placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                        placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                        nodeNW.placedObj = placedNodeNWProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced);

                        GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosSW, Quaternion.Euler(0, 0, 0)) as GameObject;

                        Level_Object placedNodeSWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                        placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                        placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                        nodeSW.placedObj = placedNodeSWProperties;
                        manager.inSceneGameObjects.Add(actualWallPlaced2);
                    }
                }
            }
                        if (nodeSS.placedObj != null)
                        {
                            if (nodeS.placedObj.isEnclosureObject == false && nodeSS.placedObj.isEnclosureObject == true)
                            {
                                if (nodeSE.placedObj.isWallObject == true && nodeSW.placedObj.isWallObject == true)
                                {
                                    actualWallPlaced = Instantiate(wallCornerPrefab, nodePosSE, Quaternion.Euler(0, 0, 0)) as GameObject;

                                    Level_Object placedNodeSEProperties = actualWallPlaced.GetComponent<Level_Object>();

                                    placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                                    placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                                    nodeSE.placedObj = placedNodeSEProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced);

                                    GameObject actualWallPlaced2 = Instantiate(wallCornerPrefab, nodePosSW, Quaternion.Euler(0, 270, 0)) as GameObject;

                                    Level_Object placedNodeSWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                                    placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                                    placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                                    nodeSW.placedObj = placedNodeSWProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced2);
                                }
                                else if (nodeSE.placedObj.isCornerObject == true && nodeSW.placedObj.isCornerObject == true)
                                {
                                    actualWallPlaced = Instantiate(wallPrefab, nodePosSE, Quaternion.Euler(0, 90, 0)) as GameObject;

                                    Level_Object placedNodeSEProperties = actualWallPlaced.GetComponent<Level_Object>();

                                    placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                                    placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                                    nodeSE.placedObj = placedNodeSEProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced);

                                    GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosSW, Quaternion.Euler(0, 90, 0)) as GameObject;

                                    Level_Object placedNodeSWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                                    placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                                    placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                                    nodeSW.placedObj = placedNodeSWProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced2);
                                }
                            }
                        }
                    
                
            




                            /*
                            if (nodeNN.placedObj !=null)
                            {
                                if (nodeNN.placedObj.isEnclosureObject == true)
                                {
                                    manager.inSceneGameObjects.Remove(nodeN.placedObj.gameObject);
                                    Destroy(nodeN.placedObj.gameObject);
                                    GameObject actualWallPlaced = Instantiate(enclosureObject, nodePosN, Quaternion.Euler(0, 0, 0)) as GameObject;
                                    Level_Object placedNodeNProperties = actualWallPlaced.GetComponent<Level_Object>();

                                    placedNodeNProperties.gridPosX = nodeN.nodePosX;
                                    placedNodeNProperties.gridPosZ = nodeN.nodePosZ;
                                    nodeN.placedObj = placedNodeNProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced);

                                   /* manager.inSceneGameObjects.Remove(nodeNE.placedObj.gameObject);
                                    Destroy(nodeNE.placedObj.gameObject);
                                    GameObject actualWallPlaced1 = Instantiate(wallPrefab, nodePosNE, Quaternion.Euler(0, 90, 0)) as GameObject;
                                    Level_Object placedNodeNEProperties = actualWallPlaced1.GetComponent<Level_Object>();

                                    placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                                    placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                                    nodeNE.placedObj = placedNodeNEProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced1);

                                    manager.inSceneGameObjects.Remove(nodeNW.placedObj.gameObject);
                                    Destroy(nodeNW.placedObj.gameObject);
                                    GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosNW, Quaternion.Euler(0, 90, 0)) as GameObject;
                                    Level_Object placedNodeNWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                                    placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                                    placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                                    nodeNW.placedObj = placedNodeNWProperties;
                                    manager.inSceneGameObjects.Add(actualWallPlaced2);*/
                            /*
                                            }
                                        }
                                        if (nodeEE.placedObj != null)
                                        {
                                            if (nodeEE.placedObj.isEnclosureObject == true)
                                            {
                                                manager.inSceneGameObjects.Remove(nodeE.placedObj.gameObject);
                                                Destroy(nodeE.placedObj.gameObject);
                                                GameObject actualWallPlaced = Instantiate(enclosureObject, nodePosE, Quaternion.Euler(0, 0, 0)) as GameObject;
                                                Level_Object placedNodeEProperties = actualWallPlaced.GetComponent<Level_Object>();

                                                placedNodeEProperties.gridPosX = nodeE.nodePosX;
                                                placedNodeEProperties.gridPosZ = nodeE.nodePosZ;
                                                nodeE.placedObj = placedNodeEProperties;
                                                manager.inSceneGameObjects.Add(actualWallPlaced);

                                               /* manager.inSceneGameObjects.Remove(nodeNE.placedObj.gameObject);
                                                Destroy(nodeNE.placedObj.gameObject);
                                                GameObject actualWallPlaced1 = Instantiate(wallPrefab, nodePosNE, Quaternion.Euler(0, 0, 0)) as GameObject;
                                                Level_Object placedNodeNEProperties = actualWallPlaced1.GetComponent<Level_Object>();

                                                placedNodeNEProperties.gridPosX = nodeNE.nodePosX;
                                                placedNodeNEProperties.gridPosZ = nodeNE.nodePosZ;
                                                nodeNE.placedObj = placedNodeNEProperties;
                                                manager.inSceneGameObjects.Add(actualWallPlaced1);

                                                manager.inSceneGameObjects.Remove(nodeSE.placedObj.gameObject);
                                                Destroy(nodeSE.placedObj.gameObject);
                                                GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosSE, Quaternion.Euler(0, 0, 0)) as GameObject;
                                                Level_Object placedNodeSEProperties = actualWallPlaced2.GetComponent<Level_Object>();

                                                placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                                                placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                                                nodeSE.placedObj = placedNodeSEProperties;
                                                manager.inSceneGameObjects.Add(actualWallPlaced2);*/
                            /*
                                            }
                                        }
                                        if (nodeWW.placedObj != null)
                                        {
                                            if (nodeWW.placedObj.isEnclosureObject == true)
                                            {
                                                manager.inSceneGameObjects.Remove(nodeW.placedObj.gameObject);
                                                Destroy(nodeW.placedObj.gameObject);
                                                GameObject actualWallPlaced = Instantiate(enclosureObject, nodePosW, Quaternion.Euler(0, 0, 0)) as GameObject;
                                                Level_Object placedNodeWProperties = actualWallPlaced.GetComponent<Level_Object>();

                                                placedNodeWProperties.gridPosX = nodeW.nodePosX;
                                                placedNodeWProperties.gridPosZ = nodeW.nodePosZ;
                                                nodeW.placedObj = placedNodeWProperties;
                                                manager.inSceneGameObjects.Add(actualWallPlaced);


                                                /*manager.inSceneGameObjects.Remove(nodeNW.placedObj.gameObject);
                                                Destroy(nodeNW.placedObj.gameObject);
                                                GameObject actualWallPlaced1 = Instantiate(wallPrefab, nodePosNW, Quaternion.Euler(0, 0, 0)) as GameObject;
                                                Level_Object placedNodeNWProperties = actualWallPlaced1.GetComponent<Level_Object>();

                                                placedNodeNWProperties.gridPosX = nodeNW.nodePosX;
                                                placedNodeNWProperties.gridPosZ = nodeNW.nodePosZ;
                                                nodeNW.placedObj = placedNodeNWProperties;
                                                manager.inSceneGameObjects.Add(actualWallPlaced1);

                                                manager.inSceneGameObjects.Remove(nodeSW.placedObj.gameObject);
                                                Destroy(nodeSW.placedObj.gameObject);
                                                GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosSW, Quaternion.Euler(0, 0, 0)) as GameObject;
                                                Level_Object placedNodeSWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                                                placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                                                placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                                                nodeSW.placedObj = placedNodeSWProperties;
                                                manager.inSceneGameObjects.Add(actualWallPlaced2);*/
                            /*             }
                                     }
                                     if (nodeSS.placedObj != null)
                                     {
                                         if (nodeSS.placedObj.isEnclosureObject == true)
                                         {
                                             manager.inSceneGameObjects.Remove(nodeS.placedObj.gameObject);
                                             Destroy(nodeS.placedObj.gameObject);
                                             GameObject actualWallPlaced = Instantiate(enclosureObject, nodePosS, Quaternion.Euler(0, 0, 0)) as GameObject;
                                             Level_Object placedNodeSProperties = actualWallPlaced.GetComponent<Level_Object>();

                                             placedNodeSProperties.gridPosX = nodeS.nodePosX;
                                             placedNodeSProperties.gridPosZ = nodeS.nodePosZ;
                                             nodeS.placedObj = placedNodeSProperties;
                                             manager.inSceneGameObjects.Add(actualWallPlaced);

                                           /*  manager.inSceneGameObjects.Remove(nodeSE.placedObj.gameObject);
                                             Destroy(nodeSE.placedObj.gameObject);
                                             GameObject actualWallPlaced1 = Instantiate(wallPrefab, nodePosSE, Quaternion.Euler(0, 90, 0)) as GameObject;
                                             Level_Object placedNodeSEProperties = actualWallPlaced1.GetComponent<Level_Object>();

                                             placedNodeSEProperties.gridPosX = nodeSE.nodePosX;
                                             placedNodeSEProperties.gridPosZ = nodeSE.nodePosZ;
                                             nodeSE.placedObj = placedNodeSEProperties;
                                             manager.inSceneGameObjects.Add(actualWallPlaced1);

                                             manager.inSceneGameObjects.Remove(nodeSW.placedObj.gameObject);
                                             Destroy(nodeSW.placedObj.gameObject);
                                             GameObject actualWallPlaced2 = Instantiate(wallPrefab, nodePosSW, Quaternion.Euler(0, 90, 0)) as GameObject;
                                             Level_Object placedNodeSWProperties = actualWallPlaced2.GetComponent<Level_Object>();

                                             placedNodeSWProperties.gridPosX = nodeSW.nodePosX;
                                             placedNodeSWProperties.gridPosZ = nodeSW.nodePosZ;
                                             nodeSW.placedObj = placedNodeSWProperties;
                                             manager.inSceneGameObjects.Add(actualWallPlaced2);*/
                            /*     }
                             }*/


                        }
        #endregion


        void OnGUI()
        {
            GUI.Label(new Rect(20, 20, 150, 30), totalPlaced.ToString());
        }

       
    }
}