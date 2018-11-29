using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterDoor : MonoBehaviour {

	[SerializeField] GameObject dayNightLight;
	GameObject doorLight;

	GameObject player;
	float distToPlayer;

	Animator anim;
	BoxCollider doorCol;


	void Start() {
		dayNightLight = GameObject.Find("Day-Night Light");
		doorLight = GameObject.Find("DoorLight");
		anim = GetComponent<Animator>();
		doorCol = GetComponent<BoxCollider>();
		player = GameObject.FindWithTag("Player");
	}


	void Update() {
		distToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
		if (dayNightLight != null && !dayNightLight.GetComponent<DayNightCycle>().dayTime) {
			doorLight.SetActive(true);
			doorCol.enabled = false;
			if (distToPlayer <= 2f) {
				anim.SetBool("playerActivated", true);
			}
		}
		else if (anim.GetBool("playerActivated") == true && distToPlayer > 1f) {
			anim.SetBool("playerActivated", false);
		}
		else if (dayNightLight.GetComponent<DayNightCycle>().dayTime) {
			doorLight.SetActive(false);
//TODO This\/ will enable the BoxCollider on the door during the day, preventing the player from passing through
			//doorCol.enabled = true;
			anim.SetBool("playerActivated", false);
		}
	}
}
