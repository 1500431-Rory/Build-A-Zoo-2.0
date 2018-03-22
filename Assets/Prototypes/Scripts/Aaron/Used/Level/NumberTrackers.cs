using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberTrackers : MonoBehaviour {

    //AnimalNumbers
    public static float noAnimals;
    public static float noAnimalsH;
    public static float noAnimalsC;
    public static float noAnimalsO;

    //Toy Numbers
    public static float noToys;
    public static float noWaterToys;
  
    //Care Numbers
    public static float noAid;
    public static float noShelter;
    public static float noAnimalsSheltered;
    public static float noCarnivorous;
    public static float noHerbivorous;

    //Fence Numbers
    public static float noConcrete;
    public static float noConcreteW;
    public static float noGlass;
    public static float noWire;
    public static float noWooden;
    public static float noWoodenW;

    //Foliage Numbers 
    public static float noBush;
    public static float noRock;
    public static float noOther;

    //Terrain Numbers
    public static float noStone;
    public static float noSand;
    public static float noWater;
    public static float noGrass;


    //totals
    public static float totalFences = 51; //set to match number of marker places, can be adjusted to find number on awake for changing enclosure sizes
    public static float totalTerrainSquares = 150;

    //Money
    public static float totalMoney;
    public static float maintenance;

    // Use this for initialization
    void Start ()
    {
        //Money
        totalMoney = 200000;
        maintenance = 0;

        noAnimals = 0;
        noAnimalsH = 0;
        noAnimalsC = 0;
        noAnimalsO = 0;

        //Toy Numbers
        noToys = 0;
        noWaterToys = 0;
   
        //Care Numbers
        noAid = 0;
        noShelter = 0;
        noAnimalsSheltered = 0;
        noCarnivorous =0;
        noHerbivorous = 0;

        //Fence Numbers
        noConcrete = 0;
        noConcreteW = 0;
        noGlass = 0;
        noWire = 0;
        noWooden = 0;
        noWoodenW = 0;

        //Foliage Numbers
        noBush = 0;
        noRock = 0;
        noOther = 0;

        //Terrain Numbers
        noStone = 0;
        noSand = 0;
        noWater = 0;
        noGrass = 0;

        

    }

    void OnGUI()
    {
        GUI.color = Color.black;
        GUI.Label(new Rect(800, 10, 150, 30), "Animals: " + noAnimals.ToString());

       // GUI.Label(new Rect(100, 50, 150, 20), "Toys: " + noToys.ToString());
       // GUI.Label(new Rect(100, 70, 150, 20), "Water Toys: " + noWaterToys.ToString());

       // GUI.Label(new Rect(100, 90, 150, 20), "Aid: " + noAid.ToString());
       // GUI.Label(new Rect(100, 110, 150, 20), "Shelter: " + noShelter.ToString());
       // GUI.Label(new Rect(100, 130, 150, 20), "Carnivorous: " + noCarnivorous.ToString());
       // GUI.Label(new Rect(100, 150, 150, 20), "Herbivorous: " + noHerbivorous.ToString());

       // GUI.Label(new Rect(100, 170, 150, 20), "Concrete: " + noConcrete.ToString());
       // GUI.Label(new Rect(100, 190, 150, 20), "ConcreteW: " + noConcreteW.ToString());
       // GUI.Label(new Rect(100, 210, 150, 20), "Glass: " + noGlass.ToString());
       // GUI.Label(new Rect(100, 230, 150, 20), "Wire: " + noWire.ToString());
       // GUI.Label(new Rect(100, 250, 150, 20), "Wooden: " + noWooden.ToString());
       // GUI.Label(new Rect(100, 270, 150, 20), "WoodenW: " + noWoodenW.ToString());

       //GUI.Label(new Rect(100, 290, 150, 20), "Bush: " + noBush.ToString());
       //GUI.Label(new Rect(100, 310, 150, 20), "Rock: " + noRock.ToString());
       //GUI.Label(new Rect(100, 330, 150, 20), "Other: " + noOther.ToString());

       // GUI.Label(new Rect(180, 30, 150, 20), "noStone: " + noStone.ToString());
       // GUI.Label(new Rect(180, 50, 150, 20), "noSand: " + noSand.ToString());
       // GUI.Label(new Rect(180, 70, 150, 20), "Water: " + noWater.ToString());
       // GUI.Label(new Rect(180, 90, 150, 20), "Grass: " + noGrass.ToString());

        GUI.Label(new Rect(800, 25, 150, 20), "TotalMoney: " + totalMoney.ToString());
        GUI.Label(new Rect(800, 40, 150, 20), "Maintenance: " + maintenance.ToString());

    }


}