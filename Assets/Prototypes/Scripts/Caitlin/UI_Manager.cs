using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Manager : MonoBehaviour {

	public GameObject pauseMenuImage;

	void Start()
	{


	}

	// For pauing the game
	 void PausetheGame()
	{
		// Pause/unpause the game
		// If time is not 0
		if(Time.timeScale != 0)
		{
			// Set time to equal zero
			Time.timeScale = 0;
		} else
		{
			// Set time tp equal one
			Time.timeScale = 1;
		}

		// Make the game greyscale

	}
		
	// Open animal tab
	void AnimalTab()
	{
		
	}
}
