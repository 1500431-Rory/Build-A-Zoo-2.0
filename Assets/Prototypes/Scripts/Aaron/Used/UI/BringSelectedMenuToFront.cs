using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringSelectedMenuToFront : MonoBehaviour {

  
    RectTransform theRectTransform;
    public void Clicked()
    {
            theRectTransform = transform as RectTransform; // Cast it to RectTransform
        theRectTransform.SetAsLastSibling(); // Make the panel show on top.
    }

}
