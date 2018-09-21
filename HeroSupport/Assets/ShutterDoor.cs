using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterDoor : MonoBehaviour {

	GameObject player;
	float distToPlayer;

	[SerializeField] Animator anim;


	void Start() {
		player = GameObject.Find("Player");
	}


	void Update() {
		distToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
		if (distToPlayer <= 2f) {
			anim.SetBool("playerActivated", true);
		}
		else if (anim.GetBool("playerActivated") == true && distToPlayer > 1f) {
			anim.SetBool("playerActivated", false);
		}
	}
}
