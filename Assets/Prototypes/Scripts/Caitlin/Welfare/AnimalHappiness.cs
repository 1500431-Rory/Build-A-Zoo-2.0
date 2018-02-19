using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// *** Note *** Key press code is for testing only. 
// This will need to be chancged for the prototype once there is UI and we have merged the project.

public class AnimalHappiness : MonoBehaviour {

	// Enums
	// For food types
	enum FoodType
	{
		//VEG,
		FISH,
		//FRUIT,
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
		MESH,
		NONE
	}

	// For terrain types
	enum TerrainType
	{
		WATER,
		STONE,
		GRASS,
		SAND,
		NONE
	}

	//  For toy types
	enum ToyType
	{
		SHINYBOTTLE,
		SHINYDISC,
		ROPES,
		NONE
	}

	// Variables for enum types
	AnimalType animal;
	FoodType food;
	DietType diet;
	FenceType fence;
	TerrainType terrain;
	ToyType toy;

	// General variables
	// Counters
	private int currentHappiness;
	private int shelterNum;
	private int animalNum;
	private int foodNum;
	// Happiness and Stars
	private int overallMaxHappiness;

	// Stars
	private bool terrainStarAchieved;
	private bool fenceStarAchieved;
	private bool folliageStarAchieved;
	private bool careStarAchieved;
	private bool costStarAchieved;

	// Bool for if animal is lonley
	private bool isLonley;

	// Text for displaying  welfare data.
	private Text welfareText;

	// Use this for initialization
	void Start () 
	{
		// Initualise variables
		shelterNum = 0;
		animalNum = 0;
		foodNum = 0;
		currentHappiness = 0;
		overallMaxHappiness = 100;

		// Initulize bools
		isLonley = false;
		terrainStarAchieved = false;
		fenceStarAchieved = false;
		folliageStarAchieved = false;
		careStarAchieved = false;
		costStarAchieved = false;

		// initualize enum types
		food = FoodType.NONE;
		animal = AnimalType.NONE;
		diet = DietType.NONE;
		fence = FenceType.NONE;
		toy = ToyType.SHINYBOTTLE;
		terrain = TerrainType.NONE;
	}
	
	// Update is called once per frame
	void Update () 
	{

		// Call to checks
		//Animal();
		Lonley();

		// Display animal welfare on canvas
		welfareText.text = " currentHappiness: " + currentHappiness + "\n Max currentHappiness: " + overallMaxHappiness + "\n Shelter Number: " + shelterNum + "\n Animal Number: " + animalNum + "\n Food Type: " + food + "\n Toy Type: " + toy + "\n Fence Type: " + fence + "\n Terrain Type: " + terrain + "\n Diet Type " + diet;
        welfareText.color = new Color(1f, 1f, 1f);

		// Food type given
		if(foodNum < 10)
		{
			// Check for f being pressed
			if(Input.GetKeyDown("f"))
			{
				// Set food|Type to fish
				food = FoodType.FISH;
				// Check that currentHappiness is less than overallMaxHappiness
				if(currentHappiness < overallMaxHappiness)
				{
					// Add 1 to foodNum
					foodNum ++;
					// Check if current happiness is less than 20
					if(currentHappiness < 20)
					{
						// Add 1 to currentHappiness
						currentHappiness ++;
					}


				}

				// Call to FoodCall
				FoodCheck();

				// Print out foodType
				print("F: " + food);
			}
		}

		// Check foodNum is > 0
		if(foodNum > 0)
		{
			// Check for "g" being pressed
			if(Input.GetKeyDown("g"))
			{
				if(currentHappiness > 0)
				{
					// Subrtact 1 from foodNum
					foodNum --;
				}

				// Call to FoodCall
				FoodCheck();

				// Print out foodType
				print("F: " + food);
			}
		}

		// Check shelter number is less than 10
		if(shelterNum < 10)
		{
			// Check if "s" key has been pressed
			if(Input.GetKeyDown("s"))
			{
				// Add 1 to shelterNum
				shelterNum ++;
				// Call to ShelterCheck()
				ShelterCheck();
			}
		}

		// Check shelterNUm > 0
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

		// Check if 't' key is pressed
		if (Input.GetKeyDown("t")) 
		{
			// Check animal type = penguin
			if(animal == AnimalType.PENGUIN)
			{
				//  Set terrain to be TerrainType Stone
				terrain = TerrainType.STONE;
			}
			// Call TerrainCheck()
			TerrainCheck();
		}

		// Check if 'w' key is pressed
		if (Input.GetKeyDown("w")) 
		{
			// Check animal type = penguin
			if(animal == AnimalType.PENGUIN)
			{
				//  Set terrain to be TerrainType Water
				terrain = TerrainType.WATER;
			}

			// Call TerrainCheck()
			TerrainCheck();
		}

		// Check if 'e' key is pressed
		if (Input.GetKeyDown("e")) 
		{
			// Check animal type = penguin
			if(animal == AnimalType.PENGUIN)
			{
				//  Set terrain to be TerrainType sand
				terrain = TerrainType.SAND;
			}

			// Call TerrainCheck()
			TerrainCheck();
		}

		// Check if 'r' key is pressed
		if (Input.GetKeyDown("r")) 
		{
			// Check animal type = penguin
			if(animal == AnimalType.PENGUIN)
			{
				//  Set terrain to be TerrainType grass
				terrain = TerrainType.GRASS;
			}

			// Call TerrainCheck()
			TerrainCheck();
		}

		// Check if 'l' key is pressed
		if (Input.GetKeyDown("l")) 
		{
			// Check animal type = penguin
			if(animal == AnimalType.PENGUIN)
			{
				//  Set toy to ToyType Shinny Bottle
				toy = ToyType.SHINYBOTTLE;
			}

			// Call ToyCheck()
			ToyCheck();
		}

		// Check if 'm' key is pressed
		if (Input.GetKeyDown("k")) 
		{
			// Check animal type = penguin
			if(animal == AnimalType.PENGUIN)
			{
				//  Set toy to ToyType rope
				toy = ToyType.ROPES;
			}

			// Call ToyCheck()
			ToyCheck();
		}

		// Check if 'm' key is pressed
		if (Input.GetKeyDown("m")) 
		{
			// Check animal type = penguin
			if(animal == AnimalType.PENGUIN)
			{
				//  Set fence type to type glass 
				fence = FenceType.GLASS;
			}

			// Call FenceCheck()
			FenceCheck();
		}

		// Check if 'n' key is pressed
		if (Input.GetKeyDown("n")) 
		{
			// Check animal type = penguin
			if(animal == AnimalType.PENGUIN)
			{
				//  Set fence type to type stone 
				fence = FenceType.STONE;
			}

			// Call FenceCheck()
			FenceCheck();
		}

		// Check if 'j' key is pressed
		if (Input.GetKeyDown("j")) 
		{
			// Check animal type = penguin
			if(animal == AnimalType.PENGUIN)
			{
				//  Set fence type to type mesh
				fence = FenceType.MESH;
			}

			// Call FenceCheck()
			FenceCheck();
		}
	}

	void StarSystem()
	{
		
	}
		
	public void Animal()
	{
		// Check if "p" key has been pressed
		if(animalNum < 10)
		{
			// Check if "p" key is pressed
			//if(Input.GetKeyDown("p"))
			//{
				// Set the animal type to penguin
				animal = AnimalType.PENGUIN;
				// Check if currentHappiness < overallMaxHappiness
				if(currentHappiness < overallMaxHappiness)
				{
					//  Add 1 to  animalNum
					animalNum ++;
				}
			//}
		}

		// Check if animal is > 0
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

	// Check if animal is lonley
	void Lonley()
	{
		// Check if anialNum is == 1
		if(animalNum == 1)
		{
			// Set isLonley = true;
			isLonley = true;
		}else
		{
			// Set isLonely = false
			isLonley = false;
		}
			

		// Check isLonley == true
		if(isLonley == true)
		{
			// CHeck if hapiness is > 0
			if(currentHappiness > 0)
			{
				// SUbtract 1 from currentHappiness 
				currentHappiness--;
			}
		}
	}

	// For checking the food given is the right food
	void FoodCheck()
	{

		// Check foodType and dietType match
		if (food == FoodType.FISH && diet == DietType.CARNIVOROUS)
		{
			// Check currentHappiness is less than overallMaxHappiness
			if(currentHappiness < overallMaxHappiness)
			{
				//  Add 1 to currentHappiness
				currentHappiness  ++;
			}
		}

		// Check that foodNum is not greater than animalNum
		if(animalNum > foodNum)
		{
			// Check if currentHappiness is > 0
			if(currentHappiness > 0)
			{
				// Subtract currentHappiness
				currentHappiness --;
			}
		}

		// Print animalType and diet
		print("AT: " + animal);
		print("D: " + diet);
	
	}

	// For checking if the fence is the right fence for the animal
	//  Check the enclosure size
	void FenceCheck()
	{
		// Fence height

		// check if fence type matches animal type
		if(animal == AnimalType.PENGUIN && fence == FenceType.STONE || animal == AnimalType.PENGUIN && fence == FenceType.GLASS)
		{
			// Check if currentHappiness < overallMaxHappiness
			if(currentHappiness < overallMaxHappiness)
			{
				// Add 1 to currentHappiness
				currentHappiness++;
			}
		}

		// Check if animal and fence type doesn't match
		if(animal == AnimalType.PENGUIN && fence == FenceType.MESH)
		{
			// CHeck if currentHappiness > 0
			if(currentHappiness > 0)
			{
				// Subtract 1 from currentHappiness
				currentHappiness--;
			}
		}
		// Enclosure size
	}

	// Check if the toy is correct for the animal
	public void ToyCheck()
	{
		
		// Check type of animal matches type of toy
		if(animal == AnimalType.PENGUIN && toy == ToyType.SHINYBOTTLE)
		{
			// Check if currentHappiness < overallMaxHappiness
			if(currentHappiness < overallMaxHappiness)
			{
				// Add 1 to currentHappiness
				currentHappiness++;
			}
		}

		// check animal and toy type doesnt match
		if(animal == AnimalType.PENGUIN && toy == ToyType.ROPES)
		{
			// Check if animal currentHappiness is > 0
			if(currentHappiness > 0)
			{
				// Subtract 1 from currentHappiness
				currentHappiness--;
			}
		}

	}

	// Check the right terrain is given to the animal
	void TerrainCheck()
	{

		if(currentHappiness < overallMaxHappiness)
		{
			// Check if terrain and animal type match
			if(animal == AnimalType.PENGUIN && terrain == TerrainType.STONE || animal == AnimalType.PENGUIN && terrain == TerrainType.WATER)
			{
				// Check if currentHappiness < overallMaxHappiness
				if(currentHappiness < overallMaxHappiness)
				{
					// Add 1 to currentHappiness
					currentHappiness++;
				}

			}
		}


		// Check if animal and terrain type don't match
		if(animal == AnimalType.PENGUIN  && terrain == TerrainType.SAND || animal == AnimalType.PENGUIN && terrain == TerrainType.GRASS)
		{
			// CHeck if currentHappiness is > 0
			if(currentHappiness > 0)
			{
				// Subtract 1 from currentHappiness
				currentHappiness--;
			}
		}
			

	}

	// check the shelter is the correct number compared to the animalNum
	void ShelterCheck()
	{ 
		// Check if shelterNum = animalNum
		if (animalNum == shelterNum) 
		{
			// Check if currentHappiness < overallMaxHappiness
			if(currentHappiness < overallMaxHappiness)
			{
				// Add 1 to currentHappiness
				currentHappiness ++;
			}
		}
			

		// *** Note *** encluser will require at least one shelter for the star
		// Only remove currentHappiness if it is > 0
		if(currentHappiness > 0)
		{
			// Check if animalNUm > shelterNum
			if(animalNum > shelterNum)
			{
				// Subtract 1 from currentHappiness
				currentHappiness --;
			}

		}

		//print("H: " + happieness);
		print("S: " + shelterNum);
		print("A: " + animalNum);

	}
}
