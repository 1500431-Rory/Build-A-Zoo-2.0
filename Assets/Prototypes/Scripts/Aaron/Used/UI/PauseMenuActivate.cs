using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuActivate : MonoBehaviour
{

    //public Button yourButton;
    public GameObject PauseMenu;

    public void panelManagement()
    {
        if (PauseMenu.activeInHierarchy == true)
        {
            hidePanel();
        }
        else if (PauseMenu.activeInHierarchy == false)
        {
            showPanel();
        }
    }

    void showPanel()
    {
        PauseMenu.gameObject.SetActive(true);
    }
    void hidePanel()
    {
        PauseMenu.gameObject.SetActive(false);
    }

}


