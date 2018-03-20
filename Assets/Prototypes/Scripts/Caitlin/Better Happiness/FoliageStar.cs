using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageStar : MonoBehaviour {

	public bool foliageStarAchieved;
	public int rockHappiness;
	public int foliageHappiness;

	public OverallHappiness overallVariables;

	// Use this for initialization
	void Start () 
	{
		// Initulize variables
		foliageStarAchieved = false;
		rockHappiness = 0;
		foliageHappiness = 20;

		// Get compent/Find game object with script All Stars
		overallVariables = GameObject.Find("All Stars").GetComponent<OverallHappiness>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Call to folliageHappiness()
		FolliageHappiness();

		// Check if currentFolliageHappiness is equal to 20
		if(rockHappiness == foliageHappiness)
		{
			// Set folliageStarAcheived to true
			foliageStarAchieved = true;
		}
	}

	void FolliageHappiness()
	{
		
		// Check animal type is penguin
		if(overallVariables.animalType == OverallHappiness.AnimalType.PENGUIN)
		{
			// Check if foliageType is rock
			if(overallVariables.foliageType == OverallHappiness.FoliageType.ROCK)
			{
				// Set rockHapiness to 20
				rockHappiness = 20;
			}
		}
	}
}
