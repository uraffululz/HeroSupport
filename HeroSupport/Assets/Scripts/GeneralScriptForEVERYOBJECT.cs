using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralScriptForEVERYOBJECT : MonoBehaviour {

	//Material myMat;

	void Start () {
/*		if (gameObject.GetComponent<Material>() != null) {
			myMat = gameObject.GetComponent<Material>();
		}
*/
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

		//Sets the object to its respective rendering layer depending on its position on the upper/lower level of the play area.
		//This affects its rendering by the camera, as well as its interaction with the scene's main light
		if (gameObject.transform.position.y > 4f) {
			gameObject.layer = LayerMask.NameToLayer("UpperLevel - House");
		}
		else {gameObject.layer = LayerMask.NameToLayer("LowerLevel - Hideout");
		}
	}
}
