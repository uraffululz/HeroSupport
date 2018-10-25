using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHitbox : MonoBehaviour {

	SphereCollider sphereCol;


	void Start () {
		sphereCol = GetComponent<SphereCollider>();
	}


	void Update () {
		
	}


	private void OnTriggerEnter(Collider other) {
		HeroAttack myAttack = GetComponentInParent<HeroAttack>();

		if (other.tag == "Enemy") {
			other.GetComponent<EnemyStats>().TakeDamage(myAttack.attackDmg);
		}
	}
}
