using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

    public Camera cam1;
    public Camera cam2;

    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }


    public void Switch()
    { 
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam2.enabled;
        if (cam1.enabled)
        {
            cam1.transform.position.Set(cam2.transform.position.x,cam1.transform.position.y,cam2.transform.position.z);
        }
        else
        {
            cam2.transform.position.Set(cam1.transform.position.x, cam2.transform.position.y, cam1.transform.position.z);
        }

    }

}