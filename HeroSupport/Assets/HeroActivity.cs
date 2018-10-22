using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroActivity : MonoBehaviour {

	CharacterStats charStats;

	// Use this for initialization
	void Start () {
		charStats = GetComponent<CharacterStats>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	private void OnTriggerEnter(Collider other) {
		if (other.tag == "EnemyAttack") {
			charStats.DamageFatigue(other.GetComponentInParent<EnemyActivity>().atkDmg);
		}
	}
}
