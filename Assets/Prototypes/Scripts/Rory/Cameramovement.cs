﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramovement : MonoBehaviour
{

    Transform cam;
    private Vector3 Offset;
    public float xPos;
    public float zPos;
    public float xMod;
    public float zMod;

    GridBase gridBase;

    // Use this for initialization
    void Start ()
    {
        gridBase = GridBase.GetInstance();
        cam = GetComponent<Transform>();
        xPos = cam.position.x;
        zPos = cam.position.z;
	}

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
     

        xMod = Input.GetAxis("Horizontal");
        zMod = Input.GetAxis("Vertical");

        xPos = cam.position.x;
        zPos = cam.position.z;
        if (xPos >= gridBase.sizeX && xMod > 0)
        {
            xMod = 0;
        }
        else if(xPos <= 0 && xMod < 0)
        {
            xMod = 0;
        }

        if (zPos >= gridBase.sizeZ && zMod > 0)
        {
            zMod = 0;
        }
        else if (zPos <= 0 && zMod < 0)
        {
            zMod = 0;
        }
        cam.position = new Vector3(xPos + xMod,cam.position.y, zPos + zMod);
    }
}
