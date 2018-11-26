using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroAttack : MonoBehaviour {

	ArenaSceneManagement arenaManager;

	[SerializeField] SphereCollider hitColUpper;

	Animator anim;
	//StatsPlayer charStats;

	public bool isTargeting = false;
	GameObject target = null;

	bool allowAttack1 = true;
	bool allowAttack2 = false;
	//bool allowAttack3 = false;

	//public int attackDmg = 10;


	void Awake() {
		if (SceneManager.GetActiveScene().name != "SampleActivityArena") {
			this.enabled = false;
		}
	}


	void OnEnable () {
		arenaManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArenaSceneManagement>();
		anim = GetComponent<Animator>();
		//charStats = GetComponent<StatsPlayer>();
	}


	void Update () {
		if (this.enabled) {
			if (hitColUpper.enabled) {
				hitColUpper.enabled = false;
			}

			Targeting();
			StartAttack();
		}
	
	}


	void Targeting() {
		List<GameObject> enemies = arenaManager.enemies;

		if (enemies.Count > 0) {
			if (!isTargeting) {
				if (Input.GetKeyDown(KeyCode.Q)) {
					isTargeting = true;

					float closestDist = 20f;
					foreach (GameObject enemy in enemies) {
						float distToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
						if (distToEnemy < closestDist) {
							closestDist = distToEnemy;
							target = enemy;
							//distToTarget = distToEnemy;
						}
					}
				}
			}
			else if (isTargeting && target != null) {
				if (Input.GetKeyDown(KeyCode.Q)) {
					isTargeting = false;
					target = null;
				}

				if (target != null) {
					if (Input.GetKeyDown(KeyCode.Comma)) {
						if (enemies[enemies.IndexOf(target)] == enemies[0]) {
							target = enemies[enemies.Count - 1];
						}
						else {
							target = enemies[enemies.IndexOf(target) - 1];
						}
					}
					else if (Input.GetKeyDown(KeyCode.Period)) {
						if (enemies.IndexOf(target) == enemies.Count - 1) {
							target = enemies[0];
						}
						else {
							target = enemies[enemies.IndexOf(target) + 1];
						}
					}
					Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
					transform.LookAt(targetPos);
				}
			}
		}
	}


	void AttackHit() {
		hitColUpper.enabled = true;
		//Debug.Log("Hero AttackHit event function activated");
	}


	void StartAttack() {
		if (allowAttack1 && Input.GetKeyDown(KeyCode.R)) {
			StartCoroutine("FirstAttack");
		}
		if (allowAttack2 && Input.GetKeyDown(KeyCode.R)) {
			StopCoroutine("FirstAttack");
			//anim.SetBool("isAttacking", false);
			StartCoroutine("SecondAttack");
		}
	}

//TODO Speed up the "AttackChop" animation in Blender, and then adjust these "WaitForSeconds" to match
//Also adjust "WaitForSeconds" below, in SecondAttack(), and on the EnemyAttack script
	IEnumerator FirstAttack() {
		allowAttack1 = false;
		anim.SetBool("isAttacking", true);
		yield return new WaitForSeconds(.9f);

		//hitColUpper.enabled = true;
		//Debug.Log("Player Hitbox collider enabled");
		allowAttack2 = true;
		yield return new WaitForSeconds(.2f);

		//hitColUpper.enabled = false;
		allowAttack2 = false;
		yield return new WaitForSeconds(.2f);

		anim.SetBool("isAttacking", false);
		allowAttack1 = true;
	}

	IEnumerator SecondAttack() {
		//hitColUpper.enabled = false;
		allowAttack1 = false;
		allowAttack2 = false;
		anim.SetBool("isAttacking", false);
		anim.SetBool("isAttacking2", true);
		yield return new WaitForSeconds(.9f);

		//hitColUpper.enabled = true;
		//Debug.Log("Player Hitbox collider enabled");
		//allowAttack3 = true;
		yield return new WaitForSeconds(.2f);

		//hitColUpper.enabled = false;
		//allowAttack3 = false;
		yield return new WaitForSeconds(.2f);

		anim.SetBool("isAttacking2", false);
		allowAttack1 = true;
		allowAttack2 = false;
	}
}
