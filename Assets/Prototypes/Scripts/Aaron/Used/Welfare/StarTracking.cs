using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTracking : MonoBehaviour {

    bool careStarAchieved;
    bool correctFood;
    bool haveShelter;
    bool haveAid;
    public Animator animCare;

    bool costStarAchieved;
    bool underBudget;
    public Animator animCost;

    bool enrichmentStarAchieved;
    bool containsRocks;
    bool containsPlants;
    bool containsToy;
    bool containsWaterToy;
    public Animator animEnrichment;

    bool fenceStarAchieved;
    bool containsFullFence;
    bool containsWindows;
    public Animator animFence;

    bool terrainStarAchieved;
    bool correctHard;
    bool correctSoft;
    bool containsWater;
    bool incorrectLand;
    public Animator animTerrain;

    private static StarTracking instance = null;
    public static StarTracking GetInstance()
    {
        return instance;
    }
    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        careStarAchieved = false;
        correctFood = true;
        haveShelter = false;
        haveAid = false;

        costStarAchieved = false;
        underBudget = false;

        enrichmentStarAchieved = false;
        containsRocks = false;
        containsPlants = false;
        containsToy = false;
        containsWaterToy = false;

        fenceStarAchieved = false;
        containsFullFence = false;
        containsWindows = false;
        terrainStarAchieved = false;

        terrainStarAchieved = false;
        correctHard= false;
        correctSoft = false;
        containsWater = false;
        incorrectLand = false;
    }

    #region CareStar
    // For food and diet
    public void CareCheck(Animator animCare)
    {

        if (NumberTrackers.noAnimalsC > 0)
        {
            // Check foodType is appropreate for penguins diet and have enough food
            if (NumberTrackers.noCarnivorous > 0)
            {
                correctFood = true;
            }
        }
        else if (NumberTrackers.noAnimalsH > 0)
        {
            // Check foodType is appropreate for penguins diet and have enough food
            if (NumberTrackers.noHerbivorous > 0)
            {
                correctFood = true;
            }
        }
        else if (NumberTrackers.noAnimalsO > 0)
        {
            // Check foodType is appropreate for penguins diet and have enough food
            if (NumberTrackers.noCarnivorous > 0 || NumberTrackers.noHerbivorous > 0)
            {
                correctFood = true;
            }
        }

        if (NumberTrackers.noShelter > 0)
        {
            haveShelter = true;
        }

        if (NumberTrackers.noAid > 0)
        {
            haveAid = true;
        }

        if (correctFood == true && haveShelter == true && haveAid == true)
        {
            animCare.SetBool("StarAchieved", true);
            careStarAchieved = true;
        }
        else
        {
            animCare.SetBool("StarAchieved", false);
            careStarAchieved = false;
        }

    }
    #endregion

    #region CostStar

    public void CostCheck(Animator animCost)
    {

        // Add costs to costLevel to be compared to the budget
        if (NumberTrackers.totalMoney > 0)
        {
            underBudget = true;
        }
        else
        {
            underBudget = false;
        }

        if (careStarAchieved == true && fenceStarAchieved == true && enrichmentStarAchieved == true && terrainStarAchieved == true && underBudget == true)
        {
            animCost.SetBool("StarAchieved", true);
            costStarAchieved = true;
        }
        else
        {
            animCost.SetBool("StarAchieved", false);
            costStarAchieved = false;
        }
    }
    #endregion
    #region EnrichmentStar
    public void EnrichmentCheck(Animator animEnrichment)
    {

        // Check if rocks are there and no other foliage as that upsets penguins(probably wouldnt upset penguins but for sake of game)
        if (NumberTrackers.noRock >= 3)
        {
            containsRocks = true;
        }
        else
        {
            containsRocks = false;
        }
        if (NumberTrackers.noBush > 0 || NumberTrackers.noOther > 0)
        {
            containsPlants = true;
        }
        else
        {
            containsPlants = false;
        }
        if (NumberTrackers.noToys > 0)
        {
            containsToy = true;
        }
        else
        {
            containsToy = false;
        }
        if (NumberTrackers.noWaterToys > 0)
        {
            containsWaterToy = true;
        }
        else
        {
            containsWaterToy = false;
        }


        if (containsRocks == true && containsToy == true && containsWaterToy == true && containsPlants == false)
        {
            animEnrichment.SetBool("StarAchieved", true);
            enrichmentStarAchieved = true;
        }
        else
        {
            animEnrichment.SetBool("StarAchieved", false);
            enrichmentStarAchieved = false;
        }

    }
    #endregion
    #region FenceStar

    // Checking fence
    public void FencesCheck(Animator animFence)
    {
        if (NumberTrackers.noConcrete + NumberTrackers.noConcreteW + NumberTrackers.noGlass + NumberTrackers.noWire + NumberTrackers.noWooden + NumberTrackers.noWoodenW == NumberTrackers.totalFences)
        {
            containsFullFence = true;
        }
        if (NumberTrackers.noConcrete + NumberTrackers.noWooden == NumberTrackers.totalFences)
         {      
            containsWindows = false;
         }
         else if (NumberTrackers.noWoodenW + NumberTrackers.noConcreteW + NumberTrackers.noGlass + NumberTrackers.noGlass > NumberTrackers.totalFences / 4)
         {
             containsWindows = true;
         }

        if (containsFullFence == true && containsWindows == true)
        {
            animFence.SetBool("StarAchieved", true);
            fenceStarAchieved = true;
        }
        else
        {
            animFence.SetBool("StarAchieved", false);
            fenceStarAchieved = false;
        }
    }
    #endregion
    #region TerrainStar

    public void TerrainCheck(Animator animTerrain)
    {

        // Check terrain type is stone
        if (NumberTrackers.noStone>= NumberTrackers.totalTerrainSquares / 4)
        {
            correctHard= true;
        }
        else
        {
            correctHard = false;
        }
        if (NumberTrackers.noSand >= NumberTrackers.totalTerrainSquares / 4)
        {
            correctSoft = true;
        }
        else
        {
            correctSoft = false;
        }

        // Check terrain type is stone
        if (NumberTrackers.noWater >= NumberTrackers.totalTerrainSquares / 4)
        {
            containsWater = true;
        }
        else
        {
            containsWater = false;
        }
        // Check terrain type is Grass
        if (NumberTrackers.noGrass > NumberTrackers.totalTerrainSquares / 4)
        {
            incorrectLand = true;
        }
        else
        {
            incorrectLand = false;
        }

        if (correctHard == true && correctSoft == true && containsWater == true)
        {
            animTerrain.SetBool("StarAchieved", true);
            terrainStarAchieved = true;
        }
        else
        {
            animTerrain.SetBool("StarAchieved", false);
            terrainStarAchieved = false;
        }
    }
    #endregion
}
