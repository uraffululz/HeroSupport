using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroActivity : MonoBehaviour {

	[SerializeField] ArenaSceneManagement arenaManager;

	//StatsPlayer playerStats;

	public HealthBar HPParent;

	//public bool isColliding = false;


	private void Awake() {
		if (SceneManager.GetActiveScene().name != "SampleActivityArena") {
			this.enabled = false;
		}
	}


	void OnEnable () {
		arenaManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArenaSceneManagement>();
		//playerStats = GetComponent<StatsPlayer>();

		HPParent = GameObject.Find("HeroHPBackground").GetComponent<HealthBar>();
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
			StatsPlayer.DamageFatigue(other.GetComponentInParent<StatsEnemy>().damage);
			HPParent.UpdateFatigueBar();

			if (StatsPlayer.currentFatigue >= StatsPlayer.maxFatigue) {
				Debug.Log("You have become physically exhausted and cannot fight");
				arenaManager.ActivityFailed();
			}
			//}
		}
	}

}
