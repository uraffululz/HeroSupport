using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : MonoBehaviour {

	GameObject player;
	float distToPlayer;

	[SerializeField] Animator anim;


	void Start () {
		player = GameObject.Find("Player");
	}


	void Update () {
		distToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
		if (distToPlayer <= 2f) {
			anim.SetBool("isOpen", true);
		}
		else if (anim.GetBool("isOpen") == true && distToPlayer > 1f) {
			anim.SetBool("isOpen", false);
		}
	}
}
