using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelEditor
{
    public class InterfaceManager : MonoBehaviour
    {

        public bool mouseOverUIElement;

        public Transform loadLevelGrid;
        public Transform saveLevelDialog;
        public GameObject[] otherUI;
        public GameObject loadlevelUIbuttonPrefab;

        public GameObject btnWall;
        public GameObject btnObjects;
        public GameObject btnTerrain;

        Level_SaveLoad sl;




        void Start()
        {
            saveLevelDialog.gameObject.SetActive(false);
            CloseLoadLevelGrid();

            sl = Level_SaveLoad.GetInstance();

            CreateUIButtonsForAvailableLevels();
        }

        void CreateUIButtonsForAvailableLevels()
        {
            foreach (string s in sl.availableLevels)
            {
                GameObject go = Instantiate(loadlevelUIbuttonPrefab) as GameObject;
                go.transform.SetParent(loadLevelGrid);
                LoadLevelUIButton b = go.GetComponent<LoadLevelUIButton>();
                b.levelName = s;
                b.txt.text = s;
            }
        }

        void Update()
        {
            mouseOverUIElement = EventSystem.current.IsPointerOverGameObject();
        }

        public void OpenCloseLevelGrid()
        {
            if (loadLevelGrid.gameObject.activeInHierarchy)
            {
                CloseLoadLevelGrid();
            }
            else
            {
                OpenLoadLevelGrid();
            }
        }
        public void OpenLoadLevelGrid()
        {
            loadLevelGrid.gameObject.SetActive(true);
        }
        public void CloseLoadLevelGrid()
        {
            loadLevelGrid.gameObject.SetActive(false);
        }

        public void OpenSaveLevelDialog()
        {
            saveLevelDialog.gameObject.SetActive(true);

            foreach (GameObject go in otherUI)
            {
                go.SetActive(false);
            }

            loadLevelGrid.gameObject.SetActive(false);
        }
        public void CloseSaveLevelDialog()
        {
            saveLevelDialog.gameObject.SetActive(false);

            foreach (GameObject go in otherUI)
            {
                go.SetActive(true);
            }

            loadLevelGrid.gameObject.SetActive(true);
        }

        private static InterfaceManager instance = null;
        public static InterfaceManager GetInstance()
        {
            return instance;
        }
        void Awake()
        {
            instance = this;
        }

        public void NewLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        public void ReloadFiles()
        {
            Button[] prevB = loadLevelGrid.GetComponentsInChildren<Button>();
            foreach (Button b in prevB)
            {
                Destroy(b.gameObject);
            }
            sl.availableLevels.Clear();

            sl.LoadAllFileLevels();
            CreateUIButtonsForAvailableLevels();
        }


        public void TogglePauseMenu()
        {
            // not the optimal way but for the sake of readability
            if (btnObjects.GetComponentInChildren<Canvas>().enabled)
            {
                btnObjects.GetComponentInChildren<Canvas>().enabled = false;
                Time.timeScale = 1.0f;
            }
            else
            {
                btnObjects.GetComponentInChildren<Canvas>().enabled = true;
                Time.timeScale = 0f;
            }

            Debug.Log("GAMEMANAGER:: TimeScale: " + Time.timeScale);
        }
    }
}