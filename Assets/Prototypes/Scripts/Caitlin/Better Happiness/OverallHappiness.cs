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
	// For diet types
	public enum DietType
	{
		CARNIVOROUS,
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
		SAND,
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
	// Counters
	private int shelterNum;
	private int animalNum;
	private int foodNum;

	// Happiness variables
	public int currentHappiness;
	private int maxAnimalHappiness;

	// Bool for if animal is lonley
	private bool isLonley;

	// For other class varaibles
	public TerrainStar terrainStar;

	// Use this for initialization
	void Start () 
	{
		// Initualise variables
		shelterNum = 0;
		animalNum = 2;
		foodNum = 0;
		currentHappiness = 0;
		maxAnimalHappiness = 100;

		// Set all enums to NONE
		animalType = AnimalType.NONE;
		fenceType = FenceType.NONE;
		foliageType = FoliageType.NONE;
		terrainType = TerrainType.NONE;
		toyType = ToyType.NONE;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Call to AnimalisLonley()
		//AnimalisLonely();
		// Call to set type functions
		SetAnimal();
		SetCare();
		SetCost();
		SetFence();
		SetFoliage();
		SetTerrain();

		//print(animalType);
		//print(terrainType);


		// Add type happiness to the current happiness
		currentHappiness += terrainStar.terrainHappiness;
		
	}

	void AnimalisLonely()
	{
		// Check if number of animals is < 2
		if(animalNum < 2)
		{
			// Set is lonely to true
			isLonley = true;
			// Check if current happiness is > 0
			if(currentHappiness > 0)
			{
				// Subtract 1 from current happiness
				currentHappiness --;
			}
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
				print("default A");
				break;
		}
	}

	// Public care type variables
	DietType dietType;
	FoodType foodType;
	ToyType toyType;
	// Set care
	public void SetCare()
	{
		
	}

	public void SetCost()
	{
		
	}

	// Public fence type variables
	FenceType fenceType;
	// Set Fence
	public void SetFence()
	{
		
	}

	// Public foliage type variables
	FoliageType foliageType;
	// Set Foliage
	public void SetFoliage()
	{
		
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
			terrainType = TerrainType.GRASS;
			break;
		case "2" :
			terrainType = TerrainType.SAND;
			break;
		case "3" :
			terrainType = TerrainType.STONE;
			break;
		case "4" :
			terrainType = TerrainType.WATER;
			break;
		default :
			print("default T");
			break;
		}
	}


}
