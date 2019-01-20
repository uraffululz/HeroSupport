using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterDoor : MonoBehaviour {

	[SerializeField] GameObject dayNightLight;
	DayNightCycle DNCycle;
	GameObject doorLight;

	GameObject player;
	float distToPlayer;

	Animator anim;
	BoxCollider doorCol;


	void Start() {
		dayNightLight = GameObject.FindGameObjectWithTag("DayNightLight");
		if (dayNightLight != null) {
			DNCycle = dayNightLight.GetComponent<DayNightCycle>();
		}
		doorLight = GameObject.Find("DoorLight");
		anim = GetComponent<Animator>();
		doorCol = GetComponent<BoxCollider>();
		player = GameObject.FindWithTag("Player");
	}


	void Update() {
		distToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
		if (dayNightLight != null) {
			if (!DNCycle.dayTime) {
				doorLight.SetActive(true);
				doorCol.enabled = false;
				if (distToPlayer <= 2f) {
	//TOMAYBEDO Just have the door open up at night, regardless of its distance to the player
	//Alternatively, make the player ACTIVATE the door to open it
					anim.SetBool("playerActivated", true);
				}
			}
			else if (anim.GetBool("playerActivated") == true && distToPlayer > 1f) {
				anim.SetBool("playerActivated", false);
			}
			else if (DNCycle.dayTime) {
				doorLight.SetActive(false);
				doorCol.enabled = true;
				anim.SetBool("playerActivated", false);
			}
		}
	}
}
