using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelEditor;

public class LoadLevelUIButton : MonoBehaviour {

    public Text txt;
    public string levelName;

    public void LoadLevel()
    {
        Level_SaveLoad.GetInstance().LoadLevelButton(levelName);
        InterfaceManager.GetInstance().CloseLoadLevelGrid();
    }

}
