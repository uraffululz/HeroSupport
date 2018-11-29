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

	[SerializeField] List<GameObject> mapLocations;
	public GameObject mapLoc;
	public GameObject currentLocation;

	GameObject player;
	GameObject sidekick;

	enum mapState {Idle, Grappling, Moving};
	[SerializeField] mapState myMapState;


	void Awake () {
		dayNightLight = GameObject.Find("Day-Night Light");
		canvas = GameObject.FindObjectOfType<Canvas>();
		eventSys = GameManager.FindObjectOfType<EventSystem>().GetComponent<EventSystem>();

		mapLocations = new List<GameObject>(GameObject.FindGameObjectsWithTag("MapLocation"));

		player = GameObject.FindWithTag("Player");
		sidekick = GameObject.FindWithTag("Sidekick");

		if (player != null) {
			player.transform.position = Vector3.zero;
			player.GetComponent<HeroActivity>().enabled = false;
			player.GetComponent<HeroAttack>().enabled = false;
			player.GetComponent<PlayerInteracting>().enabled = false;
		}

		if (sidekick != null) {
			sidekick.transform.position = Vector3.back;
		}

		myMapState = mapState.Idle;

		//Set the first nightly activity
		NightManager.SetCrimeRates();
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
					if (camRayHit.collider.CompareTag("MapLocation")) {
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
			if (DNCycle.dayTime == true) {
				StartCoroutine("OpenDaylightReturnUI");
			}
			else if (DNCycle.transition > .1f && DNCycle.transition < .101f) {
				mapLoc = mapLocations[Random.Range(0, mapLocations.Count)];
				MapLocationManager mapLocManager = mapLoc.GetComponent<MapLocationManager>();
				mapLocManager.myHighTierActivity = mapLocManager.highTierActivities[Random.Range(0, mapLocManager.highTierActivities.Length)];
				if (mapLocManager.myHighTierActivity != null && mapLocManager.myHighTierActivity != "Blank") {
					mapLoc.GetComponent<MeshRenderer>().material.color = Color.yellow;
					Debug.Log("High-Tier Activity happening at " + mapLoc.name + ": " + mapLocManager.myHighTierActivity);
				}
				else {
					Debug.Log("No high-tier activity happening tonight.");
				}
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
		SceneManager.LoadScene("HideoutScene");
	}
}
