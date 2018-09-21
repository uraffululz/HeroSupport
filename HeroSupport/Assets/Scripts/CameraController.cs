using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	Camera thisCam;

	[SerializeField]GameObject player;

	public enum camStates {above, below, transitioning};
	public camStates myCamState;
	float transitionSpeed = 1.5f;

	Vector3 camOffset;
	Vector3 upperPos;
	Vector3 lowerPos;


	void Start () {
		thisCam = GetComponent<Camera>();
		myCamState = camStates.below;

		camOffset = new Vector3(0, 10f, -10f);
	}


	void Update () {
		if (myCamState != camStates.transitioning) {
			transform.position = player.transform.position + camOffset;
		} else {
			transform.position = new Vector3(player.transform.position.x + camOffset.x, transform.position.y, player.transform.position.z + camOffset.z);
		}

		if (Input.GetKeyDown(KeyCode.Tab)) {
			MoveCam();
		}
	}

	public void MoveCam () {
		upperPos = new Vector3(transform.position.x, 15f, transform.position.z);
		lowerPos = new Vector3(transform.position.x, 10f, transform.position.z);

		if (myCamState == camStates.below) {
			StartCoroutine(MoveCamVert(upperPos));
			myCamState = camStates.transitioning;

			//Enables rendering of the upper "House" level/rendering layer (as well as ALL other layers) of the play area
			gameObject.GetComponent<Camera>().cullingMask = -1;

			thisCam.backgroundColor = Color.green / 2f; ;
		}
		else if (myCamState == camStates.above) {
			StartCoroutine(MoveCamVert(lowerPos));
			myCamState = camStates.transitioning;

			//Disables rendering of the upper "House" level of the play area
			gameObject.GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("UpperLevel - House"));

			thisCam.backgroundColor = new Color(.47f, .36f, 0.28f, 1f);
		}
	}

	IEnumerator MoveCamVert(Vector3 newPos) {
		float elapsedTime = 0;

		while (elapsedTime < 1f) {
			transform.position = Vector3.Lerp(transform.position, newPos, elapsedTime);
			elapsedTime += Time.deltaTime * transitionSpeed;

			yield return new WaitForEndOfFrame();
		}
		if (transform.position.y == upperPos.y) {
			myCamState = camStates.above;
			if (player.transform.position.y > 5f) {
				camOffset.y = transform.position.y - 6;
			}
			else {
				camOffset.y = transform.position.y - 1;
			}
		} else if (transform.position.y == lowerPos.y) {
			myCamState = camStates.below;
			if (player.transform.position.y > 5) {
				camOffset.y = transform.position.y - 6;
			}
			else {
				camOffset.y = transform.position.y - 1;
		} }

	}
}
