using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Merged Script of Caitlins Star Scripts, all changes will be marked with // on top and bottom

public class CareStarAw : MonoBehaviour {

    bool careStarAchieved;
    bool correctFood;
    bool allAnimalsSheltered;
    bool haveAid;

    public Animator anim;

    // Use this for initialization
    void Start()
    {
        correctFood = false;
        allAnimalsSheltered = false;
        haveAid = false;
    }
    // For food and diet
    void AnimalCare(Animator anim)
    {

        if(NumberTrackers.noAnimalsC > 0)
        { 
            // Check foodType is appropreate for penguins diet and have enough food
            if (NumberTrackers.noCarnivorous > 0)
            {
                correctFood = true;
            }
        }
        else if (NumberTrackers.noAnimalsH > 0)
        {
            // Check foodType is appropreate for penguins diet and have enough food
            if (NumberTrackers.noHerbivorous > 0)
            {
                correctFood = true;
            }
        }
        else if (NumberTrackers.noAnimalsO > 0)
        {
            // Check foodType is appropreate for penguins diet and have enough food
            if (NumberTrackers.noCarnivorous > 0 || NumberTrackers.noHerbivorous > 0)
            {
                correctFood = true;
            }
        }

        if(NumberTrackers.noShelter > 0)
        {
            allAnimalsSheltered = true;
        }

        if(NumberTrackers.noAid > 0)
        {
            haveAid = true;
        }

        if (correctFood == true && allAnimalsSheltered == true && haveAid == true)
        {
            anim.SetBool("StarAchieved", true);
        }
        else
        {
            anim.SetBool("StarAchieved", false);
        }

    }
}
