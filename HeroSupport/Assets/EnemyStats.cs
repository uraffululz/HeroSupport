using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

	int maxHealth = 100;
	int currentHealth;


	void Start () {
		currentHealth = maxHealth;
	}


	void Update () {
		
	}


	public void TakeDamage(int damage) {
		currentHealth -= damage;
	}
}
