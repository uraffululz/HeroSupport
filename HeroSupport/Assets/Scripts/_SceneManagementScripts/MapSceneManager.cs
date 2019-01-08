using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSceneManager : MonoBehaviour {
	[SerializeField] GameObject dayNightLight;
	[SerializeField] GameObject daylightUI;

	Canvas canvas;
	GraphicRaycaster UIRay;
	PointerEventData UIRayEvent;
	EventSystem eventSys;

	[SerializeField] Image policeAlertBG;
	[SerializeField] Text policeAlertText;
	Color bgOnColor = new Color(1, 1, 1, .2f);
	Color bgOffColor = new Color(1, 1, 1, 0f);

	[SerializeField] List<GameObject> mapNodes;
	public static GameObject mapLoc;
	static int mapLocNum;
	public static GameObject currentLocation;

	GameObject player;
	GameObject sidekick;

	enum mapState {Idle, Grappling, Moving};
	[SerializeField] mapState myMapState;

	public static bool highTierActSet = false;
	bool nightOver = false;


	void Awake () {
		dayNightLight = GameObject.Find("Day-Night Light");
		canvas = GameObject.FindObjectOfType<Canvas>();
		eventSys = GameManager.FindObjectOfType<EventSystem>().GetComponent<EventSystem>();

		mapNodes = new List<GameObject>(GameObject.FindGameObjectsWithTag("MapNode"));

		player = GameObject.FindWithTag("Player");
		sidekick = GameObject.FindWithTag("Sidekick");

		if (player != null) {
			//player.transform.position = Vector3.zero;
			player.GetComponent<HeroActivity>().enabled = false;
			player.GetComponent<HeroAttack>().enabled = false;
			player.GetComponent<PlayerInteracting>().enabled = false;
		}

		if (sidekick != null) {
			sidekick.transform.position = Vector3.back;
		}

		myMapState = mapState.Idle;

		policeAlertBG.color = bgOffColor;
		policeAlertText.text = "";

		//TODO This is probably unnecessary at this point
		//Set the first nightly activity
		//NightManager.SetCrimeRates(ClueMaster.gangs[Random.Range(0, ClueMaster.gangs.Length)]);
	}


	void Update () {
		ChooseLocation();
		MoveToLocation();
		DayNightFunctions();
	}


	void ChooseLocation () {
		if (Input.GetMouseButtonDown(0)) {
			UIRay = canvas.GetComponent<GraphicRaycaster>();
			UIRayEvent = new PointerEventData(eventSys);
			UIRayEvent.position = Input.mousePosition;
			List<RaycastResult> UIElementsHit = new List<RaycastResult>();
			UIRay.Raycast(UIRayEvent, UIElementsHit);

			Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit camRayHit;

			if (Physics.Raycast(camRay, out camRayHit)) {
				if (UIElementsHit.Count == 0) {
					if (camRayHit.collider.CompareTag("MapNode")) {
						currentLocation = camRayHit.collider.gameObject;
					}
				}
				else {
					//foreach (RaycastResult result in UIElementsHit) {
					//	Debug.Log("UIRay hit " + result.gameObject.name);
					//}
				}
			}
		}
	}


	void MoveToLocation () {
		if (currentLocation != null && player.transform.position != currentLocation.transform.position) {
			myMapState = mapState.Moving;
			player.transform.position = Vector3.Lerp(player.transform.position, currentLocation.transform.position, .1f);

			float distPlayerToLoc = Vector3.Distance(player.transform.position, currentLocation.transform.position);
			if (distPlayerToLoc <= .1f) {
				myMapState = mapState.Idle;
			}
		}
	}


	void DayNightFunctions () {
		if (dayNightLight != null) {
			DayNightCycle DNCycle = dayNightLight.GetComponent<DayNightCycle>();

			if (DNCycle.dayTime && !nightOver) {
//TODO Have this trigger the "DaylightCome" event on the DayNightCycle script (Not implemented yet), and move all of these related processes,
//including the "OpenDaylightReturnUI" method, into that event trigger method somewhere below on this script
				highTierActSet = false;
				NightHighTierManager.ResetValues();
				if (currentLocation != null) {
					NightManager.SetCrimeRates(currentLocation.GetComponent<NodeDetails>().gangName);
				}
				foreach (GameObject node in mapNodes) {
					//NodeDetails.myHighTierActivity = "";
					node.GetComponent<MeshRenderer>().material.color = node.GetComponent<NodeDetails>().gangColor;
				}
				policeAlertBG.color = bgOffColor;
				policeAlertText.text = "";
				//StartCoroutine("OpenDaylightReturnUI");
				nightOver = true;
			}
			else if (DNCycle.transition > .01f && !DNCycle.dayTime) {
				//Once per night (for now), determine if there are any high-tier activities happening in the city

				if (!highTierActSet) {
					mapLocNum = Random.Range(0, mapNodes.Count);
				}

				mapLoc = mapNodes[mapLocNum];
				NodeDetails nodeDeets = mapLoc.GetComponent<NodeDetails>();

				if (!highTierActSet && !nodeDeets.isEventHappeningHere) {
					NodeDetails.myHighTierActivity = nodeDeets.highTierActivities[Random.Range(0, nodeDeets.highTierActivities.Length)];
				}

				if (NodeDetails.myHighTierActivity != null && NodeDetails.myHighTierActivity != "" && NodeDetails.myHighTierActivity != "Blank") {
					mapLoc.GetComponent<MeshRenderer>().material.color = Color.yellow;

					policeAlertBG.color = bgOnColor;
					policeAlertText.color = Color.yellow;
					policeAlertText.text = (StatsPlayer.myName + "! We need your assistance! There is a " +
						NodeDetails.myHighTierActivity + " happening at the " + nodeDeets.myLandmark);

					//Debug.Log("High-Tier Activity happening at " + mapLoc.name + ": " + nodeDeets.myHighTierActivity);
				}
				else {
					policeAlertBG.color = bgOffColor;
					policeAlertText.text = "";
					Debug.Log("No high-tier activity happening tonight.");
				}

				highTierActSet = true;
				nightOver = false;
			}
		}
	}


	IEnumerator OpenDaylightReturnUI () {
		daylightUI.GetComponent<Animator>().SetBool("compActivated", true);

		yield return new WaitForSeconds(1f);
		//Debug.Log("Daylight has come, and you must return home.");
		Time.timeScale = 0f;
	}


	public void ReturnToHideout () {
		Time.timeScale = 1f;
		//highTierActSet = false;
		//NightHighTierManager.ResetValues();
		SceneManager.LoadScene("HideoutScene");
	}


	public static void ResetHighTier () {
		mapLoc = null;
		currentLocation = null;
		highTierActSet = false;

		NodeDetails.myHighTierActivity = null;
	}
}
