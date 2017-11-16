using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// *** Note *** Key press code is for testing only. 
// This will need to be chancged for the prototype once there is UI and we have merged the project.

public class AnimalHappiness : MonoBehaviour {

	// For food types
	enum FoodType
	{
		MEAT,
		VEG,
		FISH,
		FRUIT,
		NONE
	}

	// For diet types
	enum DietType
	{
		CARNIVOROUS,
		NONE
	}

	// For animal types
	enum AnimalType
	{
		PENGUIN,
		NONE
	}

	// For fence types
	enum FenceType
	{
		GLASS,
		STONE,
		MESH
	}

	// For terrain types
	enum TerrainType
	{
		WATER,
		STONE,
		GRASS,
		SAND
	}

	//  For toy types
	enum ToyType
	{
		SHINYBOTTLE,
		SHINYDISC,
		ROPES
	}

	public int happiness;
	public int shelterNum;
	public int animalNum;
	public int foodNum;
	public int maxHappiness;

	AnimalType animal;
	FoodType food;
	DietType diet;
	FenceType fence;
	TerrainType terrain;
	ToyType toy;

	// Use this for initialization
	void Start () 
	{
		shelterNum = 0;
		animalNum = 0;
		foodNum = 0;
		maxHappiness = 5;
		happiness = 0;

		food = FoodType.NONE;
		animal = AnimalType.NONE;
		food = FoodType.NONE;
		diet = DietType.NONE;
	}
	
	// Update is called once per frame
	void Update () 
	{

		// Call to checks
		FoodCheck();
		Animal();

		if(shelterNum < 10)
		{
			// Check if "s" key has been pressed
			if(Input.GetKeyDown("s"))
			{
				// Add 1 to shelterNum
				shelterNum ++;
				// Call to ShleterCheck()
				ShelterCheck();
			}
		}

		if(shelterNum > 0)
		{
			// Check if "d" key is pressed
			if (Input.GetKeyDown("d")) 
			{
				// Subtract 1 from shelterNum
				shelterNum --;
				// Call to ShleterCheck()
				ShelterCheck();
			}
		}
	}
		
	void Animal()
	{
		// Check if "p" key has been pressed
		if(animalNum < 10)
		{
			if(Input.GetKeyDown("p"))
			{
				// Set the animal type to penguin
				animal = AnimalType.PENGUIN;
				//  Add 1 to  animalNum
				animalNum ++;
			}
		}

		if(animalNum > 0)
		{
			// Check if "o" key has been pressed
			if(Input.GetKeyDown("o"))
			{
				//  Remove 1 from animalNum
				animalNum --;
			}
		}


		// Check animal type is set to penguin
		if(animal == AnimalType.PENGUIN)
		{
			// Set diet to carnivorous
			diet = DietType.CARNIVOROUS;
		}

	}

	// For checking the food given is the right food
	void FoodCheck()
	{
		// Food type given
		if(foodNum < 10)
		{
			// Check for f being pressed
			// Test
			if(Input.GetKeyDown("f"))
			{
				// Set food|Type to fish
				food = FoodType.FISH;
				// Add 1 to foodNum
				foodNum ++;
				// Check that foodType and animaType match
				/*if (food == FoodType.FISH && animal == AnimalType.PENGUIN)
				{
					//  Add 1 to happiness
					happiness ++;

				} */

				// Print out foodType
				print("F: " + food);
			}
		}

		if(foodNum > 0)
		{
			// Check for "g" being pressed
			if(Input.GetKeyDown("g"))
			{
				// Subrtact 1 from foodNum
				foodNum --;

				// Print out foodType
				print("F: " + food);
			}
		}

		// Check foodType and dietType match
		if (food == FoodType.FISH && diet == DietType.CARNIVOROUS)
		{
			//  Add 1 to happiness
			happiness  ++;
			// Set food and diet to NULL
			food = FoodType.NONE;
			diet = DietType.NONE;
		}

		// Check that foodNum is not greater than animalNum
		if(foodNum > animalNum)
		{
			happiness --;
		}

		// Print animalType and diet
		print("AT: " + animal);
		print("D: " + diet);

		// Number of food needed for the number of animals
		// Check animalNum
	
	}

	// For checking if the fence is the right fence for the animal
	//  Check the enclosure size
	void FenceCheck()
	{
		// Fence height

		// Fence type

		// Enclosure size
	}

	// Check if the toy is correct for the animal
	void ToyCheck()
	{
		// Check type of animal for type of toy

		// Check number of animals for number of toys
	}

	// Check the right terrain is given to the animal
	void TerrainCheck()
	{
		// Check animal type

	}

	// check the shelter is the correct number compared to the animalNum
	void ShelterCheck()
	{ 

		//if(shelterNum != animalNum)
		//{
			//happieness = maxNum - Mathf.Abs(shelterNum - animalNum);
		//}

		// Check if shelterNum = animalNum
		if (shelterNum == animalNum) 
		{
			// Add 1 to happiness
			happiness ++;
		}

		// *** Note *** encluser will require at least one shelter for the star
		// Only remove happiness if it is > 0
		if(happiness > 0)
		{
			// Check if shelterNum > animalNum || shelterNum < animalNum
			if(shelterNum > animalNum)
			{
				// Subtract 1 from happiness
				happiness --;
			}

		}

		//print("H: " + happieness);
		//print("S: " + shelterNum);
		print("A: " + animalNum);

	}
}
