using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteracting : MonoBehaviour {

	[SerializeField] Camera mainCam;


	void OnEnable () {
		mainCam = Camera.main;
	}


	void Update () {
		Interact();
	}


	void Interact() {
		//TODO Move this function to its own script, where I can find better ways to assign interactive GameObjects
		float reachDist = 1.5f;
		Ray reachRay = new Ray(transform.position, transform.forward);
		Debug.DrawRay(transform.position, transform.forward, Color.magenta);
		RaycastHit reached;

		if (Physics.Raycast(reachRay, out reached, reachDist)) {
			if (reached.collider.tag == "Construct") {
				print("Press E to use the " + reached.collider.name);

				if (Input.GetKeyDown(KeyCode.E)) {
					print("You used the " + reached.collider.name);

					switch (reached.collider.name) {
						case "SpiralStairs":
							CameraController camControl = mainCam.GetComponent<CameraController>();

							if (transform.position.y > 4f) {
								Vector3 stairPos = new Vector3(reached.transform.position.x, 1f, reached.transform.position.z - 1f);
								transform.position = stairPos;
								camControl.myCamState = CameraController.camStates.above;
								camControl.MoveCam();
							}
							else {
								Vector3 stairPos = new Vector3(reached.transform.position.x, 5f, reached.transform.position.z - 1f);
								transform.position = stairPos;
								camControl.myCamState = CameraController.camStates.below;
								camControl.MoveCam();
							}
							break;

						case "Computer":
							if (ClueMaster.eventOngoing) {
								GameObject clueDisplay = GameObject.Find("ClueDisplay");
								clueDisplay.GetComponent<Animator>().SetBool("compActivated", true);
							}
							else {
								Debug.Log("There is no event ongoing. Obtain your first clue.");
							}
							break;

						case "WeaponCase":
							Transform holster = GetComponent<HeroEquipment>().holster;
							HeroEquipment.equippedGadget = reached.collider.GetComponent<Gadgets>().gadgetArray[0];
							HeroEquipment.equippedAmmo = reached.collider.GetComponent<Gadgets>().gadgetArray[1];
							Instantiate(HeroEquipment.equippedGadget, holster.position, holster.rotation, holster);
							Debug.Log("You picked up the grappling gun and ammo");
							break;

						default:
							break;
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
