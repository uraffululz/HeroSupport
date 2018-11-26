using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSceneManager : MonoBehaviour {
	GameObject player;
	GameObject sidekick;


	void Awake () {
		player = GameObject.FindWithTag("Player");
		sidekick = GameObject.FindWithTag("Sidekick");

		if (player != null) {
			player.transform.position = Vector3.zero;
			player.GetComponent<HeroActivity>().enabled = false;
			player.GetComponent<HeroAttack>().enabled = false;
			player.GetComponent<PlayerInteracting>().enabled = false;
		}

		if (sidekick != null) {
			sidekick.transform.position = Vector3.back;
		}
	}


	void Update () {
		
	}


//TODO Do this whenever the player moves to a new "neighborhood", rather than when the scene is loaded.
	private void OnLevelWasLoaded() {
		NightManager.SetCrimeRates();
	}
}
