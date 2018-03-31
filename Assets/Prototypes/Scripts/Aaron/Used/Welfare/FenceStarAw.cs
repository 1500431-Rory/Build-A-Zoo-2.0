using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Merged Script of Caitlins Star Scripts, all changes will be marked with // on top and bottom

public class FenceStarAw : MonoBehaviour {

    public Image UIStar;
    private float fillAmount;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        UIStar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Fences call
        Fences(anim);
        
    }

    // Checking fence
    void Fences(Animator anim)
    {
        if(NumberTrackers.noConcrete+NumberTrackers.noConcreteW+NumberTrackers.noGlass+NumberTrackers.noWire+NumberTrackers.noWooden+NumberTrackers.noWoodenW == NumberTrackers.totalFences)
        {
            if(NumberTrackers.noConcrete+NumberTrackers.noWooden == NumberTrackers.totalFences)
            {
                UIStar.fillAmount = 0.5f;
            }
            else if(NumberTrackers.noWoodenW+NumberTrackers.noConcreteW+NumberTrackers.noGlass+NumberTrackers.noGlass > NumberTrackers.totalFences/4)
            {
                UIStar.fillAmount = 1;
            }
        }

        if (UIStar.fillAmount == 1)
        {
            anim.SetBool("StarAchieved",true);
        }
        else
        {
            anim.SetBool("StarAchieved", false);
        }
    }
}
