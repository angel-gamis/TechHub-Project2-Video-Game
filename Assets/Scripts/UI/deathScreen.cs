using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathScreen : MonoBehaviour
{
	[SerializeField] private Button playAgain;
	[SerializeField] private Button mainMenu;

	private void Start()
	{
		playAgain.onClick.AddListener(ReloadScene);
		mainMenu.onClick.AddListener(StartMenu);
	}

	private void ReloadScene()
	{
		scenesManager.Instance.ReloadScene();
		Destroy(gameObject);
	}

	private void StartMenu()
	{
		scenesManager.Instance.LoadScene(scenesManager.Scene.StartMenu);
	}
}
