using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelectionActivate : MonoBehaviour
{

    //public Button yourButton;
    public GameObject ObjectSelectionMenu;

    /*public void panelManagement()
    {
        if (ObjectSelectionPanel.activeInHierarchy == true)
        {
            hidePanel();
        }
        else if (ObjectSelectionPanel.activeInHierarchy == false)
        {
            showPanel();
        }
    }*/

    public void showPanel()
    {
        ObjectSelectionMenu.gameObject.SetActive(true);
    }
    public void hidePanel()
    {
        ObjectSelectionMenu.gameObject.SetActive(false);
    }

}


