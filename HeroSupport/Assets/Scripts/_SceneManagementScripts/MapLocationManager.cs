using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class MapLocationManager : MonoBehaviour {

	[SerializeField] ComputerDisplay compDisplay;

//TODO Set up a ScriptableObject for each location, with a list of individual "Landmarks", "High-Tier Activities", etc.
	public string[] highTierActivities;
	public string myHighTierActivity;

	void Start () {
		highTierActivities = new string[] { "High-Tier Activity1", "High-Tier Activity 2", "Blank", "Blank", "Blank", "Blank" };
	}


	void Update () {

	}


	void OnTriggerEnter(Collider enterCol) {
		if (enterCol.gameObject.CompareTag("Player")) {
			if (myHighTierActivity != null || myHighTierActivity != "Blank") {
				//The Nightly Activity from the NightHighTierManager script becomes equal to "myHighTierActivity"
				NightHighTierManager.SetHTCrimeRates(myHighTierActivity);
			}
			else {
				NightManager.SetCrimeRates();

			}
			compDisplay.OpenCompDisplay();
		}
	}


	private void OnTriggerExit(Collider exitCol) {
		if (exitCol.gameObject.CompareTag("Player")) {
			compDisplay.CloseDisplay();
		}
	}
}
