﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPause = false;

	public GameObject pauseMenuUI;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (GameIsPause) {
				Resume ();
			} else 
			{
				Pause ();
			}
		}
	}

	public void Resume ()
	{
		pauseMenuUI.SetActive (false);
		Time.timeScale = 1f;
		GameIsPause = false;
	}

	void Pause ()
	{
		pauseMenuUI.SetActive (true);
		Time.timeScale = 0f;
		GameIsPause = true;
	}

	public void LoadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("Menu");
	}

	public void QuitGame()
	{
		Debug.Log ("saiu do game");
		Application.Quit ();
	}
}
