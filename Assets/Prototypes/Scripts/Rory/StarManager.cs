using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour {

    public GameObject[] animalList;
	// Use this for initialization
	void Start ()
    {
        animalList = GameObject.FindGameObjectsWithTag("Animal");
        // to do: create all holding variables for the happiness criteria
	}
	
	// Update is called once per frame
	void Update ()
    {
		//Should not be required but keeping just in case
	}

    //This function will be called when exhibit editing ends
    // and on load to update all the totals for ratings.
    void starUpdate()
    {
        animalList = GameObject.FindGameObjectsWithTag("Animal");
        for (int m = 0; m == animalList.Length; m++)
        {
            //get functions for all animal stats

            //add all animal stats to relavent counters
        }
    
    // here, divide all counters by the list length and multiply by 100 to get percentage happiness.
    }
}
