using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour {


	Animator anim;

	//StatsPlayer playerStats;

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


	void Awake() {
		
	}


	void Start () {

		anim = GetComponent<Animator>();

		//playerStats = GetComponent<StatsPlayer>();
		walkSpeed = StatsPlayer.valueAgi / 2;
		runSpeed = StatsPlayer.valueAgi;

		//transform.position = Vector3.zero;
	}
	

	void Update () {
		Move ();
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

}
