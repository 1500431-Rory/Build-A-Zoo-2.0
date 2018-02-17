using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainStar : MonoBehaviour {

	public bool terrainStarAchived;

	public OverallHappiness terrainHappiness;

	// Use this for initialization
	void Start () 
	{
		terrainStarAchived = false;

		terrainHappiness = GameObject.Find("All Stars").GetComponent<OverallHappiness>();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
