using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LevelEditor
{
    public class Terrain_Object : MonoBehaviour
    {

        public int matId;
        public int gridPosX;
        public int gridPosZ;
        public Vector3 worldPositionOffset;
        public Vector3 worldRotation;

        public float price = 0;
        public float maintenance = 0;

        public void UpdateNode(Node[,] grid)
        {
            Node node = grid[gridPosX, gridPosZ];

            Vector3 worldPosition = node.vis.transform.position;
            worldPosition += worldPositionOffset;
            transform.rotation = Quaternion.Euler(worldRotation);
            transform.position = worldPosition;
        }

    }

}

