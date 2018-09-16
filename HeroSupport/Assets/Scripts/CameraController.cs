using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField]GameObject player;

	enum camStates {above, below, transitioning};
	[SerializeField]camStates myCamState;

	Vector3 camOffset;
	Vector3 upperPos;
	Vector3 lowerPos;


	void Start () {
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
			upperPos = new Vector3(transform.position.x, 15f, transform.position.z);
			lowerPos = new Vector3(transform.position.x, 10f, transform.position.z);

			if (myCamState == camStates.below) {
				StartCoroutine(MoveCamVert(upperPos));
				myCamState = camStates.transitioning;
			}
			else if (myCamState == camStates.above) {
				StartCoroutine(MoveCamVert(lowerPos));
				myCamState = camStates.transitioning;
			}
		}
	}

	IEnumerator MoveCamVert(Vector3 newPos) {
		float elapsedTime = 0;

		while (elapsedTime < 1f) {
			transform.position = Vector3.Lerp(transform.position, newPos, elapsedTime);
			elapsedTime += Time.deltaTime;

			yield return new WaitForEndOfFrame();
		}
		if (transform.position.y == upperPos.y) {
			myCamState = camStates.above;
			camOffset.y = transform.position.y - 1;
		} else if (transform.position.y == lowerPos.y) {
			myCamState = camStates.below;
			camOffset.y = transform.position.y - 1;
		}
	}
}
