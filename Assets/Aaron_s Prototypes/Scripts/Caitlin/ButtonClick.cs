using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {

	public Button shelterButton;
	RaycastClass ray;

	// Use this for initialization
	void Start () 
	{
		Button btn = shelterButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

		ray = GameObject.Find("Shelter").GetComponent<RaycastClass>();
	}

	void TaskOnClick()
	{
		ray.isClicked = true;
		Debug.Log("You have clicked the button!");
	}
}
