using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceClass : MonoBehaviour {

 public enum FenceTypes
    {
        GLASS,
        WIRE,
        WIREW,
        WOODEN,
        WOODENW,
        CONCRETE,
        CONCRETEW
        
    }

    public FenceTypes fenceType;
}

