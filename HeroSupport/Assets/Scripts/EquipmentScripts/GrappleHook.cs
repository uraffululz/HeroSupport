using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour {

	GameObject player;
	HeroAttack heroAtk;
	Transform hookSpawn;

	[SerializeField] GameObject hookedObject;
	[SerializeField] GameObject hookedPos;

	public bool wasFired = false;
	public bool retracting = false;
	[SerializeField] bool seekingEnemy = false;


	void OnEnable () {
		player = GameObject.FindGameObjectWithTag("Player");
		heroAtk = player.GetComponent<HeroAttack>();
		hookSpawn = transform.parent;
	}


	void Update () {
		if (retracting && hookedObject != null) {
			transform.position = hookedPos.transform.position;

			Vector3 playerRelevantPos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
			player.transform.position = Vector3.MoveTowards(player.transform.position, playerRelevantPos, 10 * Time.deltaTime);
			//hookedObject.transform.position = new Vector3(transform.position.x, hookedObject.transform.position.y, transform.position.z);

			if (Vector3.Distance(player.transform.position, hookedPos.transform.position) <= 3f) {
				heroAtk.anim.SetBool("hookedObject", true);

				if (hookedObject.CompareTag("Enemy")) {
					//TODO Play the Hero's "OverheadSmash" animation and resolve damage (which is boosted beyond normal attack damage (how much?))
					heroAtk.anim.SetBool("hookedEnemy", true);

					heroAtk.anim.speed = 1;

					hookedObject.GetComponent<EnemyActivity>().enemyNav.enabled = true;
				}
				else {
					heroAtk.anim.SetBool("hookedObject", true);
					heroAtk.anim.SetBool("hookedEnemy", false);
					//retracting = false;
				}

				Debug.Log("Hook retracted. Hero animator should be moving again");
			}
		}
		else if (wasFired) {
			if (/*heroAtk.isTargeting &&*/ seekingEnemy) {
				transform.position = Vector3.MoveTowards(transform.position, 
					new Vector3(heroAtk.target.transform.position.x, transform.position.y, heroAtk.target.transform.position.z), 17 * Time.deltaTime);
				
			}
			else {
				transform.Translate(player.transform.forward, Space.World);
			}
		}

		if (wasFired || retracting) {
			if (Input.GetKey(KeyCode.G)) {
				HookedFinalize();

				heroAtk.anim.Play("CombatIdle");
			}
		}
	}
	

	void OnTriggerEnter(Collider hookedCol) {
		if (wasFired) {
			wasFired = false;
			retracting = true;

			if (hookedObject == null) {
				hookedObject = hookedCol.gameObject;
				hookedPos = new GameObject("HookedEmpty");
				hookedPos.transform.position = gameObject.transform.position;
			}
		
			heroAtk.anim.SetBool("hookHitSomething", true);

			Debug.Log("You hit the " + hookedObject.name + "with your grappling hook. Retracting");

			if (hookedObject.CompareTag("Enemy")) {
				//heroAtk.anim.SetBool("hookedEnemy", true);
				hookedObject.transform.parent = hookedPos.transform;
				hookedObject.GetComponent<EnemyActivity>().enemyNav.enabled = false;
				seekingEnemy = false;

				//StartCoroutine(HookHitAnEnemy());
			}

			heroAtk.anim.speed = 1;
		}
	}


	public void HookFired() { //Animation event triggered by the "Combat_Attack_Ranged_Weapon_OneHanded" animation
		wasFired = true;
		heroAtk.anim.SetBool("hookHitSomething", false);
		heroAtk.anim.SetBool("hookedObject", false);
		heroAtk.anim.SetBool("hookedEnemy", false);

		if (heroAtk.isTargeting) {
			seekingEnemy = true;
		}

		//Stop the player from being able to "unlock" or switch targets on the HeroAttack script
		heroAtk.allowTargeting = false;
	}


	public void HookedFinalize() {
		heroAtk.anim.SetBool("firedGun", false);
		heroAtk.anim.SetBool("hookHitSomething", false);
		heroAtk.anim.SetBool("hookedObject", false);
		heroAtk.anim.SetBool("hookedEnemy", false);

		//Re-parent the hook to the hookSpawn, and reset its localPosition and localRotation
		transform.parent = hookSpawn;
		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = Vector3.zero;

		if (hookedObject != null) {
			hookedObject.transform.parent = null;
			hookedObject = null;
		}

		if (hookedPos != null) {
			Destroy(hookedPos);

		}

		wasFired = false;
		retracting = false;
		
		heroAtk.anim.speed = 1;

		//Re-enable the player's targeting ability in the HeroAttack script
		heroAtk.allowGadget = true;
		heroAtk.allowTargeting = true;
	}
}
