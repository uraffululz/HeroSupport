using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyActivity : MonoBehaviour {

	[SerializeField] ArenaSceneManagement arenaManager;
	GameObject player;
	public List <GameObject> civilians;

	public NavMeshAgent enemyNav;
	Animator enemyAnim;

	StatsEnemy enemyStats;
	EnemyHPBar HPBar;

	public enum enemyStates {idle, pursuing, readyToAttack, stunned};
	public enemyStates myState;

	public enum attackFocus {notApplicable, focusedOnMelee, focusedOnRanged, focusedOnFleeing};
	public attackFocus myFocus;

	//bool isColliding = false;
	[SerializeField] int health;

	public GameObject target = null;
	public float distToTarget;

	//Move these to a scriptableObject for all enemies [DONE, but may move variables and "DetectAndMove" method to EnemyAttack Script]
	//They are referenced in the "EnemyAttack" script, so I'll have to adjust the references there
	public float atkDistMelee;
	public float atkDistRanged;


	void Start () {
		arenaManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArenaSceneManagement>();
		player = GameObject.FindWithTag("Player");
		civilians = arenaManager.civilians;
		enemyNav = GetComponent<NavMeshAgent>();
		enemyAnim = GetComponent<Animator>();
		enemyStats = GetComponent<StatsEnemy>();
		HPBar = GetComponentInChildren<EnemyHPBar>();

		enemyNav.speed = enemyStats.speed;

		health = enemyStats.currentHP;

		atkDistMelee = enemyStats.meleeDist;
		atkDistRanged = enemyStats.rangedDist;
	}


	void Update () {
		//isColliding = false;
		//if (myState == enemyStates.pursuing) {
			if (enemyNav.enabled) {
				DetectAndMove();
			}
		//}
	}


	void OnTriggerEnter (Collider other) {
		//If attacked by the Hero, the Enemy takes damage
		if (other.CompareTag("HeroAttack")) {
			//Stops the Enemy from taking damage multiple times with the same attack (probably due to physics/collider calculations)
			/*if (isColliding == true) {
				Debug.Log("Enemy isColliding already true");
				return;
			}
			else {
				isColliding = true;
			*/
				enemyStats.TakeDamage(StatsPlayer.damage);
				//Debug.Log("Enemy was hit");

				//If the Enemy's health drops to (or below) zero, he dies
				if (enemyStats.currentHP <= 0) {
					Die();
				}
			//}
			
		}
	}


	void Die () {
		HeroAttack playerAttack = player.GetComponent<HeroAttack>();
		playerAttack.isTargeting = false;

		arenaManager.gainedNotoriety += enemyStats.worthNotoriety;

		if (arenaManager.enemies.Contains(gameObject)) {
			arenaManager.enemies.Remove(gameObject);
			if (arenaManager.enemies.Count == 0) {
				Debug.Log("All enemies eliminated");
				arenaManager.ActivitySuccess();
			}
		}

		GetComponent<Rigidbody>().useGravity = false;
		enemyNav.enabled = false;
		GetComponent<Collider>().isTrigger = true;

		enemyAnim.StopPlayback();
		enemyAnim.SetBool("IsDead", true);

		//Destroy(gameObject);
	}


	void DetectAndMove(){
		float distToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

		//If the Player if close enough to pose a threat:
		if (distToPlayer <= atkDistRanged) {
			target = player;
			distToTarget = distToPlayer;
		}
		//Otherwise, attack the nearest Civilian
		else {
			float closestDist = 20f;
			foreach (GameObject civ in civilians) {
				float distToCiv = Vector3.Distance(gameObject.transform.position, civ.transform.position);
				if (distToCiv < closestDist) {
					closestDist = distToCiv;
					target = civ;
					distToTarget = distToCiv;
				}
			}
		}

		if (myState != enemyStates.idle || myState != enemyStates.stunned) {
			if (distToTarget <= atkDistRanged) {
				//float focusedAttackRange;
				myState = enemyStates.readyToAttack;
/*				int combatFocusNum = Random.Range(0, 3);

				if (combatFocusNum == 0) {
					myFocus = attackFocus.focusedOnMelee;
					focusedAttackRange = atkDistMelee;
				}
				else {
					myFocus = attackFocus.focusedOnRanged;
					focusedAttackRange = atkDistRanged;
				}
*/			}
			else {
				myState = enemyStates.pursuing;
				myFocus = attackFocus.notApplicable;
			}
		}

		if (target != null) {
			enemyNav.destination = target.transform.position;
			transform.LookAt(target.transform.position);

			if (distToTarget <= 1.05f) {
				enemyNav.isStopped = true;
			}
			else {
				enemyNav.isStopped = false;
			}
		}
		else {
			enemyNav.enabled = false;
		}

	}
}
