using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class MapLocationManager : MonoBehaviour {

	public string gangControllingLoc;

	public ObjectLocation locObject;
	[SerializeField] string locLandmark1;
	[SerializeField] string locLandmark2;
	[SerializeField] string locLandmark3;
	[SerializeField] string locLandmark4;



	void Awake () {
		gangControllingLoc = locObject.gangInCharge;

		locLandmark1 = locObject.landmarks[0];
		locLandmark2 = locObject.landmarks[1];
		if (locObject.landmarks.Length > 2) {
			locLandmark3 = locObject.landmarks[2];
		}
		if (locObject.landmarks.Length > 3) {
			locLandmark4 = locObject.landmarks[3];
		}
	}


	void Update () {

	}
}
