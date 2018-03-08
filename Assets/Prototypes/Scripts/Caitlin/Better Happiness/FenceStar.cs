using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceStar : MonoBehaviour {

	[HideInInspector]
	public bool fenceStarAchieved;
	public int animalHappiness;
	public int guestHappiness;
	public int fenceHappiness;

	public OverallHappiness overallVariables;

	// Use this for initialization
	void Start () 
	{
		// Initulize variables
		animalHappiness = 0;
		guestHappiness = 0;
		fenceHappiness = 20;
		fenceStarAchieved = false;

		// Find overallhappiness object 
		overallVariables = GameObject.Find("All Stars").GetComponent<OverallHappiness>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		// Fences call
		Fences();

		// Check that the sum of both happiness variables are equal to fenceHappiness
		if(animalHappiness + guestHappiness == fenceHappiness)
		{
			fenceStarAchieved = true;
		}
	}

	// Checking fence types
	void Fences()
	{
		// Check animalType is penguine
		if(overallVariables.animalType == OverallHappiness.AnimalType.PENGUIN)
		{
			// Check if fenceType is glass
			if(overallVariables.fenceType == OverallHappiness.FenceType.GLASS)
			{
				// Set animalHappiness to 10
				animalHappiness = 10;
				// Set guestHappiness to 10
				guestHappiness = 10;
			} 

			// Check if fenceType is stone
			if( overallVariables.fenceType == OverallHappiness.FenceType.STONE)
			{
				// Set animalHappiness to 10
				animalHappiness = 10;
				// Set guestHappiness to 5
				guestHappiness = 5;
			}

			// Check fenceType is mesh
			if(overallVariables.fenceType == OverallHappiness.FenceType.MESH)
			{
				// Set animalHappiness to 0
				animalHappiness = 0;
				// Set guestHappiness to 10
				guestHappiness = 10;
			}
		}
	}
}
