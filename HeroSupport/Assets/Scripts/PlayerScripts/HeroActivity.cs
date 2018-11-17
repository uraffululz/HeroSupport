using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroActivity : MonoBehaviour {

	ArenaSceneManagement arenaManager;

	StatsPlayer playerStats;

	//public bool isColliding = false;


	private void Awake() {
		if (SceneManager.GetActiveScene().name != "SampleActivityArena") {
			this.enabled = false;
		}
	}


	void Start () {
		arenaManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArenaSceneManagement>();
		playerStats = GetComponent<StatsPlayer>();
	}


	void Update () {
	}


	void OnTriggerEnter(Collider other) {
		if (other.tag == "EnemyAttack") {
			/*if (isColliding == true) {
				Debug.Log("Hero isColliding already true");
				return;
			}
			else {
				Debug.Log("Hero isColliding is now true");
				isColliding = true;*/
				playerStats.DamageFatigue(other.GetComponentInParent<StatsEnemy>().damage);
			if (playerStats.currentFatigue >= playerStats.maxFatigue) {
				Debug.Log("You have become physically exhausted and cannot fight");
				arenaManager.ActivityFailed();
			}
			//}
		}
	}

}
