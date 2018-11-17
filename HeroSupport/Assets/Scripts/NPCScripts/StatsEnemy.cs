using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsEnemy : MonoBehaviour {

	public ObjectEnemy enemyObject;

	public int HP;
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

	void Awake () {
		GetComponent<MeshFilter>().mesh = enemyObject.mesh;

		HP = enemyObject.health;
		FP = enemyObject.focus;
		FPRegen = enemyObject.focusRegenRate;

		speed = enemyObject.moveSpeed;

		damage = enemyObject.attackDmg;
		attackRate = enemyObject.attackRate;
		meleeDist = enemyObject.meleeDist;
		rangedDist = enemyObject.rangedDist;

		worthNotoriety = enemyObject.notoriety;

		primaryAttack = enemyObject.primaryAttackScript;
		secondaryAbility = enemyObject.secondaryAbilityScript;
	}
	
}
