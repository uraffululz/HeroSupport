using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArenaSceneManagement : MonoBehaviour {

	GameObject player;
	GameObject sidekick;

	[SerializeField] GameObject enemyPrefab;
	[SerializeField] GameObject civilianPrefab;

	[SerializeField] Image completionDisplay;
	[SerializeField] Text notorietyText;

	int enemySpawnNum = 2; //For now
	int civSpawnNum = 2; //For now

	public List<GameObject> enemies;
	public List<GameObject> civilians;

	public int gainedNotoriety = NightManager.baseNotoriety;


	private void Awake() {
		player = GameObject.FindWithTag("Player");
		sidekick = GameObject.FindWithTag("Sidekick");

		if (player != null) {
			player.transform.position = Vector3.zero;
			player.GetComponent<HeroActivity>().enabled = true;
			player.GetComponent<HeroAttack>().enabled = true;
			player.GetComponent<PlayerInteracting>().enabled = false; //until I can figure out how to use the "scene loading" events
		}

		if (sidekick != null) {
			sidekick.transform.position = Vector3.right;
		}

		SpawnCivilians();
		SpawnEnemies();
	}


	void Start () {
		enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
		civilians = new List<GameObject>(GameObject.FindGameObjectsWithTag("Civilian"));
	}


	void Update () {
		
	}


	void SpawnCivilians () {
		for (int i = 0; i < civSpawnNum; i++) {
			//TOMAYBEDO Adjust the civilian spawn points to that they are closer to an enemy (May need to declare their lists up here first)
			float civSpawnX = Random.Range(-10, 10);
			float civSpawnZ = Random.Range(-10, 10);
			Vector3 civSpawnPoint = new Vector3(civSpawnX, 0, civSpawnZ);

			GameObject spawnedCiv = Instantiate(civilianPrefab, civSpawnPoint, Quaternion.identity);
			spawnedCiv.transform.LookAt(Vector3.zero);
		}
	}


	void SpawnEnemies () {
		for (int i = 0; i < enemySpawnNum; i++) {
			float enemySpawnX = Random.Range(-10, 10);
			float enemySpawnZ = Random.Range(-10, 10);
			Vector3 enemySpawnPoint = new Vector3(enemySpawnX, 0, enemySpawnZ);

			GameObject spawnedEnemy = Instantiate(enemyPrefab, enemySpawnPoint, Quaternion.identity);
			spawnedEnemy.transform.LookAt(Vector3.zero);
		}
	}


	public void ActivitySuccess () {
		Debug.Log("Activity SUCCEEDED");
		//Display "activity completion" UI menu
		completionDisplay.GetComponent<Animator>().SetBool("compActivated", true);

		//Add the gainedNotoriety total to the player's notoriety stat and display both amounts
		StatsPlayer.notoriety += gainedNotoriety;
		string gainedOrLost = ((gainedNotoriety >= 0) ? "Notoriety gained: " : "Notoriety lost: ");
		notorietyText.text = (gainedOrLost + gainedNotoriety + " || " + "Total: " + StatsPlayer.notoriety);

		if (!ClueMaster.eventOngoing) {
			int chanceFirstClueFound = Random.Range(0, 100);

			//%chance of finding a "First Clue" on accomplishing the activity
			//TODO Knock down to 10% or so later
				//or have it influenced by the activity's difficulty/rarity/whatever
			if (chanceFirstClueFound < 100) {
				Debug.Log("You found your FIRST CLUE");
				ClueMaster.ChooseEventParameters();
				ClueMaster.GetAClue();
				
			}
		}
	}


	public void ActivityFailed () {
		Debug.Log("Activity FAILED");
		//Display "activity completion" UI menu
		completionDisplay.GetComponent<Animator>().SetBool("compActivated", true);

		//Add the gainedNotoriety total to the player's notoriety stat and display both amounts
		StatsPlayer.notoriety += gainedNotoriety;
		string gainedOrLost = ((gainedNotoriety >= 0) ? "Notoriety gained: " : "Notoriety lost: ");
		notorietyText.text = (gainedOrLost + gainedNotoriety + " || " + "Total: " + StatsPlayer.notoriety);
	}


	public void ReturnToMap () {
		SceneManager.LoadScene("TestMapScene");
	}


	public void ReturnToHideout () {
		SceneManager.LoadScene("HideoutScene");
	}
}
