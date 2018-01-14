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


    //Shelter variables
    public static float noAnimalsPerShelter;

    //Aid variables
    //Food variables
    public enum FoodType
    {
        CARNIVOUROUS,
        HERBIVOROUS
    }
    public FoodType foodType;


}


