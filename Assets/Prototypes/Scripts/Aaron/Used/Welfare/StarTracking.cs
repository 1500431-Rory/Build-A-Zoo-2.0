using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarTracking : MonoBehaviour {

    bool careStarAchieved;
    bool correctFood;
    bool haveShelter;
    bool haveAid;
    public AudioSource careAudio;

    bool costStarAchieved;
    bool underBudget;
    public AudioSource costAudio;

    bool enrichmentStarAchieved;
    bool containsRocks;
    bool containsPlants;
    bool containsToy;
    bool containsWaterToy;
    public AudioSource enrichmentAudio;

    bool fenceStarAchieved;
    bool containsFullFence;
    bool containsWindows;
    public AudioSource fenceAudio;

    bool terrainStarAchieved;
    bool correctHard;
    bool correctSoft;
    bool containsWater;
    bool incorrectLand;
    public AudioSource terrainAudio;

    public Image careStarInfoPanelImage;
    public Sprite careStarUnacheivedSprite;
    public Sprite careStarAcheivedSprite;
    public Image[] careCheckImage;
    public Sprite[] careCheckSprite;

    public Image costStarInfoPanelImage;
    public Sprite costStarUnacheivedSprite;
    public Sprite costStarAcheivedSprite;
    public Image[] costCheckImage;
    public Sprite[] costCheckSprite;

    public Image enrichmentStarInfoPanelImage;
    public Sprite enrichmentStarUnacheivedSprite;
    public Sprite enrichmentStarAcheivedSprite;
    public Image[] enrichmentCheckImage;
    public Sprite[] enrichmentCheckSprite;

    public Image fenceStarInfoPanelImage;
    public Sprite fenceStarUnacheivedSprite;
    public Sprite fenceStarAcheivedSprite;
    public Image[] fenceCheckImage;
    public Sprite[] fenceCheckSprite;

    public Image terrainStarInfoPanelImage;
    public Sprite terrainStarUnacheivedSprite;
    public Sprite terrainStarAcheivedSprite;
    public Image[] terrainCheckImage;
    public Sprite[] terrainCheckSprite;

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
                careCheckImage[0].sprite = careCheckSprite[0];
                careCheckImage[1].sprite = careCheckSprite[0];
            }
        }
        else if (NumberTrackers.noAnimalsH > 0)
        {
            // Check foodType is appropreate for penguins diet and have enough food
            if (NumberTrackers.noHerbivorous > 0)
            {
                correctFood = true;
                careCheckImage[0].sprite = careCheckSprite[0];
            }
        }
        else if (NumberTrackers.noAnimalsO > 0)
        {
            // Check foodType is appropreate for penguins diet and have enough food
            if (NumberTrackers.noCarnivorous > 0 || NumberTrackers.noHerbivorous > 0)
            {
                correctFood = true;
                careCheckImage[0].sprite = careCheckSprite[0];
            }
        }
        else
        {
            careCheckImage[0].sprite = careCheckSprite[1];
            careCheckImage[1].sprite = careCheckSprite[1];
        }

        if (NumberTrackers.noShelter > 0)
        {
            haveShelter = true;
            careCheckImage[2].sprite = careCheckSprite[0];
        }
        else
        {
            haveShelter = false;
            careCheckImage[2].sprite = careCheckSprite[1];
        }

        if (NumberTrackers.noAid > 0)
        {
            haveAid = true;
            careCheckImage[3].sprite = careCheckSprite[0];
        }
        else
        {
            haveAid = false;
            careCheckImage[3].sprite = careCheckSprite[1];
        }

        if (correctFood == true && haveShelter == true && haveAid == true)
        {
            animCare.SetBool("StarAchieved", true);
            careStarAchieved = true;
            careStarInfoPanelImage.sprite = careStarAcheivedSprite;
            careAudio.PlayDelayed(0.5f);
        }
        else
        {
            animCare.SetBool("StarAchieved", false);
            careStarAchieved = false;
            careStarInfoPanelImage.sprite = careStarUnacheivedSprite;
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
            costCheckImage[0].sprite = costCheckSprite[0];
        }
        else
        {
            underBudget = false;
            costCheckImage[0].sprite = costCheckSprite[1];
        }

        if (careStarAchieved == true && fenceStarAchieved == true && enrichmentStarAchieved == true && terrainStarAchieved == true && underBudget == true)
        {
            animCost.SetBool("StarAchieved", true);
            costStarAchieved = true;
            costStarInfoPanelImage.sprite = costStarAcheivedSprite;
            costAudio.PlayDelayed(0.5f);
        }
        else
        {
            animCost.SetBool("StarAchieved", false);
            costStarAchieved = false;
            costStarInfoPanelImage.sprite = costStarUnacheivedSprite;
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
            enrichmentCheckImage[0].sprite = enrichmentCheckSprite[0];
        }
        else
        {
            containsRocks = false;
            enrichmentCheckImage[0].sprite = enrichmentCheckSprite[1];
        }
        if (NumberTrackers.noBush > 0 || NumberTrackers.noOther > 0)
        {
            containsPlants = true;
            enrichmentCheckImage[1].sprite = enrichmentCheckSprite[1];
        }
        else
        {
            containsPlants = false;
            enrichmentCheckImage[1].sprite = enrichmentCheckSprite[0];
        }
        if (NumberTrackers.noToys > 0)
        {
            containsToy = true;
            enrichmentCheckImage[2].sprite = enrichmentCheckSprite[0];
        }
        else
        {
            containsToy = false;
            enrichmentCheckImage[2].sprite = enrichmentCheckSprite[1];
        }
        if (NumberTrackers.noWaterToys > 0)
        {
            containsWaterToy = true;
            enrichmentCheckImage[3].sprite = enrichmentCheckSprite[0];
        }
        else
        {
            containsWaterToy = false;
            enrichmentCheckImage[3].sprite = enrichmentCheckSprite[1];
        }


        if (containsRocks == true && containsToy == true && containsWaterToy == true && containsPlants == false)
        {
            animEnrichment.SetBool("StarAchieved", true);
            enrichmentStarAchieved = true;
            enrichmentStarInfoPanelImage.sprite = enrichmentStarAcheivedSprite;
            enrichmentAudio.PlayDelayed(0.5f);
        }
        else
        {
            animEnrichment.SetBool("StarAchieved", false);
            enrichmentStarAchieved = false;
            enrichmentStarInfoPanelImage.sprite = enrichmentStarUnacheivedSprite;
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
            fenceCheckImage[0].sprite = fenceCheckSprite[0];
        }
        else
        {
            containsFullFence = false;
            fenceCheckImage[0].sprite = fenceCheckSprite[1];
        }
        if (NumberTrackers.noConcrete + NumberTrackers.noWooden == NumberTrackers.totalFences)
         {      
            containsWindows = false;
            fenceCheckImage[1].sprite = fenceCheckSprite[1];
        }
         else if (NumberTrackers.noWoodenW + NumberTrackers.noConcreteW + NumberTrackers.noGlass + NumberTrackers.noGlass > NumberTrackers.totalFences / 4)
         {
             containsWindows = true;
            fenceCheckImage[1].sprite = fenceCheckSprite[0];
        }

        if (containsFullFence == true && containsWindows == true)
        {
            animFence.SetBool("StarAchieved", true);
            fenceStarAchieved = true;
            fenceStarInfoPanelImage.sprite = fenceStarAcheivedSprite;
            fenceAudio.PlayDelayed(0.5f);
        }
        else
        {
            animFence.SetBool("StarAchieved", false);
            fenceStarAchieved = false;
            fenceStarInfoPanelImage.sprite = fenceStarUnacheivedSprite;
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
            terrainCheckImage[1].sprite = terrainCheckSprite[0];
        }
        else
        {
            correctHard = false;
            terrainCheckImage[1].sprite = terrainCheckSprite[1];
        }
        if (NumberTrackers.noSand >= NumberTrackers.totalTerrainSquares / 4)
        {
            correctSoft = true;
            terrainCheckImage[2].sprite = terrainCheckSprite[0];
        }
        else
        {
            correctSoft = false;
            terrainCheckImage[2].sprite = terrainCheckSprite[1];
        }

        if(correctHard == true && correctSoft == true)
        {
            terrainCheckImage[0].sprite = terrainCheckSprite[0];
        }
        else
        {
            terrainCheckImage[0].sprite = terrainCheckSprite[1];
        }

        // Check terrain type is stone
        if (NumberTrackers.noWater >= NumberTrackers.totalTerrainSquares / 4)
        {
            containsWater = true;
            terrainCheckImage[3].sprite = terrainCheckSprite[0];
        }
        else
        {
            containsWater = false;
            terrainCheckImage[3].sprite = terrainCheckSprite[1];
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
            terrainStarInfoPanelImage.sprite = terrainStarAcheivedSprite;
            terrainAudio.PlayDelayed(0.5f);
        }
        else
        {
            animTerrain.SetBool("StarAchieved", false);
            terrainStarAchieved = false;
            terrainStarInfoPanelImage.sprite = terrainStarUnacheivedSprite;
        }
    }
    #endregion
}
