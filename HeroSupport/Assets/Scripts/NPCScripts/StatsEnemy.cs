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

	//[SerializeField] GameObject HPBar;
	[SerializeField] GameObject myHPBar;


	void Awake () {
		GetComponent<MeshFilter>().mesh = enemyObject.mesh;

		if (NightHighTierManager.isHighTierActivityHere) {
			maxHP = enemyObject.health + NightHighTierManager.enemyHPBonus;
			FP = enemyObject.focus + NightHighTierManager.enemyFPBonus;
			FPRegen = enemyObject.focusRegenRate + NightHighTierManager.enemyIntBonus;

			speed = enemyObject.moveSpeed + NightHighTierManager.enemyAgiBonus;

			damage = enemyObject.attackDmg + NightHighTierManager.enemyStrBonus;
			attackRate = enemyObject.attackRate - (NightHighTierManager.enemyAgiBonus/10);

			worthNotoriety = enemyObject.notoriety + NightHighTierManager.enemyNotorietyBonus;
		}
		else {
			maxHP = enemyObject.health + NightManager.enemyHPBonus;
			FP = enemyObject.focus + NightManager.enemyFPBonus;
			FPRegen = enemyObject.focusRegenRate + NightManager.enemyIntBonus;

			speed = enemyObject.moveSpeed + NightManager.enemyAgiBonus;

			damage = enemyObject.attackDmg + NightManager.enemyStrBonus;
			attackRate = enemyObject.attackRate - (NightManager.enemyAgiBonus/10);

			worthNotoriety = enemyObject.notoriety + NightManager.enemyNotBonus;
		}

		currentHP = maxHP;
		meleeDist = enemyObject.meleeDist;
		rangedDist = enemyObject.rangedDist;

		primaryAttack = enemyObject.primaryAttackScript;
		secondaryAbility = enemyObject.secondaryAbilityScript;

		//SetupHPBar();
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


	//void SetupHPBar () {
	//	Canvas myCanvas = GetComponentInChildren<Canvas>();
	//	//myHPBar = Instantiate(HPBar, myCanvas.transform) as GameObject;
	//	//myHPBar.GetComponent<EnemyHPBar>().myEnemy = gameObject.transform;
	//}
	
}
