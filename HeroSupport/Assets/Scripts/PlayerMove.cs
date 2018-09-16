using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	int moveSpeed = 3;
	int rotSpeed = 10;


	void Start () {
		
	}
	

	void Update () {
		Move ();
		Interact();
	}


	void Move () {
		if (Input.GetAxis("Horizontal") != 0f) {
			Vector3 playerPos = transform.position;
			playerPos += Vector3.right * moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
			transform.position = playerPos;

			Vector3 rotHor = Vector3.RotateTowards(transform.position, (Vector3.right * Input.GetAxis("Horizontal")).normalized, rotSpeed, 0.0f);
			transform.rotation = Quaternion.LookRotation(rotHor);
		}
		if (Input.GetAxis("Vertical") != 0f) {
			Vector3 playerPos = transform.position;
			playerPos += Vector3.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
			transform.position = playerPos;

			Vector3 rotVert = Vector3.RotateTowards(transform.position, (Vector3.forward * Input.GetAxis("Vertical")).normalized, rotSpeed, 0.0f);
			transform.rotation = Quaternion.LookRotation(rotVert);
		}
	}


	void Interact () {
		float reachDist = 0.75f;
		Ray reachRay = new Ray(transform.position, transform.forward);
		Debug.DrawRay(transform.position, transform.forward, Color.magenta);
		RaycastHit reached;

		if (Physics.Raycast(reachRay, out reached, reachDist)) {
			if (reached.collider.tag == "Construct") {
				print("Press E to use the " + reached.collider.name);

				if (Input.GetKeyDown(KeyCode.E)) {
					print("You used the " + reached.collider.name);
				}
			}
			else if (reached.collider.tag == "Hero") {
				GameObject daHero = reached.collider.gameObject;
				if (daHero.GetComponent<DialogueTrigger>() != null) {
					DialogueTrigger heroDialogue = daHero.GetComponent<DialogueTrigger>();

					print("Press E to talk to " + heroDialogue.dialogue.name);

					if (Input.GetKeyDown(KeyCode.E)) {
						heroDialogue.SetupDialogue();
					}
				}
			}
		}
	}
}
