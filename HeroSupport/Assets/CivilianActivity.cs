using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilianActivity : MonoBehaviour {

	[SerializeField] GameObject enemy;

	NavMeshAgent civNav;

	public int civHealth = 50;

	float distToEnemy;


	void Start () {
		civNav = GetComponent<NavMeshAgent>();
	}


	void Update () {
		distToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

		if (distToEnemy <= 5.0f) {
			civNav.destination = Vector3.MoveTowards(transform.position, enemy.transform.position, -1);
		}
		else {
			civNav.destination = transform.position;
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "EnemyAttack") {
			civHealth = civHealth - other.GetComponentInParent<EnemyActivity>().atkDmg;
		}
	}
}
