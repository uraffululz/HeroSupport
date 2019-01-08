using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsCivilian : MonoBehaviour {

	public static StatsCivilian instance;

	public ObjectCivilian civilianObject;

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

	[SerializeField] GameObject myHPBar;

	void Awake () {
		GetComponent<MeshFilter>().mesh = civilianObject.mesh;

		if (StatsPlayer.notoriety > 0) {
			maxHP = civilianObject.health + (int)(StatsPlayer.notoriety / 10); //Fine for testing, but later, divide by 100, 1000, whatever
		}
		else {
			maxHP = civilianObject.health;
		}
		currentHP = maxHP;

		FP = civilianObject.focus;
		FPRegen = civilianObject.focusRegenRate;

		speed = civilianObject.moveSpeed;

		damage = civilianObject.attackDmg;
		attackRate = civilianObject.attackRate;
		meleeDist = civilianObject.meleeDist;
		rangedDist = civilianObject.rangedDist;

		worthNotoriety = civilianObject.notoriety * NightManager.crimeRate;

		primaryAttack = civilianObject.primaryAttackScript;
		secondaryAbility = civilianObject.secondaryAbilityScript;

		//SetupHPBar();
	}


	public void TakeDamage(int damageTaken) {
		currentHP -= damageTaken;

		if (currentHP > maxHP) {
			currentHP = maxHP;
		}
		myHPBar.GetComponent<CivHPBar>().UpdateHPBar();
	}


	//void SetupHPBar() {
	//	Canvas myCanvas = GetComponentInChildren<Canvas>();
	//	//myHPBar = Instantiate(HPBar, myCanvas.transform) as GameObject;
	//	//myHPBar.GetComponent<CivHPBar>().myCiv = gameObject.transform;
	//}
}
