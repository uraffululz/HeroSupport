using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsCivilian : MonoBehaviour {

	public ObjectCivilian civilianObject;

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

	void Start () {
		GetComponent<MeshFilter>().mesh = civilianObject.mesh;

		HP = civilianObject.health;
		FP = civilianObject.focus;
		FPRegen = civilianObject.focusRegenRate;

		speed = civilianObject.moveSpeed;

		damage = civilianObject.attackDmg;
		attackRate = civilianObject.attackRate;
		meleeDist = civilianObject.meleeDist;
		rangedDist = civilianObject.rangedDist;

		worthNotoriety = civilianObject.notoriety;

		primaryAttack = civilianObject.primaryAttackScript;
		secondaryAbility = civilianObject.secondaryAbilityScript;
	}
	

}
