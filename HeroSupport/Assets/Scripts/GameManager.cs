using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	GameObject menuManager = null;
	//GameObject hideoutManager = null;


	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy(gameObject);
		}
		GameObject.DontDestroyOnLoad(gameObject);

		menuManager = GameObject.Find("MainMenuManager");

		menuManager.SetActive(true);

	}


	private void Start() {
		
	}


	
	private void OnLevelWasLoaded(int level) {
		if (SceneManager.GetActiveScene().name == "_MainMenu") {
			menuManager.SetActive(true);


		}
		else if (SceneManager.GetActiveScene().name == "HideoutScene") {

			menuManager.SetActive(false);
		}
		else if (SceneManager.GetActiveScene().name == "TestMapScene") {

		}
		else if (SceneManager.GetActiveScene().name == "SampleActivityArena") {

		}
	}
	
}
