using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentLightingscript : MonoBehaviour
{
    //all variables public for test purposes
    public float NowTime = 0; // holds time cycle current time
    public float CheckTime = 0; // rounded Version of time to test against
    public int TimeMultiplier = 1; // the rate which time passes
    public int LightMultiplier = 1; // a multiplier on the amplitude of change to intensity of the light source
    public bool Morning = true; // used to track day and night to decide tint on light source
    float period; //current phase of sun/moon
    Light mod; // object to hold scene light

    // This script must be attached to an object with a light quality to work
    void Start ()
    {
        mod = GetComponent<Light>();
    }


    void Update()
    {
        //track elapsed time
        NowTime = NowTime + Time.deltaTime;
        CheckTime = (int)NowTime;

        //decides colour of light based on time (case statement would be better)
        if (CheckTime == 0)
        {
            mod.color = new Vector4(1, 0.5f, 0.5f, 1);
            Morning = true;
        }

        else if (CheckTime == 15)
        {
            mod.color = new Vector4(0.8f, 0.8f, 0.8f, 1);
        }

        else if (CheckTime == 30)
        {
            Morning = false;
        }

        else if (CheckTime == 45)
        {
            mod.color = new Vector4(0.5f, 0.5f, 1.0f, 1);
        }

        else if (NowTime >= 60)
        {
            NowTime = 0;
        }

        //checks whether to increase or decrease intensity based on time
        if (Morning)
        {
            period = (NowTime / 30);
        }
        else
        {
            period = (1 - ((NowTime-30) /30));
        }
        //sets the light variables
        mod.intensity = period;
        mod.shadowStrength = period;




        //if (this.transform.eulerAngles.y > 0 && this.transform.eulerAngles.y <= 90)
        //{
        //    transform.Rotate(Vector3.up, Time.deltaTime * TimeMultiplier, Space.World);
        //    mod.intensity += Time.deltaTime / LightMultiplier;
        //    mod.shadowStrength += Time.deltaTime / LightMultiplier;
        //}
        //else
        //{
        //    transform.Rotate(Vector3.up, Time.deltaTime * TimeMultiplier, Space.World);
        //    mod.intensity -= Time.deltaTime / LightMultiplier;
        //    mod.shadowStrength -= Time.deltaTime / LightMultiplier;
        //}
        //transform.Rotate(Vector3.up, Time.deltaTime * TimeMultiplier, Space.World);
    }

}

