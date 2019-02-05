using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroAttack : MonoBehaviour {

	ArenaSceneManagement arenaManager;

	[SerializeField] SphereCollider hitColUpper;

	public Animator anim;
	//StatsPlayer charStats;
	HeroEquipment myEquipment;

	public bool allowTargeting = true;
	public bool isTargeting = false;
	public GameObject target = null;

	bool allowAttack1 = true;
	bool allowAttack2 = false;
	//bool allowAttack3 = false;

	//public int attackDmg = 10;

	public bool allowGadget = true;
	GameObject projectile;


	void Awake() {
		if (SceneManager.GetActiveScene().name != "SampleActivityArena") {
			this.enabled = false;
		}
		else {
			this.enabled = true;
		}
	}


	void OnEnable () {
		arenaManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArenaSceneManagement>();
		anim = GetComponent<Animator>();
		anim.SetBool("inCombat", true);
		//charStats = GetComponent<StatsPlayer>();
		myEquipment = GetComponent<HeroEquipment>();
	}


	void Update () {
		//if (this.enabled) {
			if (hitColUpper.enabled) {
				hitColUpper.enabled = false;
			}

			Targeting();
			StartAttack();
		UseGadget();
		//}
	
	}


	void AttackHit() { //Event triggered by the "Combat_Melee_Chop" animation
		hitColUpper.enabled = true;
		//Debug.Log("Hero AttackHit animation event function activated");
	}


	void Targeting() {
		List<GameObject> enemies = arenaManager.enemies;

		if (enemies.Count > 0) {
			if (allowTargeting) {
				if (!isTargeting) {
					if (Input.GetKeyDown(KeyCode.Q)) {
						isTargeting = true;

						float closestDist = 20f;
						foreach (GameObject enemy in enemies) {
							float distToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
							if (distToEnemy < closestDist) {
								closestDist = distToEnemy;
								target = enemy;
								//distToTarget = distToEnemy;
							}
						}
					}
				}
				else if (isTargeting && target != null) {
					if (Input.GetKeyDown(KeyCode.Q)) {
						isTargeting = false;
						target = null;
					}

					if (target != null) {
						if (Input.GetKeyDown(KeyCode.Comma)) {
							if (enemies[enemies.IndexOf(target)] == enemies[0]) {
								target = enemies[enemies.Count - 1];
							}
							else {
								target = enemies[enemies.IndexOf(target) - 1];
							}
						}
						else if (Input.GetKeyDown(KeyCode.Period)) {
							if (enemies.IndexOf(target) == enemies.Count - 1) {
								target = enemies[0];
							}
							else {
								target = enemies[enemies.IndexOf(target) + 1];
							}
						}
						Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
						transform.LookAt(targetPos);
					}
				}
			}
		}
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
	

	IEnumerator FirstAttack() {
		allowAttack1 = false;
		anim.SetBool("isAttacking", true);
		yield return new WaitForSeconds(.55f);

		allowAttack2 = true;
		yield return new WaitForSeconds(.2f);

		allowAttack2 = false;
		yield return new WaitForSeconds(.2f);

		anim.SetBool("isAttacking", false);
		allowAttack1 = true;
	}


	IEnumerator SecondAttack() {
		//hitColUpper.enabled = false;
		allowAttack1 = false;
		allowAttack2 = false;
		anim.SetBool("isAttacking", false);
		anim.SetBool("isAttacking2", true);
		yield return new WaitForSeconds(.55f);

		//allowAttack3 = true;
		yield return new WaitForSeconds(.2f);

		//allowAttack3 = false;
		yield return new WaitForSeconds(.2f);

		anim.SetBool("isAttacking2", false);
		allowAttack1 = true;
		allowAttack2 = false;
	}


	void UseGadget() {
		if (HeroEquipment.equippedGadget != null && allowGadget && Input.GetKeyDown(KeyCode.G)) {
			switch (HeroEquipment.equippedGadget.name) {
				case "GrappleGun":
					Transform projectileSpawn = myEquipment.myGadget.transform.GetChild(0);

//TODO Just instantiate this in the HeroEquipment script, right along with spawning the gadget itself
					if (projectile == null) {
						projectile = Instantiate(HeroEquipment.equippedAmmo, projectileSpawn.position, Quaternion.identity, projectileSpawn);
					}
					projectile.transform.localEulerAngles = Vector3.zero;
					GrappleHook hookScript = projectile.GetComponent<GrappleHook>();
						if (!hookScript.wasFired && !hookScript.retracting) {
							StartCoroutine(UseGrappleGun());
						}
					break;

				default:
					break;
			}
		}
	}


	IEnumerator UseGrappleGun() {
		allowGadget = false;

		anim.SetBool("firedGun", true);
		yield return new WaitForSeconds(.3f);

		//Put the gun in the Hero's hand
		myEquipment.myGadget.transform.parent = myEquipment.equipRightHand;
		myEquipment.myGadget.transform.localPosition = Vector3.zero;
		myEquipment.myGadget.transform.localRotation = Quaternion.Euler(Vector3.up * 90);

		
		//yield return new WaitForSeconds(.44f);

	}

	void PlayerFiredProjectile() {
		anim.speed = 0;

		projectile.GetComponent<GrappleHook>().HookFired();
	}

	void HookHeroLanding() {
		projectile.GetComponent<GrappleHook>().HookedFinalize();
	}

	void HeroOverheadSmash () {
		hitColUpper.enabled = true;

		projectile.GetComponent<GrappleHook>().HookedFinalize();

	}

	void PlayerHolsteredWeapon() {
		StartCoroutine("HolsterWeapon");
	}


	IEnumerator HolsterWeapon() {
		yield return new WaitForSeconds(.40f);

		//Re-attach the gun to the Hip Holster
		//Destroy(projectile);
		myEquipment.myGadget.transform.parent = myEquipment.holster;
		myEquipment.myGadget.transform.localPosition = Vector3.zero;
		myEquipment.myGadget.transform.localRotation = Quaternion.Euler(Vector3.zero);
		yield return new WaitForSeconds(.36f);
	}

}
