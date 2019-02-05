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

	int enemySpawnNum = 1; //For now
	int civSpawnNum = 3; //For now

	public List<GameObject> enemies;
	public List<GameObject> civilians;

	[SerializeField] public List<Bounds> spawnBounds;

	public int gainedNotoriety = NightManager.baseNotoriety;


	private void Awake() {
		player = GameObject.FindWithTag("Player");
		sidekick = GameObject.FindWithTag("Sidekick");

		spawnBounds = new List<Bounds>();

		if (player != null) {
			Vector3 playerSpawnPos = new Vector3(10, 0, 10);
			player.transform.position = playerSpawnPos;
			player.GetComponent<HeroActivity>().enabled = true;
			player.GetComponent<HeroAttack>().enabled = true;
			player.GetComponent<PlayerInteracting>().enabled = false; //until I can figure out how to use the "scene loading" events

			spawnBounds.Add(player.GetComponent<CapsuleCollider>().bounds);
		}

		if (sidekick != null) {
			sidekick.transform.position = Vector3.right;

			spawnBounds.Add(sidekick.GetComponent<CapsuleCollider>().bounds);

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
		for (int c = 0; c < civSpawnNum; c++) {
			//TOMAYBEDO Adjust the civilian spawn points to that they are closer to an enemy (May need to declare their lists up here first)
			float civSpawnX = Random.Range(0, 20);
			float civSpawnZ = Random.Range(0, 20);
			Vector3 civSpawnPoint = new Vector3(civSpawnX, 0, civSpawnZ);

			GameObject spawnedCiv = Instantiate(civilianPrefab, civSpawnPoint, Quaternion.identity);

			foreach (Bounds colBounds in spawnBounds) {
				if (spawnedCiv.GetComponent<CapsuleCollider>().bounds.Intersects(colBounds)) {
					Destroy(spawnedCiv);
					c--;
				}
			}

			if (spawnedCiv != null) {
				spawnBounds.Add(spawnedCiv.GetComponent<CapsuleCollider>().bounds);
				spawnedCiv.transform.LookAt(Vector3.zero);
			}
		}
	}


	void SpawnEnemies () {
		for (int e = 0; e < enemySpawnNum; e++) {
			float enemySpawnX = Random.Range(0, 20);
			float enemySpawnZ = Random.Range(0, 20);
			Vector3 enemySpawnPoint = new Vector3(enemySpawnX, 0, enemySpawnZ);

			GameObject spawnedEnemy = Instantiate(enemyPrefab, enemySpawnPoint, Quaternion.identity);

			foreach (Bounds colBounds in spawnBounds) {
				if (spawnedEnemy.GetComponent<CapsuleCollider>().bounds.Intersects(colBounds)) {
					Destroy(spawnedEnemy);
					e--;
				}
			}

			if (spawnedEnemy != null) {
				spawnBounds.Add(spawnedEnemy.GetComponent<CapsuleCollider>().bounds);
				spawnedEnemy.transform.LookAt(Vector3.zero);
			}
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
				eventResults.text = ("One of the city's gangs is planning something big");
				DetermineIfClueFound();
			}
		}
		else {
			ClueMaster.ChooseEventParameters();
			DetermineIfClueFound();

			eventResults.text = ("One of the city's gangs is planning something big");			
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
		if (MapSceneManager.currentLocation == MapSceneManager.mapLoc && NodeDetails.myHighTierActivity != null &&
									NodeDetails.myHighTierActivity != "" && NodeDetails.myHighTierActivity != "Blank") {

			MapSceneManager.ResetHighTier();
		}

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
				//if (NightHighTierManager.gangInvolved == ClueMaster.gangInvolvedInEvent) {
					if (chanceClueFound < 100) {
						ClueMaster.GetAClue();
						//Display each clue when found
						clueText.text = ("Clue found: " + ClueMaster.mostRecentClue);
					}
				//}
			}
			else {
				//if (NightManager.gangInvolved == ClueMaster.gangInvolvedInEvent) {
					if (chanceClueFound < 100) {
						ClueMaster.GetAClue();
						//Display each clue when found
						clueText.text = ("Clue found: " + ClueMaster.mostRecentClue);
					}
				//}
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
