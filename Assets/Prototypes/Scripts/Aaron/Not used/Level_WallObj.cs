using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LevelEditor
{
    public class Level_WallObj : MonoBehaviour
    {
        public WallDirection wallDirection;
        public bool corner_a;
        public bool corner_b;
        public bool corner_c;

        public enum WallDirection
        {
            ab,
            bc,
            all
        }

        public List<Wall_Base> wallsList = new List<Wall_Base>();
        public List<Corner_Base> cornersList = new List<Corner_Base>();

        public void UpdateWall(WallDirection direction)
        {
            wallsList[0].active = false;
            wallsList[0].wall.SetActive(false);
            wallsList[1].active = false;
            wallsList[1].wall.SetActive(false);

            switch(direction)
            {
                case WallDirection.ab:
                    wallsList[0].active = true;
                    wallsList[0].wall.SetActive(true);
                    break;
                case WallDirection.bc:
                    wallsList[1].active = true;
                    wallsList[1].wall.SetActive(true);
                    break;
                case WallDirection.all:
                    wallsList[0].active = true;
                    wallsList[0].wall.SetActive(true);
                    wallsList[1].active = true;
                    wallsList[1].wall.SetActive(true);
                    break;
            }
            wallDirection = direction;
        }

        public void UpdateCorners(bool a, bool b, bool c)
        {
            cornersList[0].corner.SetActive(a);
            cornersList[1].corner.SetActive(b);
            cornersList[2].corner.SetActive(c);

            corner_a = a;
            corner_b = b;
            corner_c = c;
        }


        //From here we take the properties we want to be saved
        public WallObjectSaveableProperties GetSaveable()
        {
            WallObjectSaveableProperties s = new WallObjectSaveableProperties();
            s.direction = wallDirection;
            s.corner_a = corner_a;
            s.corner_b = corner_b;
            s.corner_c = corner_c;

            return s;
        }
    }

    [Serializable]
    public class WallObjectSaveableProperties
    {
        public Level_WallObj.WallDirection direction;
        public bool corner_a;
        public bool corner_b;
        public bool corner_c;
    }
    //[Serializable]
   // public class Wall_Base
  //  {
    //    public bool active;
   //     public GameObject wall;
   // }
    [Serializable]
    public class Corner_Base
    {
        public GameObject corner;
    }

}