using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallHappiness : MonoBehaviour {

	// Enums
	// For animal types
	enum AnimalType
	{
		PENGUIN,
		OTHER,
		NONE
	}
	// For diet types
	enum DietType
	{
		CARNIVOROUS,
		OTHER,
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
	// For food types
	enum FoodType
	{
		FISH,
		OTHER,
		NONE
	}
	// For foliage types
	enum FoliageType
	{
		ROCK,
		BUSH,
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

	// General variables
	// Counters
	private int shelterNum;
	private int animalNum;
	private int foodNum;

	// Happiness variables
	private int currentHappiness;
	private int maxAnimalHappiness;

	// Bool for if animal is lonley
	private bool isLonley;

	// Use this for initialization
	void Start () 
	{
		// Initualise variables
		shelterNum = 0;
		animalNum = 0;
		foodNum = 0;
		currentHappiness = 0;
		maxAnimalHappiness = 100;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Call to AnimalisLonley()
		AnimalisLonely();
		SetAnimal();
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
	AnimalType animalType;
	// Set animal
	public void SetAnimal()
	{
		string input;
		input = Input.inputString;
		// Detect keys have been pressed
		switch(input)
		{
			case "p" :
				animalType = AnimalType.PENGUIN;
				break;
			case "o" :
				animalType = AnimalType.OTHER;
				break;
			default :
				animalType = AnimalType.NONE;
				break;
		}
	}
	// Get animal
	public void GetAnimal()
	{
		AnimalType animal;
		animal = animalType;
	}

	// Public care type variables
	DietType dietType;
	FoodType foodType;
	ToyType toyType;
	// Set care
	public void SetCare()
	{
		
	}
	// Get care
	public void GetCare()
	{

	}

	// Public fence type variables
	FenceType fenceType;
	// Set Fence
	public void SetFence()
	{
		
	}
	// Get Fence
	public void GetFence()
	{

	}

	// Public foliage type variables
	FoliageType foliageType;
	// Set Foliage
	public void SetFoliage()
	{
		
	}
	// Get Foliage
	public void GetFoliage()
	{

	}

	// Public terrain type variables
	TerrainType terrainType;
	// Set terrain
	public void SetTerrain()
	{
		
		string input;
		input = Input.inputString;
		// Detect keys have been pressed
		switch(input)
		{
		case "p" :
			terrainType = TerrainType.GRASS;
			break;
		case "o" :
			terrainType = TerrainType.SAND;
			break;
		case "i" :
			terrainType = TerrainType.STONE;
			break;
		case "j" :
			terrainType = TerrainType.WATER;
			break;
		
		default :
			terrainType = TerrainType.NONE;
			break;
		}
	}
	// Get terrain
	public void GetTerrain()
	{
		TerrainType terrain;
		terrain = terrainType;
	}


}
