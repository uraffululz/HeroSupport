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


	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("Player")) {
			compDisplay.OpenCompDisplay();
		}
	}
}
