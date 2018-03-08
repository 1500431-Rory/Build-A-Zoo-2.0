using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageStar : MonoBehaviour {

	[HideInInspector]
	public bool foliageStarAchieved;
	public int currentFoliageHappiness;
	public int rockFoliageHappiness;
	public int foliageHappiness;

	public OverallHappiness overallVariables;

	// Use this for initialization
	void Start () 
	{
		// Initulize variables
		foliageStarAchieved = false;
		currentFoliageHappiness = 0;
		rockFoliageHappiness = 0;
		foliageHappiness = 20;


		overallVariables = GameObject.Find("All Stars").GetComponent<OverallHappiness>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Call to folliageHappiness()
		FolliageHappiness();

		// Check if currentFolliageHappiness is equal to 20
		if(currentFoliageHappiness + rockFoliageHappiness == foliageHappiness)
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
			// Check folliage type
			switch(overallVariables.foliageType)
			{
			case OverallHappiness.FoliageType.BUSH:
				currentFoliageHappiness = 10;
				break;
			case OverallHappiness.FoliageType.ROCK:
				rockFoliageHappiness = 10;
				break;
			default:
				print("No FoliageType");
				break;
			}
		}
	}
}
