using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

	public Button animal;
	public Button fence;
	public Button rock;
	public Button toy;
	public Button menu;

	void Start()
	{
		// Get Button Component
		Button animalBtn = animal.GetComponent<Button>();
		Button fenceBtn = fence.GetComponent<Button>();
		Button rockBtn = rock.GetComponent<Button>();
		Button toyBtn = toy.GetComponent<Button>();
		Button menuBtn = menu.GetComponent<Button>();

		// Add a listenr to button
		animalBtn.onClick.AddListener(TaskOnClick);
		fenceBtn.onClick.AddListener(TaskOnClick);
		rockBtn.onClick.AddListener(TaskOnClick);
		toyBtn.onClick.AddListener(TaskOnClick);
		menuBtn.onClick.AddListener(TaskOnClick);

	}

	void TaskOnClick()
	{
		

	}
}
