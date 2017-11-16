using UnityEngine;
using System.Collections;

public class ShaderManip : MonoBehaviour {

    // init required variables


    public Renderer rend; // the renderer for the scene, render functions from this
    public int redness; // is public for testing purposes can be private on release
    public Vector4 colour; // holds colour of light, public to allow for script based events change

	void Start ()
    {
        // creates renderer and sets ambient light on object
        rend = GetComponent<Renderer>();
        RenderSettings.ambientIntensity = 0.2f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //test functon to check the masking that can be applied to objects by changing shader on object
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
        
        colour = new Vector4(redness, 1, 1, 0);
        //This applies a colour mask directly over whatever texture is applied.
        rend.material.SetColor("_Color", colour);
	}
}
