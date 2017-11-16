using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalButton : MonoBehaviour {

	public Button animalButton;

	public int animalNum;

	// Use this for initialization
	void Start () 
	{
		Button btn = animalButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void TaskOnClick()
	{
		animalNum ++;
	}
}
