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

	// Add by Caitlin - variables
	// For checking if they should be roaming
	public bool isRoaming;
	// For animator
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


		// Added by Caitlin
		// Call to ChangeAnimationSate()
		StartCoroutine(ChangeAnimationState());
    }

	// Added by Caitlin
	// Change state
	IEnumerator ChangeAnimationState()
	{

		// Get animator
		animate = GetComponentInChildren<Animator>();

		// Do
		do
		{
			// Set animator varibles
			animate.SetBool("isIdle", false);
			animate.SetBool("isWalking", true);  // Set isWalking to true
			animate.SetBool("isSquawking", false);

			animate.SetBool("isSwimming", false);
			// Wait 10 seconds
			yield return new WaitForSecondsRealtime(10); 
			animate.SetBool("isIdle", true);  // Set isIdle to true 
			animate.SetBool("isWalking", false);
			animate.SetBool("isSquawking", false);

			animate.SetBool("isSwimming", false);

			// Wait 10 seconds
			yield return new WaitForSecondsRealtime(10);
			animate.SetBool("isIdle", false); 
			animate.SetBool("isWalking", false);
			animate.SetBool("isSquawking", true);  // Set isSquawking to true

			animate.SetBool("isSwimming", false);
			// Wait 10 seconds
			yield return new WaitForSecondsRealtime(10);
			animate.SetBool("isIdle", false); 
			animate.SetBool("isWalking", false);
			animate.SetBool("isSquawking", false);
			animate.SetBool("isSwimming", true);  // Set isSwimming to true

			// Wait 30 seconds
			yield return new WaitForSecondsRealtime(30);



		}while(isRoaming == true); // While roaming is true
	}
		
    void Roam()
    {
        var floor = Mathf.Clamp(direc - maxDirChange, 0, 360);
        var ceil = Mathf.Clamp(direc + maxDirChange, 0, 360);
        direc = Random.Range(floor, ceil);
        dest = new Vector3(0, direc, 0);
		// Added by Caitlin
		// Set isRoaming to true
		isRoaming = true;

    }
}
