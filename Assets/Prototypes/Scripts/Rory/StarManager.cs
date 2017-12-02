using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour {
    
    public GameObject[] animalList;
    public int totalWealth;
    public int wealthChange;
    public int totalMaxHappiness;
    public int currentHappiness;
    public int animalTotal;
    public int shelterTotal;
    public int foodTotal;
    public int totalLonelyAnimals;

    AnimalHappiness Tester;
	// Use this for initialization
	void Start ()
    {
        animalList = GameObject.FindGameObjectsWithTag("Happiness");
        // to do: create all holding variables for the happiness criteria
	}
	
	// Update is called once per frame
	void Update ()
    {
        starUpdate();
	}

    //This function will be called when exhibit editing ends
    // and on load to update all the totals for ratings.
    void starUpdate()
    {
        ClearStatTracking();
        animalList = GameObject.FindGameObjectsWithTag("Happiness");
        for (int m = 0; m < animalList.Length; m++)
        {
            //get functions for all animal stats and add all animal stats to relavent counters
            Tester = animalList[m].GetComponent<AnimalHappiness>();
            currentHappiness += Tester.happiness;
            totalMaxHappiness += Tester.maxHappiness;
            animalTotal += Tester.animalNum;
            shelterTotal += Tester.shelterNum;
            foodTotal += Tester.foodNum;

            if(Tester.isLonley)
            {
                totalLonelyAnimals++;
            }
               
        }
    
    // here, divide all counters by the list length and multiply by 100 to get percentage happiness perhaps?.
    }


    //little function to clear counters for improved readability
    void ClearStatTracking()
    {
        currentHappiness = 0;
        totalMaxHappiness = 0;
        animalTotal = 0;
        shelterTotal = 0;
        foodTotal = 0;
        totalLonelyAnimals = 0;
    }
}
