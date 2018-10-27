﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool gameIsPaused = false;

	public Transform player;
	public Transform camera;
	
	public GameObject pauseMenuUI;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (gameIsPaused)
			{
				resume();
			}
			else
			{
				pause();
			}
		}
	}

	public void resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
	}

	void pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		gameIsPaused = true;
	}

	public void saveGame()
	{
		SaveGame.Instance.playerPosition = player.position;
		SaveGame.Instance.cameraPosition = camera.position;
		SaveGame.Save();
		Debug.Log("Saving Game...");
	}

	public void loadSave()
	{
		SaveGame.Load();
		player.position = SaveGame.Instance.playerPosition;
		camera.position = SaveGame.Instance.cameraPosition;
		Debug.Log("Loading Save...");
	}

	public void loadMenu()
	{
		//Time.timeScale = 1f;
		//SceneManager.LoadScene("Menu");
		
		Debug.Log("Loading Menu...");
	}

	public void quitGame()
	{
		Debug.Log("Quitting Game...");
		Application.Quit();
	}
}