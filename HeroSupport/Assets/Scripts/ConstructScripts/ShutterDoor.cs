using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterDoor : MonoBehaviour {

	[SerializeField] GameObject dayNightLight;

	GameObject player;
	float distToPlayer;

	Animator anim;


	void Start() {
		anim = GetComponent<Animator>();
		player = GameObject.FindWithTag("Player");
	}


	void Update() {
		distToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
		if (distToPlayer <= 2f && !dayNightLight.GetComponent<DayNightCycle>().dayTime) {
			anim.SetBool("playerActivated", true);
		}
		else if (anim.GetBool("playerActivated") == true && distToPlayer > 1f) {
			anim.SetBool("playerActivated", false);
		}
		else if (dayNightLight.GetComponent<DayNightCycle>().dayTime) {
			anim.SetBool("playerActivated", false);
		}
	}
}
