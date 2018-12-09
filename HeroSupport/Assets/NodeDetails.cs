using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDetails : MonoBehaviour {

	NodeManager nodeManager;
	public ObjectMapNode myNodeObject;
	//public string[] myNeighbors;
	public List<GameObject> neighborObjects = new List<GameObject>();

	[SerializeField] MapSceneManager mapManager;
	[SerializeField] ComputerDisplay compDisplay;

	GameObject myNeighborhood;
	MapLocationManager myLocManager;
	string myLandmark;
	public bool isGangHQ;

	public int nodeNum;
	public string gangName;
	public Color gangColor;

	public bool battlingForControl = false;

	public bool isEventHappeningHere = false;

//TODO Set up a ScriptableObject for each location, with a list of individual "Landmarks", "High-Tier Activities", etc.
	public string[] highTierActivities;
	public string myHighTierActivity;



	void Awake() {
		nodeManager = GetComponentInParent<NodeManager>();

		//myNeighbors = myNodeObject.neighborNodes;
		myLandmark = myNodeObject.landmark;

		SetupHighTierActivities();

		DetermineIfEventHappeningHere();
	}


	void Start () {
		nodeNum = nodeManager.nodeList.IndexOf(gameObject);

		SetupMyNeighborhood();
		myLocManager = myNeighborhood.GetComponent<MapLocationManager>();

		if (NodeManager.nodeGangs[nodeNum] == null || NodeManager.nodeGangs[nodeNum] == "") {
			NodeManager.nodeGangs[nodeNum] = myLocManager.gangControllingLoc;
		}
		SetMyGangInfluence();
	}


	void OnTriggerEnter(Collider enterCol) {
		if (enterCol.gameObject.CompareTag("Player") && gameObject == mapManager.currentLocation) {
			DetermineIfEventHappeningHere();

			if (isEventHappeningHere) {
				NightHighTierManager.isHighTierActivityHere = true;
				NightHighTierManager.SetHTCrimeRates(ClueMaster.attackType, isEventHappeningHere, ClueMaster.gangInvolvedInEvent);
			}
			else if (myHighTierActivity != null && myHighTierActivity != "" && myHighTierActivity != "Blank") {
				//The Nightly Activity from the NightHighTierManager script becomes equal to "myHighTierActivity"
				NightHighTierManager.isHighTierActivityHere = true;
				NightHighTierManager.SetHTCrimeRates(myHighTierActivity, isEventHappeningHere, gangName);
				//Debug.Log("Location is host to a high-tier activity");
			}
			else {
				//Debug.Log("Location is host to a minor activity");
				NightHighTierManager.isHighTierActivityHere = false;
				NightHighTierManager.isEvent = false;
				//GetComponent<MeshRenderer>().material.color = Color.black;
				NightManager.SetCrimeRates(gangName);
			}
			compDisplay.OpenCompDisplay();
		}
	}


	void OnTriggerStay(Collider other) {
		if (Input.GetKeyDown(KeyCode.E)) {
			compDisplay.OpenCompDisplay();
		}
	}


	//private void OnTriggerExit(Collider exitCol) {
	//	if (exitCol.gameObject.CompareTag("Player")) {
	//		compDisplay.CloseDisplay();
	//	}
	//}


	void SetupHighTierActivities() {
		highTierActivities = new string[9] { "Arson", "Bomb Threat", "Chemical Attack", "Hostage Situation", "Kidnapping", "Mass Shooting", "Robbery",
			"Blank", "Blank"/*, "Blank", "Blank", "Blank"*/};

//TOMAYBEDO If I want to set up location (or landmark)-specific activities, do it here
/*		switch (myLandmark) {
			case "Bridge":
				break;
			case "City Hall":
				break;
			case "First Bank":
				break;
			case "International Corporation HQ":
				break;

			default:
				break;
		}
*/
	}


	void DetermineIfEventHappeningHere() {
		if (ClueMaster.eventOngoing && ClueMaster.eventUncovered) {
			if (ClueMaster.location == myLandmark) {
				GetComponent<MeshRenderer>().material.color = Color.green;
				isEventHappeningHere = true;
				myHighTierActivity = ClueMaster.attackType;
			}
			else {
				isEventHappeningHere = false;
			}
		}
	}


	void SetupMyNeighborhood() {
		foreach (GameObject node in nodeManager.nodeList) {
			SphereCollider nodeCol = node.GetComponent<SphereCollider>();
			if (nodeCol.bounds.Intersects(gameObject.GetComponent<SphereCollider>().bounds)) {
				neighborObjects.Add(node);
			}
		}
		if (neighborObjects.Contains(gameObject)) {
			neighborObjects.Remove(gameObject);
		}

		if (myNeighborhood == null) {
			foreach (GameObject neighborhood in nodeManager.neighborhoods) {
				if (neighborhood.GetComponent<BoxCollider>().bounds.Contains(gameObject.transform.position)) {
					myNeighborhood = neighborhood;
				}
			}
		}
	}


	public void SetMyGangInfluence() {
		gangName = NodeManager.nodeGangs[nodeNum];

		switch (gangName) {
			case "The Jackals":
				gangColor = Color.blue;
				break;
			case "The Clone Army":
				gangColor = Color.white;
				break;
			case "The Ember-kin":
				gangColor = Color.red;
				break;
			default:
				gangColor = Color.gray;
				break;
		}
		GetComponent<MeshRenderer>().material.color = gangColor;
	}

}
