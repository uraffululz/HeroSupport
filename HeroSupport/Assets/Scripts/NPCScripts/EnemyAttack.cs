using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyActivity))]
public class EnemyAttack : MonoBehaviour {

	Animator Anim;
	[SerializeField] SphereCollider hitboxUpper;

	StatsEnemy enemyStats;
	EnemyActivity enemyAct;

	[SerializeField] float attackTimer;


	void Start () {
		Anim = GetComponent<Animator>();
		enemyStats = GetComponent<StatsEnemy>();
		enemyAct = gameObject.GetComponent<EnemyActivity>();

		attackTimer = enemyStats.attackRate;
	}


	void Update () {
		if (hitboxUpper.enabled) {
			hitboxUpper.enabled = false;
		}

		if (attackTimer <= 0f) {
			if (enemyAct.myState == EnemyActivity.enemyStates.readyToAttack) {
				if (enemyAct.distToTarget <= enemyAct.atkDistMelee) {
					StartCoroutine("MeleeAttack");
				}
	//TOMAYBEDO Maybe they save their ranged attacks for the Player
/*				else if (enemyAct.distToTarget <= enemyAct.atkDistRanged) {
					RangedAttack();
				}
*/			}
		}
		else {
			if (enemyAct.distToTarget <= enemyStats.rangedDist) {
				attackTimer -= Time.deltaTime;
			}
			else {
				attackTimer = enemyStats.attackRate;
			}
		}
	}


	IEnumerator MeleeAttack(){
		if (enemyAct.enemyNav.enabled) {
			enemyAct.enemyNav.destination = transform.position;
		}
		Anim.SetBool("isAttacking", true);
		yield return new WaitForSeconds(1.3f);

		/*hitboxUpper.enabled = true;
		yield return new WaitForSeconds(.2f);
		hitboxUpper.enabled = false;
		yield return new WaitForSeconds(.2f);
		*/

		Anim.SetBool("isAttacking", false);
		attackTimer = enemyStats.attackRate;
		if (enemyAct.enemyNav.enabled && enemyAct.target != null) {
			enemyAct.enemyNav.destination = enemyAct.target.transform.position;
		}
	}


	void AttackHit() {
		hitboxUpper.enabled = true;
		//Debug.Log("Enemy AttackHit event function activated");
	}


	void RangedAttack() {
		Debug.Log("Enemy initiated ranged attack");
	}
}
