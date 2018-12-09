using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsEnemy : MonoBehaviour {

	public ObjectEnemy enemyObject;

	public int maxHP;
	public int currentHP;
	public int FP;
	public float FPRegen;

	public float speed;

	public int damage;
	public float attackRate;
	public float meleeDist;
	public float rangedDist;

	public int worthNotoriety;

	public Object primaryAttack;
	public Object secondaryAbility;

	[SerializeField] GameObject HPBar;
	GameObject myHPBar;


	void Awake () {
		GetComponent<MeshFilter>().mesh = enemyObject.mesh;

		if (NightHighTierManager.isHighTierActivityHere) {
			maxHP = enemyObject.health + NightHighTierManager.enemyHPBonus;
			FP = enemyObject.focus + NightHighTierManager.enemyFPBonus;
		}
		else {
			maxHP = enemyObject.health + NightManager.enemyHPBonus;
			FP = enemyObject.focus + NightManager.enemyFPBonus;
		}
		currentHP = maxHP;
		FPRegen = enemyObject.focusRegenRate;

		speed = enemyObject.moveSpeed;

		damage = enemyObject.attackDmg;
		attackRate = enemyObject.attackRate;
		meleeDist = enemyObject.meleeDist;
		rangedDist = enemyObject.rangedDist;

		worthNotoriety = enemyObject.notoriety;

		primaryAttack = enemyObject.primaryAttackScript;
		secondaryAbility = enemyObject.secondaryAbilityScript;

		SetupHPBar();
	}


	void LateUpdate() {
		
	}


	public void UpdateStats () {

	}


	public void TakeDamage (int damageTaken) {
		currentHP -= damageTaken;

		if (currentHP > maxHP) {
			currentHP = maxHP;
		}
		myHPBar.GetComponent<EnemyHPBar>().UpdateHPBar();
	}


	void SetupHPBar () {
		Canvas canvas = GameObject.FindObjectOfType<Canvas>();
		myHPBar = Instantiate(HPBar, canvas.transform) as GameObject;
		myHPBar.GetComponent<EnemyHPBar>().myEnemy = this.gameObject.transform;
	}
	
}
