using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyClass : MonoBehaviour
{
  public enum ToyTypes
    {
        TOY,
        WATERTOY
    }

    public ToyTypes toyType;
    public float animalsKeptHappy;
    public bool isWaterObject;
}
