using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainClass : MonoBehaviour {

    public enum TerrainTypes
    {
        GRASS,
        CONCRETE,
        WATER,
        SAND
    }

    public TerrainTypes terrainType;
}


