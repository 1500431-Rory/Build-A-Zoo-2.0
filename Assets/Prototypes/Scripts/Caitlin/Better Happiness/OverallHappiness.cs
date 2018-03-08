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
	public TerrainStar terrainStar;
	public FoliageStar foliageStar;
	public CareStar careStar;
	public FenceStar fenceStar;

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

		// Check if terrain star is complete
		if(terrainStar.terrainStarAchived == true)
		{
			// Add terraintype happiness to the current happiness
			currentHappiness += terrainStar.terrainHappiness;
			// Reset the terrainHappiness to 0
			terrainStar.terrainHappiness = 0;

			print("overall" + terrainStar.terrainStarAchived);
		}

		// Check if foliage star is complete
		if(foliageStar.foliageStarAchieved == true)
		{
			// Add foliagetype happiness to the current happiness
			currentHappiness += foliageStar.foliageHappiness;
			// Reset the foliageHappiness to 0
			foliageStar.foliageHappiness = 0;
		}

		// Check if care star is complete
		if(careStar.careStarAchieved == true)
		{
			// Add total care happiness to the current happiness
			currentHappiness += careStar.careHappiness;
			// Reset the careHappiness to 0
			careStar.careHappiness = 0;
		}

		// Check if fence star is complete
		if(fenceStar.fenceStarAchieved == true)
		{
			// Add total fence happiness to current happiness
			currentHappiness += fenceStar.fenceHappiness;
			// Reset fenceHappiness to 0
			fenceStar.fenceHappiness = 0;
		}
		
	}

	void AnimalisLonely()
	{
		// Check if number of animals is equal to 1
		if(animalNum == 1)
		{
			// Set is lonely to true
			isLonley = true;
			// Check if current happiness is > 0
			/*if(currentHappiness > 0)
			{
				// Subtract 1 from current happiness
				currentHappiness --;
			}*/
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
			foodType = FoodType.FISH;
			break;
		case "f" :
			foodType = FoodType.OTHER;
			break;
		default :
			print("default D");
			break;
		}
	}

	public void SetCost()
	{
		
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
			fenceType = FenceType.GLASS;
			break;
		case "9":
			fenceType = FenceType.MESH;
			break;
		case "8":
			fenceType = FenceType.STONE;
			break;
		default:
			print("No fence type");
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
			foliageType = FoliageType.BUSH;
			break;
		case "r":
			foliageType = FoliageType.ROCK;
			break;
		default:
			print ("Default Fol");
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
			terrainType = TerrainType.GRASS;
			break;
		case "2" :
			terrainType = TerrainType.STONE;
			break;
		case "3" :
			terrainType = TerrainType.WATER;
			break;
		default :
			print("default T");
			break;
		}
	}


}
