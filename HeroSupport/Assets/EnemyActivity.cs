using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyActivity : MonoBehaviour {

	GameObject player;
	public List <GameObject> civilians;

	NavMeshAgent enemyNav;

	public enum enemyStates {idle, pursuing, attacking, stunned};
	public enemyStates myState;

	public int health = 50;

	public GameObject target = null;
	public float distToTarget;

	//Move these to a scriptableObject for all enemies
	//They are referenced in the "EnemyAttack" script, so I'll have to adjust the references there
	public int atkDmg;
	public float atkDistMelee = 2f;
	public int atkDmgMelee = 10;
	public float atkDistRanged = 5f;
	public int atkDmgRanged = 10;


	void Start () {
		player = GameObject.FindWithTag("Player");
		civilians = new List<GameObject>(GameObject.FindGameObjectsWithTag("Civilian"));
		enemyNav = GetComponent<NavMeshAgent>();
	}


	void Update () {
		//if (myState == enemyStates.pursuing) {
			DetectAndMove();
		//}
	}


	private void OnTriggerEnter(Collider other) {
		//If attacked by the Hero, the Enemy takes damage
		if (other.tag == "HeroAttack") {
			health = health - other.gameObject.GetComponentInParent<HeroAttack>().attackDmg;

			//If the Enemy's health drops to (or below) zero, he dies
			if (health <= 0) {
				Die();
			}
		}
	}


	void Die () {
		Destroy(gameObject);
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
				myState = enemyStates.attacking;
			}
			else {
				myState = enemyStates.pursuing;
			}
		}
		
		enemyNav.destination = target.transform.position;
/*		if (distToTarget < .1f) {
			enemyNav.isStopped = true;
		}
		else {
			enemyNav.isStopped = false;
		}
*/
		}
}
