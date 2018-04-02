using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteActive : MonoBehaviour {

    public Image Button;
    public Sprite ButtonSprite;
    public Sprite ButtonActiveSprite;
    bool activeMenu = false;

    public void ActiveButton()
    {
       Button.sprite = ButtonActiveSprite; 
    }
    public void DeactiveButton()
    {
        Button.sprite = ButtonSprite;
    }
}
