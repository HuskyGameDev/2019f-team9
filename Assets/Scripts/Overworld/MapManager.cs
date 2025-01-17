﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
	public Character Character;
	public Pin pin;
	public Text SelectedLevelText;
	public GameObject PauseMenu;
	
	/// <summary>
	/// Use this for initialization
	/// </summary>
	private void Start ()
	{
		// Pass a ref and default the player Starting Pin
		if ( PersistentData.lastScene == "MainMenu" )
		{
			PauseMenu.GetComponent<PauseMenu>().loadSave();
		}
		Character.Initialise(this, pin.toPin(PersistentData.lvlToLoad));
	}


	/// <summary>
	/// This runs once a frame
	/// </summary>
	private void Update()
	{
		// Only check input when character is stopped
		if (Character.IsMoving) return;
		
		// First thing to do is try get the player input
		CheckForInput();
	}

	
	/// <summary>
	/// Check if the player has pressed a button
	/// </summary>
	private void CheckForInput()
	{
		bool isPaused = PauseMenu.GetComponent<PauseMenu>().gameIsPaused;

		if (Sinput.GetButtonDownRepeating("Up"))
		{
			Character.TrySetDirection(Direction.Up);
		}
		else if(Sinput.GetButtonDownRepeating("Down"))
		{
			Character.TrySetDirection(Direction.Down);
		}
		else if(Sinput.GetButtonDownRepeating("Left"))
		{
			Character.TrySetDirection(Direction.Left);
		}
		else if(Sinput.GetButtonDownRepeating("Right"))
		{
			Character.TrySetDirection(Direction.Right);
		}
		//Check for both the submit button and the fact that we are NOT paused
		else if(Sinput.GetButtonDown("Submit") && (isPaused == false))
		{
			PersistentData.lvlToLoad = Character.CurrentPin.LevelName;
			PersistentData.changeScene("Overworld", Character.CurrentPin.SceneToLoad);
		}
	}

	
	/// <summary>
	/// Update the GUI text
	/// </summary>
	public void UpdateGui()
	{
		SelectedLevelText.text = string.Format("Current Level: {0}", Character.CurrentPin.LevelName);
	}
}
