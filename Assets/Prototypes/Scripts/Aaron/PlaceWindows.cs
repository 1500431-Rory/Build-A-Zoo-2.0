using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class PlaceWindows : MonoBehaviour
    {
       /* LevelManager manager;
        InterfaceManager ui;

        public GameObject Wall;
        public GameObject Window;

        bool hasWindow;
        GameObject windowToPlace;
        GameObject cloneWindow;
        Vector3 mousePosition;
        Vector3 windowPosition;
        bool deleteWall;

        void Start()
        {        
            manager = LevelManager.GetInstance();
            ui = InterfaceManager.GetInstance();
        }

        void Update()
        {
            PlaceWindow();
            DeleteWindow();
        }

        void UpdateMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Wall")
                mousePosition = hit.point;
            }

        }

        #region Place Objects
       public void PlaceWindow()
        {
            if (hasWindow)
            {
                UpdateMousePosition();

               
                
                windowPosition = Wall.transform.position;

                if (cloneWindow == null)
                {
                    cloneWindow = Instantiate(Window, windowPosition, Quaternion.identity) as GameObject;
                }
                else
                {
                    cloneWindow.transform.position = windowPosition;

                    if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                    {
                        if (Wall.gameObject.Equals(Window))
                        {
                            Window = null;
                        }

                        GameObject actualObjPlaced = Instantiate(Window, cloneWindow.transform.position, cloneWindow.transform.rotation) as GameObject;
                        Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();

                        manager.inSceneGameObjects.Add(actualObjPlaced);
                    }
                }
            }
            else
            {
                if (cloneWindow != null)
                {
                    Destroy(cloneWindow);
                }
            }
        }

        public void PassWindowToPlace(string objId)
        {
            if (cloneWindow != null)
            {
                Destroy(cloneWindow);
            }

            hasWindow = true;
            cloneWindow = null;
            windowToPlace = Window;
        }
        void DeleteWindows()
        {
            if (deleteWall)
            {
                UpdateMousePosition();
                GameObject curWall = Wall.GetComponent<GameObject>();

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                {
                    if (Wall.gameObject.Equals(Window))
                    {
                        if (manager.inSceneGameObjects.Contains(curWall.gameObject))
                        {
                            manager.inSceneGameObjects.Remove(curWall.gameObject);
                            Destroy(curWall.gameObject);
                        }
                        curWall = null;
                    }
                }
            }
        }
        public void DeleteWindow()
        {
            deleteWall = true;
        }
        #endregion


    */
    }
}
