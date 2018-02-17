using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class StarManager : MonoBehaviour
    {

        public GameObject[] animalList;
        public int totalWealth;
        public int wealthChange;
        public int totalMaxcurrentHappiness;
        public int currentcurrentHappiness;
        public int animalTotal;
        public int shelterTotal;
        public int foodTotal;
        public int totalLonelyAnimals;

        public Text OverManagerOut;

        AnimalHappiness Tester;
        // Use this for initialization
        void Start()
        {
            animalList = GameObject.FindGameObjectsWithTag("currentHappiness");
            totalWealth = 50000;
            wealthChange = 1;
            // to do: create all holding variables for the currentHappiness criteria
        }

        // Update is called once per frame
        void Update()
        {
            starUpdate();
        }

        //This function will be called when exhibit editing ends
        // and on load to update all the totals for ratings.
        void starUpdate()
        {
            ClearStatTracking();
            animalList = GameObject.FindGameObjectsWithTag("currentHappiness");
            for (int m = 0; m < animalList.Length; m++)
            {
                //get functions for all animal stats and add all animal stats to relavent counters
               /* Tester = animalList[m].GetComponent<AnimalcurrentHappiness>();
                currentcurrentHappiness += Tester.currentHappiness;
                totalMaxcurrentHappiness += Tester.overallcurrentHappiness;
                animalTotal += Tester.animalNum;
                shelterTotal += Tester.shelterNum;
                foodTotal += Tester.foodNum;
                totalWealth += wealthChange;
                if (Tester.isLonley)
                {
                    totalLonelyAnimals++;
                }*/
                

            }
            OverManagerOut.text = "<b><i><size=15>OverManager</size></i></b>" + "\nTotal Wealth: " + totalWealth + "\nWealth Change: " + wealthChange + "\nTotal Happy: " + currentcurrentHappiness + "\nZoo Max currentHappiness: " + totalMaxcurrentHappiness + "\nTotal Zoo Animals: " + animalTotal + "\nTotal Zoo Shelters: " + shelterTotal + "\nTotal food supplies: " + foodTotal;
            OverManagerOut.text = OverManagerOut.text.Replace("\\n", "\n");
            // here, divide all counters by the list length and multiply by 100 to get percentage currentHappiness perhaps?.
        }


        //little function to clear counters for improved readability
        void ClearStatTracking()
        {
            currentcurrentHappiness = 0;
            totalMaxcurrentHappiness = 0;
            animalTotal = 0;
            shelterTotal = 0;
            foodTotal = 0;
            totalLonelyAnimals = 0;
        }
    }
}