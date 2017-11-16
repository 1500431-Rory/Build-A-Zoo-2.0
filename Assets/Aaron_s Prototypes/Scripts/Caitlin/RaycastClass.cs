using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RaycastClass : MonoBehaviour {

	public Vector3 pos;
	public bool isClicked;
	public bool isPlaced;

	// Use this for initialization
	void Start () 
	{
		pos = transform.position;
		isClicked = false;
		isPlaced = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0)) // check left mouse button has been pressed
		{ 
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(isClicked == true)
			{
				isPlaced = true;
			}

			if(isPlaced == true)
			{
				if(Physics.Raycast(ray, out hit)) // check if ray has hit something
				{
					pos = hit.point; // set position to mouse hit point
					transform.position = pos; // move object to postion

					isClicked = false;
					isPlaced = false;
				}
			}
		}
	}
}
