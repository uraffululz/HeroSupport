using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaSceneManagement : MonoBehaviour {

	[SerializeField] GameObject enemyPrefab;
	[SerializeField] GameObject civilianPrefab;

	int enemySpawnNum = 2; //For now
	int civSpawnNum = 2; //For now

	public List<GameObject> enemies;
	public List<GameObject> civilians;


	private void Awake() {
		//Spawn Enemies
		for (int i = 0; i < enemySpawnNum; i++) {
			float enemySpawnX = Random.Range(-10, 10);
			float enemySpawnZ = Random.Range(-10, 10);
			Vector3 enemySpawnPoint = new Vector3(enemySpawnX, 0, enemySpawnZ);

			GameObject spawnedEnemy = Instantiate(enemyPrefab, enemySpawnPoint, Quaternion.identity);
			spawnedEnemy.transform.LookAt(Vector3.zero);
		}

		//Spawn Civilians
		for (int i = 0; i < civSpawnNum; i++) {
//TOMAYBEDO Adjust the civilian spawn points to that they are closer to an enemy (May need to declare their lists up here first)
			float civSpawnX = Random.Range(-10, 10);
			float civSpawnZ = Random.Range(-10, 10);
			Vector3 civSpawnPoint = new Vector3(civSpawnX, 0, civSpawnZ);

			GameObject spawnedCiv = Instantiate(civilianPrefab, civSpawnPoint, Quaternion.identity);
			spawnedCiv.transform.LookAt(Vector3.zero);
		}
	}


	void Start () {
		enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
		civilians = new List<GameObject>(GameObject.FindGameObjectsWithTag("Civilian"));
	}


	void Update () {
		
	}
}
