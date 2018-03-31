using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Merged Script of Caitlins Star Scripts, all changes will be marked with // on top and bottom

public class FoliageStarAw : MonoBehaviour {

    public bool foliageStarAchieved;
    public Image UIStar;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        // Initulize variables
        foliageStarAchieved = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Call to folliageHappiness()
        FolliageHappiness(anim);

        
        if (UIStar.fillAmount == 1f)
        {
            // Set folliageStarAcheived to true
            foliageStarAchieved = true;
        }
    }

    void FolliageHappiness(Animator anim)
    {

        // Check animal type is penguin
       
            // Check if rocks are there and no other foliage as that upsets penguins(probably wouldnt upset penguins but for sake of game)
            if (NumberTrackers.noRock >= 5 && NumberTrackers.noBush == 0 && NumberTrackers.noOther == 0)
            {
            UIStar.fillAmount = 1.0f;
            }
            else if (NumberTrackers.noRock == 0 || NumberTrackers.noBush > 0 || NumberTrackers.noOther > 0)
            {
            UIStar.fillAmount = 0.0f;
            }

        if (UIStar.fillAmount == 1)
        {
            anim.SetBool("StarAchieved", true);
        }
        else
        {
            anim.SetBool("StarAchieved", false);
        }

    }
}
