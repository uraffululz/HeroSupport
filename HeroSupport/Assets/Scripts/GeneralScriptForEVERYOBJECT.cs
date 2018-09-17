using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralScriptForEVERYOBJECT : MonoBehaviour {

	Camera mainCam;
	Material myMat;

	void Start () {
		mainCam = Camera.main;
		if (gameObject.GetComponent<Material>() != null) {
			myMat = gameObject.GetComponent<Material>();
		}
	}


	void Update () {
		//Set "House"-level assets to become transparent when the camera is in its "Below" state.
//May need to write my own shader for this, because apparently you can't change the "Rendering Mode" on the standard shader
//Inquire further.
/*
		if (mainCam.GetComponent<CameraController>().myCamState == CameraController.camStates.below && transform.position.y > 4f && myMat != null) {
			myMat.SetFloat("_Mode", 2);

			Color matColor = myMat.color;
			matColor.a = 0f;
			myMat.color = matColor;
		}
*/
	}
}
