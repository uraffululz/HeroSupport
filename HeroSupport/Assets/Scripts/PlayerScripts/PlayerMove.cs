using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour {

	[SerializeField] Camera mainCam;

	Animator anim;

	StatsPlayer playerStats;

	public enum activityStates {idle, Busy, Patrolling, Crimefighting, Fatigued, Stressed};
	public activityStates myActiveState;

	float walkSpeed;
	float runSpeed;
	[SerializeField] float speedSmoothTime = .2f;
	float speedSmoothVelocity;
	float currentSpeed;
	//int rotSpeed = 10;
	//[SerializeField] float turnSmoothTime = .01f;
	float turnSmoothVelocity;

	enum levelLevel {above, below };
	levelLevel myLevel;


	void Start () {


		mainCam = Camera.main;

		anim = GetComponent<Animator>();

		playerStats = GetComponent<StatsPlayer>();
		walkSpeed = playerStats.valueAgi / 2;
		runSpeed = playerStats.valueAgi;
	}
	

	void Update () {
		Move ();
		Interact();
	}


	void Move () {
		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
			//Get the Hero's position, as well as any directional input from the player
			Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
			Vector3 inputDir = input.normalized;
			//Is the player running?
			bool running = Input.GetKey(KeyCode.LeftShift);

			//Move the Hero according to the above variables
			//TODO This needs work, as the Hero still has sliding feet. Maybe also adjust the Animator's "Movement Blend Tree".
			//Not a big deal though, as the mannequin is just a placeholder for now
			Vector3 targetPos = transform.position + inputDir;
			float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
			currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
			transform.Translate((targetPos - transform.position) * currentSpeed * Time.deltaTime, Space.World);

		//Snappier, less smooth player rotation
			Vector3 playerRot = Vector3.RotateTowards(transform.position, (transform.position + inputDir.normalized), 1f, 1f);
			transform.LookAt(playerRot);

		//Use this for smoother, but less precise player rotation
			//float targetRotation = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;
			//transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);

			float animSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
			anim.SetFloat("speedPercent", animSpeedPercent, speedSmoothTime, Time.deltaTime);
		}
		else {
			anim.SetFloat("speedPercent", 0);
		}
	}


	void Interact () {
//TODO Move this function to its own script, where I can find better ways to assign interactive GameObjects
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
						if (ClueMaster.eventOngoing) {
							GameObject clueDisplay = GameObject.Find("ClueDisplay");
							clueDisplay.GetComponent<Animator>().SetBool("compActivated", true);
						}
						else {
							Debug.Log("There is no event ongoing. Obtain your first clue.");
						}
						
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
