using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CareClass : MonoBehaviour {

    public enum CareTypes
    {
        SHELTER,
        AID,
        FOOD
    }
    public CareTypes careType;


    
    //Aid variables
    //Food variables
    public enum FoodType
    {
        CARNIVOUROUS,
        HERBIVOROUS,
        NOTFOOD
    }
    public FoodType foodType;

    //shelter variables
    public float noAnimalsPerShelter;

    

}


