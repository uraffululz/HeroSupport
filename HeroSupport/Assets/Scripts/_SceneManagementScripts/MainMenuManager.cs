using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	
	public void StartGame () {
		SceneManager.LoadScene("HideoutScene");
	}

	public void QuitGame () {
		Debug.Log("You have quit the game");
	}
}
