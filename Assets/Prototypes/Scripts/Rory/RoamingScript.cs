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

	// Edit by Caitlin
	public enum PenguinState
	{
		IDAL,
		WALKING,
		SQUAKING,
		SWIMMING
	}

	PenguinState penguinState;

	public bool isRoaming;
	public Animator animate;


	// Use this for initialization
	void Awake ()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        Pingu = GetComponent<CharacterController>();
        Roam();

		// Edit by Caitlin
		// Set state to IDAL
		penguinState = PenguinState.IDAL;
		// Set isRoaming to false
		isRoaming = false;

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

		ChangeAnimationState();
		PlayAnimation();

    }


	IEnumerator ChangeAnimationState()
	{
		do 
		{
			penguinState = PenguinState.WALKING;
			yield return new WaitForSeconds(30);
			penguinState = PenguinState.IDAL;
			penguinState = PenguinState.SQUAKING;
			yield return new WaitForSeconds(20);

		}while(isRoaming == true); 

	}

	void PlayAnimation()
	{
		animate = GetComponentInChildren<Animator>();

		switch(penguinState)
		{
		case PenguinState.IDAL:
			animate.Play("Exported idal animation"); 
			break;
		case PenguinState.WALKING:
			animate.Play("Exported walking animation");
			break;
		case PenguinState.SQUAKING:
			animate.Play("Eported sqauking animation");
			break;
		case PenguinState.SWIMMING:
			animate.Play("Exported swimming animation"); 
			break;
		default:
			//animate.Stop();
			print ("NO ANIMATION");
			break;
		}
	}

    void Roam()
    {
        var floor = Mathf.Clamp(direc - maxDirChange, 0, 360);
        var ceil = Mathf.Clamp(direc + maxDirChange, 0, 360);
        direc = Random.Range(floor, ceil);
        dest = new Vector3(0, direc, 0);
		isRoaming = true;

    }
}
