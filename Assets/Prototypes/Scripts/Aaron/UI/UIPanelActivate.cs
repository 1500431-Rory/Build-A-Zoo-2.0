using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelActivate : MonoBehaviour {

    //public Button yourButton;
    public GameObject Panel;

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
        if(Panel.activeInHierarchy == true)
        {
            hidePanel();
        }
        else if (Panel.activeInHierarchy == false)
        {
            showPanel();
        }
    } 

   void showPanel()
    {
        Panel.gameObject.SetActive(true);
    }
    void hidePanel()
    {
        Panel.gameObject.SetActive(false);
    }

}
