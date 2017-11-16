using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LevelEditor
{
    public class Level_Wall : MonoBehaviour
    {

       public bool wallNorth;
        public bool wallEast;
        public bool wallWest;
        public bool wallSouth;

      public List<Wall_Base> wallList = new List<Wall_Base>();

        public void UpdateCorners(bool a, bool b, bool c,bool d)
        {
            wallList[0].wall.SetActive(a);
            wallList[1].wall.SetActive(b);
            wallList[2].wall.SetActive(c);
            wallList[3].wall.SetActive(d);

            wallNorth = a;
            wallEast = b;
            wallWest = c;
            wallSouth = d;

        }
    }

    [Serializable]
    public class Wall_Base
    {
        public bool active;
        public GameObject wall;
    }
}
