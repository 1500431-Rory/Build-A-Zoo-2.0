using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CareStar : MonoBehaviour {

	public bool careStarAchieved;
	public int foodHappiness;
	public int toyHappiness;
	public int careHappiness;
	public DietType dietType;

	// For diet types
	public enum DietType
	{
		CARNIVOROUS,
		OTHER,
		NONE
	}

	public OverallHappiness overallVariables;

	// Use this for initialization
	void Start () 
	{
		// Initulize variables
		foodHappiness = 0;
		toyHappiness = 0;
		careHappiness = 20;
		careStarAchieved = false;

		// Set deit enums to none
		dietType = DietType.NONE;

		// Find overallhappiness object 
		overallVariables = GameObject.Find("All Stars").GetComponent<OverallHappiness>();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		AnimalCare();

		// Check both happiness variables are equal to careHappiness
		if(foodHappiness + toyHappiness == careHappiness)
		{
			// Set careStarAchieved to true
			careStarAchieved = true;
		} else
		{
			// Set careStarAchieved to false
			careStarAchieved = false;
		}
	}

	// For food and diet
	void AnimalCare()
	{
		// Check animalType is penguine
		if(overallVariables.animalType == OverallHappiness.AnimalType.PENGUIN)
		{
			// Set dietType to carnivorous
			dietType = DietType.CARNIVOROUS;

			// Check toy type is appropreate for a penguine
			if(overallVariables.toyType == OverallHappiness.ToyType.SHINYBOTTLE || overallVariables.toyType == OverallHappiness.ToyType.SHINYDISC)
			{
				// Set toy happiness to 10
				toyHappiness = 10;
			}

			// Check foodType is appropreate for penguins diet
			if(overallVariables.foodType == OverallHappiness.FoodType.FISH)
			{
				// Set food happiness to 10
				foodHappiness = 10;
			}
		}
	}
		
}
