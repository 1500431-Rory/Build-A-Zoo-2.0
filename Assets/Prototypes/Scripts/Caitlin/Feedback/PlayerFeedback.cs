using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeedback : MonoBehaviour {

	enum EnclosureType
	{
		PENGUIN,
		OTHER,
		NONE
	}

	enum AnimalType
	{
		PENGUIN,
		OTHER,
		NONE
	}

	private EnclosureType enclosureType;
	private AnimalType animalType;

	// Use this for initialization
	void Start () 
	{
		// Set EnclosureType to equal NONE
		enclosureType = EnclosureType.NONE;
		// Set AnimalType to equal NONE
		animalType = AnimalType.NONE;
	}
	
	// Update is called once per frame
	void Update () 
	{
		ChangeEnclosureType();
		ChangeAnimalType();
	}

	// Test function for changing enclosure types
	void ChangeEnclosureType()
	{
		switch(Input.inputString)
		{
		case "p":
			enclosureType = EnclosureType.PENGUIN;
			print("eType: " + enclosureType);
			break;
		case "o":
			enclosureType = EnclosureType.OTHER;
			print("eType: " + enclosureType);
			break;
		default:
			enclosureType = EnclosureType.NONE;
			print("eType: " + enclosureType);
			break;
		}
	}

	// Test function for changing enclosure types
	void ChangeAnimalType()
	{
		switch(Input.inputString)
		{
		case "l":
			animalType = AnimalType.PENGUIN;
			print("aType: " + animalType);
			break;
		case "k":
			animalType = AnimalType.OTHER;
			print("aType: " + animalType);
			break;
		default:
			animalType = AnimalType.NONE;
			print("aType: " + animalType);
			break;
		}
		
	}

	// For Visual feedback
	void VisualFeedback()
	{
		// Bring up a graphic when 
		
	}

	// For Audio feeback
	void AudioFeedback()
	{
		// Play audio when annimal is placed in wrong enclosure
	}
}
