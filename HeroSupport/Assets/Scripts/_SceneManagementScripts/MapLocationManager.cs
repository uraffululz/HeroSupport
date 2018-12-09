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


	void Awake () {
		gangControllingLoc = locObject.gangInCharge;

		locLandmark1 = locObject.landmarks[0];
		locLandmark2 = locObject.landmarks[1];
	}


	void Update () {

	}
}
