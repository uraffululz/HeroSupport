using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSceneManager : MonoBehaviour {



	void Start () {
		
	}


	void Update () {
		
	}


//TODO Do this whenever the player moves to a new "neighborhood", rather than when the scene is loaded.
	private void OnLevelWasLoaded() {
		NightManager.SetCrimeRates();
	}
}
