using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Merged Script of Caitlins Star Scripts, all changes will be marked with // on top and bottom

public class CostStarAw : MonoBehaviour {

    public bool costStarAchieved;

    public OverallHappiness overallVariables;
    public CareStarAw care;
    public FenceStarAw fence;
    public FoliageStarAw foliage;
    public TerrainStarAw terrain;

    public Image UIStar;
    private float fillAmount= 0f;

    public Animator anim;

    float prevMoney;
    // Use this for initialization
    void Start()
    {     
        costStarAchieved = false;
        UIStar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Call to CostCheck()
        if (care.UIStar.fillAmount == 1.0f && fence.UIStar.fillAmount == 1.0f && foliage.UIStar.fillAmount == 1.0f && terrain.UIStar.fillAmount == 1.0f)
        {
            CostCheck(anim);
        }
        
        if (UIStar.fillAmount == 1.0f)
            costStarAchieved = true;

    }

    void CostCheck(Animator anim)
    {

       prevMoney = NumberTrackers.totalMoney;
        // Add costs to costLevel to be compared to the budget
       if(NumberTrackers.totalMoney <= 180000 && NumberTrackers.totalMoney >= 150000)
        {
            if (NumberTrackers.maintenance > 3000)
            {
                UIStar.fillAmount = 0.75f;
            }
            else
            {
                UIStar.fillAmount = 0.9f;
            }
        }
        else if (NumberTrackers.totalMoney < 150000 && NumberTrackers.totalMoney >= 120000)
        {
           
            if (NumberTrackers.maintenance > 6000)
            {
                UIStar.fillAmount = 0.2f;
            }
            else
            {
                UIStar.fillAmount = 0.5f;
            }
        }
        else
        {
            UIStar.fillAmount = 0.0f;
        }

        if (UIStar.fillAmount == 1)
        {
            anim.SetBool("StarAchieved", true);
        }
        else
        {
            anim.SetBool("StarAchieved", false);
        }
    }
}
