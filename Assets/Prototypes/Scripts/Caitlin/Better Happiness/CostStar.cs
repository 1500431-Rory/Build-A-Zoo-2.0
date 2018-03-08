using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostStar : MonoBehaviour {

	public bool costStarAchieved;
	public int costLevel;
	public int totalCostLevel;
	public int budget;
	public int costHappiness;

	public OverallHappiness overallVariables;
	public CareStar care;
	public FenceStar fence;
	public FoliageStar foliage;
	public TerrainStar terrainStar;

	// Use this for initialization
	void Start () 
	{
		// Initulize variables
		costLevel = 0;
		budget = 20000;
		costHappiness = 20;
		costStarAchieved = false;

		// Get compent/Find game object with script All Stars
		overallVariables = GameObject.Find("All Stars").GetComponent<OverallHappiness>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Call to CostCheck()
		CostCheck();

		// Check if other stars have been achieved
		if(care.careStarAchieved == true && fence.fenceStarAchieved == true && foliage.foliageStarAchieved == true && terrainStar.terrainStarAchived == true)
		{
			// Check if costLevel is  < budget
			if(costLevel < budget)
			{
				// Set folliageStarAcheived to true
				costStarAchieved = true;
			}
		}
	}

	void CostCheck()
	{
		// Add costs to costLevel to be compared to the budget
	}
}
