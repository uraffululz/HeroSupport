using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	[SerializeField] ArenaSceneManagement arenaManager;
	[SerializeField] Camera mainCam;

	Animator anim;

	float walkSpeed = 2;
	float runSpeed = 5;
	public float speedSmoothTime = .2f;
	float speedSmoothVelocity;
	float currentSpeed;
	//int rotSpeed = 10;
	public float turnSmoothTime = .01f;
	float turnSmoothVelocity;

	enum levelLevel {above, below };
	levelLevel myLevel;

	GameObject target = null;
	bool isTargeting = false;



	void Start () {
		mainCam = Camera.main;

		anim = GetComponent<Animator>();

		arenaManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArenaSceneManagement>();
	}
	

	void Update () {
		Move ();
		Targeting();
		Interact();
	}


	void Move () {
		/*		if (Input.GetAxis("Horizontal") != 0f) {
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
		*/
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


	void Targeting() {
		List<GameObject> enemies = arenaManager.enemies;

		if (enemies.Count > 0) {
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
//						ComputerDisplay display = compDisplayBox.GetComponent<ComputerDisplay>();
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
