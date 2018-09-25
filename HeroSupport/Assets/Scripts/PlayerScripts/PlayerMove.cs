using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	[SerializeField]Camera mainCam;

	int moveSpeed = 3;
	int rotSpeed = 10;

	enum levelLevel {above, below };
	levelLevel myLevel;


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
		float reachDist = 1f;
		Ray reachRay = new Ray(transform.position, transform.forward);
		Debug.DrawRay(transform.position, transform.forward, Color.magenta);
		RaycastHit reached;

		if (Physics.Raycast(reachRay, out reached, reachDist)) {
			if (reached.collider.tag == "Construct") {
				print("Press E to use the " + reached.collider.name);

				if (Input.GetKeyDown(KeyCode.E)) {
					print("You used the " + reached.collider.name);

					if (reached.collider.name == "SpiralStairs") {
						CameraController camControl = mainCam.GetComponent<CameraController>();

						if (transform.position.y > 5f) {
							Vector3 stairPos = new Vector3(reached.transform.position.x, 1f, reached.transform.position.z - 1f);
							transform.position = stairPos;
							camControl.myCamState = CameraController.camStates.above;
							camControl.MoveCam();
						} else {
							Vector3 stairPos = new Vector3 (reached.transform.position.x, 6f, reached.transform.position.z - 1f);
							transform.position = stairPos;
							camControl.myCamState = CameraController.camStates.below;
							camControl.MoveCam();
						}
					}
					else if (reached.collider.name == "Computer") {
						GameObject compDisplayBox = GameObject.Find("ComputerDisplay");
						ComputerDisplay display = compDisplayBox.GetComponent<ComputerDisplay>();
						Animator compDisplayAnim = compDisplayBox.GetComponent<Animator>();

						compDisplayAnim.SetBool("compActivated", true);
					}
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
