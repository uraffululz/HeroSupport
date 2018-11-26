using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class MapLocationManager : MonoBehaviour {

	[SerializeField] ComputerDisplay compDisplay;

	void Start () {
		
	}


	void Update () {

	}


	void OnTriggerEnter(Collider enterCol) {
		if (enterCol.gameObject.CompareTag("Player")) {
			compDisplay.OpenCompDisplay();
		}
	}


	private void OnTriggerExit(Collider exitCol) {
		if (exitCol.gameObject.CompareTag("Player")) {
			compDisplay.CloseDisplay();
		}
	}
}
