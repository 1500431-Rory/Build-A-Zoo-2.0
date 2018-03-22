using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Merged Script of Caitlins Star Scripts, all changes will be marked with // on top and bottom

public class TerrainStarAw : MonoBehaviour {

    public bool terrainStarAchieved;
    public Image UIStar;

    bool correctLand;
    bool containsWater;
    bool incorrectLand;

    float fillAmount1, fillAmount2, fillAmount3;

    // Use this for initialization
    void Start()
    {
        // Initialize variables
        terrainStarAchieved = false;
        UIStar.fillAmount = 0f;
        fillAmount1 = 0f;
        fillAmount2 = 0f;
        fillAmount3 = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Call to TerrainHappiness()
        TerrainHappiness();

      
            // Set terrainStarAcheived to true
            if(UIStar.fillAmount == 1.0f)
            terrainStarAchieved = true;
        
    }

    void TerrainHappiness()
    {

            // Check terrain type is stone
            if (NumberTrackers.noStone+NumberTrackers.noSand == NumberTrackers.totalTerrainSquares/4)
            {
                correctLand = true;
            }
            else
            {
                correctLand = false;
            }

            // Check terrain type is stone
            if (NumberTrackers.noWater == NumberTrackers.totalTerrainSquares / 4)
            {
                containsWater = true;
            }
            else
            {
                containsWater = false;
            }
        // Check terrain type is Grass
        if (NumberTrackers.noGrass > NumberTrackers.noSand + NumberTrackers.noStone || NumberTrackers.noGrass > NumberTrackers.noWater)
            {
                incorrectLand = true;
            }
            else
            {
                incorrectLand = false;
            }


        IncreaseFillAmount();
        UIStar.fillAmount = fillAmount1 + fillAmount2 + fillAmount3;
    }

    void IncreaseFillAmount()
    {
        if (correctLand == true)
        {

            fillAmount1 = 0.5f;

        }
        if (containsWater == true)
        {

            fillAmount2 = 0.5f;

        }
        if (incorrectLand == true)
        {

            fillAmount3 = -0.34f;

        }

        if (correctLand == true && containsWater == true && incorrectLand == false)
        {
            UIStar.fillAmount = 1.0f;
        }

    }
}
