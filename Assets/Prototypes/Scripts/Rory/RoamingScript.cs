using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class RoamingScript : MonoBehaviour
{
    public float speed = 3;
    public float dirChangeInt = 1;
    public float maxDirChange = 30;
    public float timePassed;
    public int diceRoll;
    public int diceMax = 6;
    Vector3 dest;
    public float direc = 0;
    CharacterController Pingu;

	// Use this for initialization
	void Awake ()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        Pingu = GetComponent<CharacterController>();
        Roam();
    }
	
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //dest = new Vector3(0, -direc, 0); 
    }

    // Update is called once per frame
    void Update ()
    {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, dest, Time.deltaTime * dirChangeInt);
        transform.TransformDirection(Vector3.forward);
       
        timePassed += Time.deltaTime;
        if (timePassed > dirChangeInt)
        {
            diceRoll = (int)Random.Range(0, diceMax);
            if (diceRoll == 0)
            {
                Roam();
                Pingu.SimpleMove((transform.TransformDirection(Vector3.forward)) * 0);
            }
            else
            {
                Pingu.SimpleMove((transform.TransformDirection(Vector3.forward)) * speed);
            }
        }

        
        
    }

    void Roam()
    {
        var floor = Mathf.Clamp(direc - maxDirChange, 0, 360);
        var ceil = Mathf.Clamp(direc + maxDirChange, 0, 360);
        direc = Random.Range(floor, ceil);
        dest = new Vector3(0, direc, 0);
    }
}
