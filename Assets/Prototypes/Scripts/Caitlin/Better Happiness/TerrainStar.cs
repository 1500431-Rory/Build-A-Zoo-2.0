using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainStar : MonoBehaviour {

	[HideInInspector]
	public bool terrainStarAchived;
	public int currentTerrainHappiness;
	public int terrainHappiness;

	public OverallHappiness overallVariables;

	// Use this for initialization
	void Start () 
	{
		// Initulize variables
		terrainStarAchived = false;
		currentTerrainHappiness = 0;
		terrainHappiness = 20;

		// Get compent/Find game object with script All Stars
		overallVariables = GameObject.Find("All Stars").GetComponent<OverallHappiness>();
	}

	// Update is called once per frame
	void Update () 
	{
		// Call to TerrainHappiness()
		TerrainHappiness();

		// Check if currentTerrainHappiness is equal to 20
		if(currentTerrainHappiness == 20)
		{
			// Set terrainStarAcheived to true
			terrainStarAchived = true;
		}
	}

	void TerrainHappiness()
	{
		// Check if terrain happiness is < 20
		if(currentTerrainHappiness < 20)
		{
			// Check animal type is penguin
			if(overallVariables.animalType == OverallHappiness.AnimalType.PENGUIN)
			{
				// Check terrain type is stone
				if(overallVariables.terrainType == OverallHappiness.TerrainType.STONE)
				{
					// Add 10 to current happiness
					currentTerrainHappiness += 10;
				}

				// Check terrain type is stone
				if(overallVariables.terrainType == OverallHappiness.TerrainType.WATER)
				{
					// Add 10 to current happiness
					currentTerrainHappiness += 10;
				}
			}
		}

		// Check if terrain happiness is > 0
		if(currentTerrainHappiness > 0)
		{
			// Check animal type is penguin
			if(overallVariables.animalType == OverallHappiness.AnimalType.PENGUIN)
			{
				// Check terrain type is stone
				if(overallVariables.terrainType == OverallHappiness.TerrainType.GRASS)
				{
					// Add 10 to current happiness
					currentTerrainHappiness --;
				}

				// Check terrain type is stone
				if(overallVariables.terrainType == OverallHappiness.TerrainType.SAND)
				{
					// Add 10 to current happiness
					currentTerrainHappiness --;
				}
			}
		}

	}
}
