using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainStar : MonoBehaviour {

	public bool terrainStarAchived;
	public int currentTerrainHappiness;
	public int terrainHappiness;

	public OverallHappiness overallVariables;

	// Use this for initialization
	void Start () 
	{
		terrainStarAchived = false;
		currentTerrainHappiness = 0;
		terrainHappiness = 20;

		overallVariables = GameObject.Find("All Stars").GetComponent<OverallHappiness>();
	}

	// Update is called once per frame
	void Update () 
	{
		TerrainHappiness();
		print(overallVariables.animalType);
		print(overallVariables.terrainType);

		if(currentTerrainHappiness == 20)
		{
			terrainStarAchived = true;
		}
	}

	void TerrainHappiness()
	{
		// Check if terrain happiness is > 0
		if(currentTerrainHappiness > -1 && currentTerrainHappiness < 20)
		{
			// Check animal type is penguin
			if(overallVariables.animalType == OverallHappiness.AnimalType.PENGUIN)
			{
				// Check terrain type
				switch(overallVariables.terrainType)
				{
				case OverallHappiness.TerrainType.GRASS:
					currentTerrainHappiness --;
					break;
				case OverallHappiness.TerrainType.SAND:
					currentTerrainHappiness --;
					break;
				case OverallHappiness.TerrainType.STONE:
					currentTerrainHappiness += 10;
					break;
				case OverallHappiness.TerrainType.WATER:
					currentTerrainHappiness += 10;
					break;
				default:
					print("No terrainType");
					break;
				}
			}
		}
	}
}
