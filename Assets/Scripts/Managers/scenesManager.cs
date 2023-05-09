using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenesManager : MonoBehaviour
{
	public static scenesManager Instance;

	public enum Scene
	{
		StartMenu,
		Level00,
		Level01
	}

	private void Awake()
	{
		Instance = this;
	}

	public void LoadScene(Scene scene)
	{
		// Load Any Chosen Level
		SceneManager.LoadScene(scene.ToString());
	}

	public void StartGame()
	{
		// Load Starting Level
		SceneManager.LoadScene(Scene.Level00.ToString());
	}

	public void ReloadScene()
	{
		// Play A Level Again
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void LoadNextScene()
	{
		// Load the active scene + 1 in the build index
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
