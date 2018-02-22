using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelActivate : MonoBehaviour
{

    //public Button yourButton;
    public GameObject UIBackDrop;
    public GameObject ObjectSelectionMenu;
    public GameObject CloseMenuButton;
    //void Start()
    //{
    //    Button btn = yourButton.GetComponent<Button>();
    //     btn.onClick.AddListener(TaskOnClick);
    // }

    // void TaskOnClick()
    //{

    // }

    public void panelManagement()
    {
        if (UIBackDrop.activeInHierarchy == true)
        {
            hidePanel();
        }
        else if (UIBackDrop.activeInHierarchy == false)
        {
            showPanel();
        }
    }

    void showPanel()
    {
        UIBackDrop.gameObject.SetActive(true);
        ObjectSelectionMenu.gameObject.SetActive(true);
        CloseMenuButton.gameObject.SetActive(true);
    }
    void hidePanel()
    {
        UIBackDrop.gameObject.SetActive(false);
        ObjectSelectionMenu.gameObject.SetActive(false);
        CloseMenuButton.gameObject.SetActive(false);
    }

}


