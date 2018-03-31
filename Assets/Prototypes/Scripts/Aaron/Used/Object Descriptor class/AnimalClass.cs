using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalClass : MonoBehaviour {

    public enum AnimalTypes
    {
        PENGUIN,
        OTHER
    }
    public AnimalTypes animalType;

    public enum AnimalFoodTypes
    {
        Herbivore,
        Carnivore,
        Omnivore
    }
    public AnimalFoodTypes animalFoodType;

}
