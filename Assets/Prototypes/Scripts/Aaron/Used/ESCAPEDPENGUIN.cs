using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ESCAPEDPENGUIN : MonoBehaviour
    {

    Collision collisionInfo;

    //private void Update()
    //{
        //OnCollisionExit(collisionInfo);
        //OnCollisionEnter(collisionInfo);
    //}
    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("EnclosureMarker"))
        {
            Debug.Log("Penguin out of Zoo");
        }
    }
}


