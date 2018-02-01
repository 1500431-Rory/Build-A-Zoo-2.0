using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class DetectMatSetEnum : MonoBehaviour
    {

        Material m_Material;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseOver()
        {
            // Change the Color of the GameObject when the mouse hovers over it
            m_Material = GetComponent<Renderer>().material;
            Debug.Log("Materials " + m_Material);
        
        }
    }
