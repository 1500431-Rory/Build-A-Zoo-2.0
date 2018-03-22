using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Merged Script of Caitlins Star Scripts, all changes will be marked with // on top and bottom

public class CareStarAw : MonoBehaviour {

    bool careStarAchieved;

    public Image UIStar;
    private float fillAmountFood = 0f;
    private float fillAmountShelter = 0f;
    private float fillAmountAid = 0f;

    bool correctFood;
    bool allAnimalsSheltered;
    bool haveAid;

    //public OverallHappiness overallVariables;

    public 

    // Use this for initialization
    void Start()
    {
        UIStar.fillAmount = 0f;
        correctFood = false;
        allAnimalsSheltered = false;
        haveAid = false;
    }

    // Update is called once per frame
    void Update()
    {
        AnimalCare();

        // Check both happiness variables are equal to careHappiness
        if (UIStar.fillAmount == 1f)
        {
            // Set careStarAchieved to true
            careStarAchieved = true;
        }
        else
        {
            // Set careStarAchieved to false
            careStarAchieved = false;
        }
    }

    // For food and diet
    void AnimalCare()
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

        if(NumberTrackers.noAnimals <= NumberTrackers.noAnimalsSheltered && NumberTrackers.noShelter > 0)
        {
            allAnimalsSheltered = true;
        }

        if(NumberTrackers.noAid == 1)
        {
            haveAid = true;
        }

        IncreaseFillAmount();
        UIStar.fillAmount = fillAmountFood + fillAmountShelter + fillAmountAid;

    }

    void IncreaseFillAmount()
    {
        if (correctFood == true)
        {
           
                fillAmountFood = 0.33f;
            
        }
        if (allAnimalsSheltered == true)
        {
            
                fillAmountShelter = 0.33f;
            
        }
        if (haveAid == true)
        {
           
                fillAmountAid = 0.34f;
            
        }

       if(correctFood==true&&allAnimalsSheltered==true&&haveAid==true)
        {
            UIStar.fillAmount = 1.0f;
        }

    }
}
