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
	[SerializeField] Text completionText;
	[SerializeField] Text notorietyText;
	[SerializeField] Text eventResults;
	[SerializeField] Text clueText;

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

		clueText.text = "";

		if (ClueMaster.eventOngoing) {
			if (NightHighTierManager.isEvent) {
				//If the player has just successfully completed the ongoing event
				eventResults.text = "Event completed";
				ClueMaster.EventSuccess();
			}
			else {
				eventResults.text = (ClueMaster.gangInvolvedInEvent + " is planning something");
				DetermineIfClueFound();
			}
		}
		else {
			ClueMaster.ChooseEventParameters();
			DetermineIfClueFound();

			eventResults.text = (ClueMaster.gangInvolvedInEvent + " is planning something");			
		}

		FinishActivity();
	}


	public void ActivityFailed () {
		Debug.Log("Activity FAILED");

		if (ClueMaster.eventOngoing) {
			if (NightHighTierManager.isEvent) {
				eventResults.text = "Event failed";
				ClueMaster.EventFailure();
			}
		}

		FinishActivity();
	}


	void FinishActivity() {
		//Display "activity completion" UI menu
		completionDisplay.GetComponent<Animator>().SetBool("compActivated", true);

		//Add the gainedNotoriety total to the player's notoriety stat and display both amounts
		StatsPlayer.notoriety += gainedNotoriety;
		string gainedOrLost = ((gainedNotoriety >= 0) ? "Notoriety gained: " : "Notoriety lost: ");
		notorietyText.text = (gainedOrLost + gainedNotoriety + " || Total: " + StatsPlayer.notoriety);
	}

	void DetermineIfClueFound() {
		//%chance of finding a "First Clue" on accomplishing the activity
//TODO Knock down to 10% chance or so later (ALSO DO THE SAME ELSEWHERE, WHENEVER DETERMINING "chanceClueFound")
//or have it influenced by the activity's difficulty/rarity/whatever

		int chanceClueFound = Random.Range(0, 100);

		if (NightHighTierManager.isHighTierActivityHere) {
			//High-tier activities give the player a better chance to find a clue
			chanceClueFound = (int)(chanceClueFound / 2);
		}

		if (ClueMaster.numberOfCluesFound < ClueMaster.maxNumberOfClues) {
			if (NightHighTierManager.isHighTierActivityHere) {
				if (NightHighTierManager.gangInvolved == ClueMaster.gangInvolvedInEvent) {
					if (chanceClueFound < 100) {
						ClueMaster.GetAClue();
						//Display each clue when found
						clueText.text = ("Clue found: " + ClueMaster.mostRecentClue);
					}
				}
			}
			else {
				if (NightManager.gangInvolved == ClueMaster.gangInvolvedInEvent) {
					if (chanceClueFound < 100) {
						ClueMaster.GetAClue();
						//Display each clue when found
						clueText.text = ("Clue found: " + ClueMaster.mostRecentClue);
					}
				}
			}
		}
	}


	public void ReturnToMap () {
		NightHighTierManager.isHighTierActivityHere = false;
		SceneManager.LoadScene("TestMapScene");
	}


	public void ReturnToHideout () {
		NightHighTierManager.isHighTierActivityHere = false;
		SceneManager.LoadScene("HideoutScene");
	}
}
