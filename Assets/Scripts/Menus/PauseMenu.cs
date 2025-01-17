﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {


	[Header("UI Elements")]
	public GameObject pauseMenuUI;
	public Selectable ResumeButton;

	public Selectable SaveButton;

	public Selectable LoadButton;

	[Header("Positions")]
	public Transform player;
	public Transform cam;

	[Header("Additional Info")]
	public saveLoad saveLoad;

	public bool gameIsPaused = false;

	public Character overworldCharacter;

	void Start() {
		Cursor.visible = false;
		if (overworldCharacter == null)
		{
			Vector3 resumePosition = ResumeButton.transform.position;
			ResumeButton.transform.position = new Vector3(resumePosition.x, resumePosition.y-60, resumePosition.z);
			SaveButton.gameObject.SetActive(false);
			LoadButton.gameObject.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Sinput.GetButtonDown("Start"))
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
		if (gameIsPaused && Sinput.GetButtonDown("Cancel"))
		{
			resume();
		}
	}

	public void resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		Cursor.visible = false;
		gameIsPaused = false;
	}

	void pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		Cursor.visible = true;
		gameIsPaused = true;
		ResumeButton.Select();
	}

	public void saveGame()
	{
		saveLoad.Save(player.position, cam.position, overworldCharacter.CurrentPin.LevelName);
		Debug.Log("Saving Game...");
	}

	public void reloadLevel()
	{
		resume();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
	}

	public void loadSave()
	{
		saveLoad.Load();
		player.position = saveLoad.saveGame.playerPosition.V3;
		cam.position = saveLoad.saveGame.cameraPosition.V3;
		PersistentData.lvlToLoad = saveLoad.saveGame.levelName;
		PersistentData.lastScene = saveLoad.saveGame.lastScene;
		resume();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);

		Debug.Log("Loading Save...");
	}

	public void loadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("StartMenu");
		
		Debug.Log("Loading Menu...");
	}

	public void loadOverworld()
	{
		LoadScene sceneLoader = new LoadScene();
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
		sceneLoader.LoadOverworld();
	}

	public void quitGame()
	{
		Debug.Log("Quitting Game...");
		
		Application.Quit();
	}
}
