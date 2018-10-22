using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : MonoBehaviour {
	[SerializeField] GameObject attackPrefab;

	public int attackDmg = 10;


	void Start () {
		
	}


	void Update () {
		FirstAttack();
	}


	void FirstAttack() {
		if (Input.GetKeyDown(KeyCode.R)) {
			Vector3 attackPos = transform.position + (transform.forward * .75f);

			GameObject attackCube = Instantiate(attackPrefab, attackPos, transform.localRotation, transform);
			attackCube.GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Impulse);
			Destroy(attackCube, 0.2f);
		}
	}
}
