using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour {
	[SerializeField] SphereCollider hitColUpper;

	Animator anim;

	bool allowAttack1 = true;
	bool allowAttack2 = false;
	bool allowAttack3 = false;

	public int attackDmg = 10;


	void Start () {
		anim = GetComponent<Animator>();
	}


	void Update () {
		StartAttack();
		
	}


	void StartAttack() {
		if (allowAttack1 && Input.GetKeyDown(KeyCode.R)) {
			StartCoroutine("FirstAttack");
		}
		if (allowAttack2 && Input.GetKeyDown(KeyCode.R)) {
			StopCoroutine("FirstAttack");
			//anim.SetBool("isAttacking", false);
			StartCoroutine("SecondAttack");
		}
	}

//TODO Speed up the "AttackChop" animation in Blender, and then adjust these "WaitForSeconds" to match
//Also adjust "WaitForSeconds" below, in SecondAttack()
	IEnumerator FirstAttack() {
		allowAttack1 = false;
		anim.SetBool("isAttacking", true);
		yield return new WaitForSeconds(.9f);

		hitColUpper.enabled = true;
		allowAttack2 = true;
		yield return new WaitForSeconds(.2f);

		hitColUpper.enabled = false;
		allowAttack2 = false;
		yield return new WaitForSeconds(.2f);

		anim.SetBool("isAttacking", false);
		allowAttack1 = true;
	}

	IEnumerator SecondAttack() {
		hitColUpper.enabled = false;
		allowAttack1 = false;
		allowAttack2 = false;
		anim.SetBool("isAttacking", false);
		anim.SetBool("isAttacking2", true);
		yield return new WaitForSeconds(.9f);

		hitColUpper.enabled = true;
		//allowAttack3 = true;
		yield return new WaitForSeconds(.2f);

		hitColUpper.enabled = false;
		//allowAttack3 = false;
		yield return new WaitForSeconds(.2f);

		anim.SetBool("isAttacking2", false);
		allowAttack1 = true;
		allowAttack2 = false;
	}
}
