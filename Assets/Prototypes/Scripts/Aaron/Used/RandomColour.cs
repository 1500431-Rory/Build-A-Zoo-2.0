using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomColour : MonoBehaviour {

    public Image image;
    public Image backGroundimage;
    public Image stripe;

    Color RZSSblue = new Color32(6, 56, 83, 255);
    Color ConsGreen = new Color32(35, 13, 88, 255);
    Color EZYellow = new Color32(243, 176, 0, 255);
    Color HWPBlue = new Color32(76, 172, 194, 255);

    void Start()
    {
        int rand = Random.Range(1, 3);
         if(rand == 1)
        {
            image.color = EZYellow;
            backGroundimage.color = RZSSblue;
            stripe.color = EZYellow;
        }
        else if (rand == 2)
        {
            image.color = RZSSblue;
            backGroundimage.color = EZYellow;
            stripe.color = RZSSblue;
        }
       

        

    }
}
