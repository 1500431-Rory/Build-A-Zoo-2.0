using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Happiness : MonoBehaviour {

    

    //Happiness Numbers
    float totalAnimalHappiness;
    float totalToyHappiness;
    float totalShelterHappiness;
    float totalTerrainHappiness;
    float totalFoodHappiness;

    //Total Number Trackers
    public static float noAnimals;
    public static float noToys;

    //prev
    public static float prevNoAnimals;
    public static float prevNoToys;
    public static float prevNoShelters;
    public static float prevNoFood;

    //Toy Numbers
    public static float noShinyBottle;
    public static float noShinyCd;
    public static float noWaterFloat;
 
    //Care Numbers
    public static float noAid;
    public static float noShelter;
    public static float noFood;

    public static float maxAnimalsHappy;
    public float noAnimalsPerToy;
    float happyPercentage;
    bool correctFood;
    bool isLonely;

    public bool carnivourous = false;
    public bool herbivourous = false;

    public static float animalsSheltered;

#region EnrichmentHappiness
    void ToyHappinessCalc()
    {
        
        totalToyHappiness = 20.0f;
       
        //Check they have a variety of toys;
        bool toyVariety = false;

        noToys = noShinyBottle + noShinyCd + noWaterFloat;
            if (noShinyBottle >= 1.0f && noShinyCd >= 1.0f)
            {
                toyVariety = true;
            }
            if (noShinyCd >= 1.0f && noWaterFloat >= 1.0f)
            {
                toyVariety = true;
            }
            if (noWaterFloat >= 1.0f && noShinyBottle >= 1.0f)
            {
                toyVariety = true;
            }

        if(maxAnimalsHappy > noAnimals)
        {
            happyPercentage = 1;
        }
        else
        {
            happyPercentage =  maxAnimalsHappy/noAnimals;
        }

        totalToyHappiness = (totalToyHappiness * happyPercentage);

        //If not a variety of toys when they require multiple toys, reduce happiness by 0.66(random chosen amount)
         if (toyVariety == false)
        {
            //more than (randomnumber) animals require a variety of toys
            if (noAnimals > 15) 
            {
                totalToyHappiness = totalToyHappiness * 0.66f;
            }
        }
    }
    #endregion
    #region CareHappiness
    void foodHappiness()
    {
        if(carnivourous == true)
        {
            ;
        }
    }
    #endregion
    #region FoliageHappiness
    #endregion
    #region TerrainHappiness
    #endregion
    #region AnimalHappiness
    #endregion

    public void TotalHappinessCalc()
    {
       ToyHappinessCalc();
      totalAnimalHappiness =  totalAnimalHappiness + totalToyHappiness;
       // Debug.Log("animal " + totalAnimalHappiness);
    } 

    void Update()
    {
        totalAnimalHappiness = 0.0f;
        
        TotalHappinessCalc();
    }
    
    
    // Use this for initialization
  


    void OnGUI()
    {
        GUI.Label(new Rect(100, 30, 150, 20), "Happiness: " + totalAnimalHappiness.ToString());
        GUI.Label(new Rect(100, 50, 150, 20), "Total Toys: " + noToys.ToString());
        GUI.Label(new Rect(100, 70, 150, 20), "Total Animals: " + noAnimals.ToString());
        GUI.Label(new Rect(100, 90, 150, 20), "Percentage: " + happyPercentage.ToString());

         GUI.Label(new Rect(100, 110, 150, 20), "Shiny Bottle: " + noShinyBottle.ToString());
         GUI.Label(new Rect(100, 130, 150, 20), "Shiny CD: " + noShinyCd.ToString());
         GUI.Label(new Rect(100, 150, 150, 20), "Water Float: " + noWaterFloat.ToString());
        GUI.Label(new Rect(100, 170, 150, 20), "AnimalsHappy: " + maxAnimalsHappy.ToString());
        GUI.Label(new Rect(100, 190, 150, 20), "Aid: " + noAid.ToString());
    }
}
