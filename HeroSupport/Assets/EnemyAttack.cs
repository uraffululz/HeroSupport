using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyActivity))]
public class EnemyAttack : MonoBehaviour {

	[SerializeField] GameObject attackSpawn;
	[SerializeField] GameObject attackPrefab;

	EnemyActivity enemyAct;

	[SerializeField] float attackSpeed = 5f;
	[SerializeField] float attackTimer;


	void Start () {
		enemyAct = gameObject.GetComponent<EnemyActivity>();
		attackTimer = attackSpeed;
	}


	void Update () {
		if (attackTimer <= 0f) {
			if (enemyAct.myState == EnemyActivity.enemyStates.attacking) {
				if (enemyAct.distToTarget <= enemyAct.atkDistMelee) {
					MeleeAttack();
				}
	//TOMAYBEDO Maybe they save their ranged attacks for the Player
/*				else if (enemyAct.distToTarget <= enemyAct.atkDistRanged) {
					RangedAttack();
				}
*/				else {
					//Reset the timer if there are no targets within range
					attackTimer = attackSpeed;
				}
			}
		}
		else {
			attackTimer -= 1f * Time.deltaTime;
		}
	}


	void MeleeAttack(){
		
	}


	void RangedAttack() {
		
	}
}
