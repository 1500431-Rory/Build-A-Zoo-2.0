using UnityEngine;
using System.Collections;

public class ShaderManip : MonoBehaviour {

    // Use this for initialization
    public Renderer rend;
    public int redness;
    Vector4 colour;
	void Start ()
    {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonUp("Fire1"))
            {
                if (redness > 1)
                {
                    redness = 1;
                }
            else
                {
                    redness = 5;
                }
            
            }
        if (Input.GetButtonUp("Jump"))
        {
        //    redne
        }
        
        colour = new Vector4(redness, 1, 1, 0);
        rend.material.SetColor("_Color", colour);
        transform.Rotate(new Vector3(0, 1, 0));
	}
}
