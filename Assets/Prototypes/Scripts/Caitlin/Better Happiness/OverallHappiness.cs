using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallHappiness : MonoBehaviour {

	// Enums
	// For animal types
	public enum AnimalType
	{
		PENGUIN,
		OTHER,
		NONE
	}
	// For fence types
	public enum FenceType
	{
		GLASS,
		STONE,
		MESH,
		NONE
	}
	// For food types
	public enum FoodType
	{
		FISH,
		OTHER,
		NONE
	}
	// For foliage types
	public enum FoliageType
	{
		ROCK,
		BUSH,
		NONE
	}
	// For terrain types
	public enum TerrainType
	{
		WATER,
		STONE,
		GRASS,
		NONE
	}
	//  For toy types
	public enum ToyType
	{
		SHINYBOTTLE,
		SHINYDISC,
		ROPES,
		NONE
	}

	// General variables
	// Animal number counter
	private int animalNum;

	// Happiness variables
	public int currentHappiness;
	private int maxAnimalHappiness;

	// Bool for if animal is lonley
	private bool isLonley;

	// For other class varaibles
	public CareStar careStar;
	public CostStar costStar;
	public FenceStar fenceStar;
	public FoliageStar foliageStar;
    public TerrainStar terrainStar;


    private static OverallHappiness instance = null;
    public static OverallHappiness GetInstance()
    {
        return instance;
    }


    // Use this for initialization
    void Start () 
	{
		// Initualise variables
		animalNum = 0;
		currentHappiness = 0;
		maxAnimalHappiness = 100;
		isLonley = false;

		// Set all enums to NONE
		animalType = AnimalType.NONE;
		fenceType = FenceType.NONE;
		foodType = FoodType.NONE;
		foliageType = FoliageType.NONE;
		terrainType = TerrainType.NONE;
		toyType = ToyType.NONE;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Call to AnimalisLonley()
		AnimalisLonely();
		// Call to set type functions
		SetAnimal();
		SetCare();
		SetCost();
		SetFence();
		SetFoliage();
		SetTerrain();

		// Check if current happiness is less than max happiness
		if(currentHappiness < maxAnimalHappiness)
		{
			// Check if care star is complete
			if(careStar.careStarAchieved == true)
			{
				// Add total care happiness to the current happiness
				currentHappiness += careStar.careHappiness;
				// Reset the careHappiness to 0
				careStar.careHappiness = 0;
			}

			// Check if cost star has been achieved 
			if(costStar.costStarAchieved == true)
			{
				// Add cost happiness to current happiness
				currentHappiness += costStar.costHappiness;
				// Reset costeHappiness to 0
				costStar.costHappiness = 0;
			}

			// Check if fence star is complete
			if(fenceStar.fenceStarAchieved == true)
			{
				// Add total fence happiness to current happiness
				currentHappiness += fenceStar.fenceHappiness;
				// Reset fenceHappiness to 0
				fenceStar.fenceHappiness = 0;
			}

			// Check if foliage star is complete
			if(foliageStar.foliageStarAchieved == true)
			{
				// Add foliagetype happiness to the current happiness
				currentHappiness += foliageStar.foliageHappiness;
				// Reset the foliageHappiness to 0
				foliageStar.foliageHappiness = 0;
			}

			// Check if terrain star is complete
			if(terrainStar.terrainStarAchived == true)
			{
				// Add terraintype happiness to the current happiness
				currentHappiness += terrainStar.terrainHappiness;
				// Reset the terrainHappiness to 0
				terrainStar.terrainHappiness = 0;
			}

		}

		// Reset stars  and happiness variables if a new level is created
		
	}

	void AnimalisLonely()
	{
		// Check if number of animals is equal to 1
		if(animalNum == 1)
		{
			// Set is lonely to true
			isLonley = true;
		}
	}

	// Public animal type veraibles
	public AnimalType animalType;
	// Set animal
	public void SetAnimal()
	{
		string input;
		input = Input.inputString;
		// Detect keys have been pressed
		switch(input)
		{
			case "p" :
				// Set the animal type to penguin
				animalType = AnimalType.PENGUIN;
				break;
			case "o" :
				// Set the animal type to other
				animalType = AnimalType.OTHER;
				break;
			default :
				print("No animal type found");
				break;
		}
	}

	// Public care type variables
	public FoodType foodType;
	public ToyType toyType;
	// Set care
	public void SetCare()
	{
		string input;
		input = Input.inputString;
		// Detect keys have been pressed
		switch(input)
		{
		case "e" : 
			// Set toy type to  shiny disc
			toyType = ToyType.SHINYDISC;
			break;
		case "a" :
			// Set toy type to shinny bottle
			toyType = ToyType.SHINYBOTTLE;
			break;
		case "s" :
			// Set toy type to ropes
			toyType = ToyType.ROPES;
			break;
		case "d" :
			// Set food type to fish
			foodType = FoodType.FISH;
			break;
		case "f" :
			// Set food type other
			foodType = FoodType.OTHER;
			break;
		default :
			print("No care type found");
			break;
		}
	}

	public void SetCost()
	{
		// Add cost of fences, terrain animals care items and folaige items
	}

	// Public fence type variables
	public FenceType fenceType;
	// Set Fence
	public void SetFence()
	{
		string input;
		input = Input.inputString;

		switch(input)
		{
		case "0":
			// Set fence type to glass 
			fenceType = FenceType.GLASS;
			break;
		case "9":
			// Set fence type to mesh
			fenceType = FenceType.MESH;
			break;
		case "8":
			// Set fence type to stone
			fenceType = FenceType.STONE;
			break;
		default:
			// Print error message
			print("No fence type found");
			break;
		}
	}

	// Public foliage type variables
	public FoliageType foliageType;
	// Set Foliage
	public void SetFoliage()
	{
		string input;
		input = Input.inputString;
		// Detect keys have been pressed
		switch (input) 
		{
		case "b":
			// Set fence type bush
			foliageType = FoliageType.BUSH;
			break;
		case "r":
			// Set foliage type to rock
			foliageType = FoliageType.ROCK;
			break;
		default:
			// Print error message 
			print ("No foliage type found");
			break;
		}
	}

	// Public terrain type variables
	public TerrainType terrainType;
	// Set terrain
	public void SetTerrain()
	{
		string input;
		input = Input.inputString;
		// Detect keys have been pressed
		switch(input)
		{
		case "1" :
			// Set terrain type to grass
			terrainType = TerrainType.GRASS;
			break;
		case "2" :
			// Set terrain type tp stone
			terrainType = TerrainType.STONE;
			break;
		case "3" :
			// Set terrain type water
			terrainType = TerrainType.WATER;
			break;
		default :
			// Print error message
			print("No terrain type found");
			break;
		}
	}


}
