using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilianActivity : MonoBehaviour {

	[SerializeField] ArenaSceneManagement arenaManager;
	GameObject targetEnemy;

	NavMeshAgent civNav;
	Animator anim;

	public int civHealth = 50;

	[SerializeField] float distToEnemy;


	void Start () {
		arenaManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArenaSceneManagement>();

		civNav = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
	}


	void Update () {
		DetectEnemies();
		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "EnemyAttack") {
			civHealth = civHealth - other.GetComponentInParent<EnemyActivity>().atkDmg;
		}
	}


	void DetectEnemies () {
		float closestEnemyDist = 20f;
		foreach (GameObject enemy in arenaManager.enemies) {
			distToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distToEnemy < closestEnemyDist) {
				closestEnemyDist = distToEnemy;
				targetEnemy = enemy;
			}
		}

		if (targetEnemy != null && closestEnemyDist <= 5.0f) {
			civNav.destination = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, -1);
			transform.LookAt(civNav.nextPosition);
		}
		else {
//For now they stand still when not being pursued. In the future maybe they keep running for an exit/some kind of safety
			civNav.destination = transform.position;
		}
	}
}
