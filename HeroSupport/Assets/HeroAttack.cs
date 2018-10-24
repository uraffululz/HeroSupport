using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour {
	Animator anim;

	public int attackDmg = 10;


	void Start () {
		anim = GetComponent<Animator>();
	}


	void Update () {
		StartAttack();
	}


	void StartAttack() {
		if (Input.GetKeyDown(KeyCode.R)) {
			StartCoroutine("FirstAttack");
		}
	}


	IEnumerator FirstAttack() {
		anim.SetBool("isAttacking", true);
		yield return new WaitForSeconds(1.1f);
		anim.SetBool("isAttacking", false);

	}
}
