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
		IDLE,
		WALKING,
		SQAUWKING,
		SWIMMING
	}

	PenguinState penguinState;

	public bool isRoaming;
	private Animator animate;


	// Use this for initialization
	void Awake ()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        Pingu = GetComponent<CharacterController>();
        Roam();

		// Edit by Caitlin
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

		// Added by Caitlin
		// Call to ChangeAnimationSate()
		StartCoroutine(ChangeAnimationState());
		// Call to PlayAnimation()
		//PlayAnimation();

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

	// Added by Caitlin
	// Change state
	IEnumerator ChangeAnimationState()
	{

		animate = GetComponentInChildren<Animator>();

		do
		{

			animate.SetBool("isIdle", false); 
			animate.SetBool("isWalking", true);
			animate.SetBool("isSquawking", false);
			animate.SetBool("isSwimming", false);
			yield return new WaitForSecondsRealtime(10); 
			animate.SetBool("isIdle", true); 
			animate.SetBool("isWalking", false);
			animate.SetBool("isSquawking", false);
			animate.SetBool("isSwimming", false);
			yield return new WaitForSecondsRealtime(10);
			animate.SetBool("isIdle", false); 
			animate.SetBool("isWalking", false);
			animate.SetBool("isSquawking", true);
			animate.SetBool("isSwimming", false);
			yield return new WaitForSecondsRealtime(10);
			animate.SetBool("isIdle", false); 
			animate.SetBool("isWalking", false);
			animate.SetBool("isSquawking", false);
			animate.SetBool("isSwimming", true);
			yield return new WaitForSecondsRealtime(30);



		}while(isRoaming == true);
	}

	// Added by Caitlin
	// Play animations
	void PlayAnimation()
	{
		// Get animator
		animate = GetComponentInChildren<Animator>(); 

		switch(penguinState)
		{
		// Check for idle state
		case PenguinState.IDLE:
			// Play idle animation
			animate.SetBool("isIdle", true); 
			animate.SetBool("isWalking", false);
			animate.SetBool("isSquawking", false);
			break;
		// Check for walking state
		case PenguinState.WALKING:
			// Play walkign animation
			animate.SetBool("isIdle", false); 
			animate.SetBool("isWalking", true);
			animate.SetBool("isSquawking", false);
			break;
		// Check for squawking state
		case PenguinState.SQAUWKING:
			// Play squawking animation
			animate.SetBool("isIdle", false); 
			animate.SetBool("isWalking", false);
			animate.SetBool("isSquawking", true); 
			break;
		// Check for swimming state
		case PenguinState.SWIMMING:
			// Play swimming animation
			animate.Play("Swimming"); 
			break;
		default:
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
