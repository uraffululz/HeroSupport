using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityParticipation : MonoBehaviour {

	//[SerializeField] GameObject hero;
	[SerializeField] GameObject sidekick;

	public GameObject activityText1;
	public GameObject activityText2;
	//	public GameObject activityText3;
	
	public Toggle heroToggle1;
	public Toggle sideToggle1;
	public Toggle heroToggle2;
	public Toggle sideToggle2;
//	public Toggle heroToggle3;
//	public Toggle sideToggle3;
	public Toggle heroToggleNightOff;
	public Toggle sideToggleNightOff;


	public void Start() {
		//hero = GameObject.FindWithTag("Player");
		sidekick = GameObject.FindWithTag("Sidekick");
	}


	public void SetTogglesToDefault() {
		heroToggle1.isOn = false;
		heroToggle2.isOn = false;
//		heroToggle3.isOn = false;
		heroToggleNightOff.isOn = true;

		if (sidekick != null) {
			sideToggle1.gameObject.SetActive(true);
			sideToggle2.gameObject.SetActive(true);
			sideToggleNightOff.gameObject.SetActive(true);

			sideToggle1.isOn = false;
			sideToggle2.isOn = false;
			//sideToggle3.isOn = false;
			sideToggleNightOff.isOn = true;
		}
		else {
			sideToggle1.gameObject.SetActive(false);
			sideToggle2.gameObject.SetActive(false);
			sideToggleNightOff.gameObject.SetActive(false);
		}
	}


	public void CheckParticipation () {
		//Determine which toggles are active, relative to the current characters and activities available
		//Then, if none of the activity toggles are on, turn on the "nightOff" toggles
		if (sidekick == null) {
			if (activityText2.activeInHierarchy) {
				if (!heroToggle1.isOn && !heroToggle2.isOn) {
					heroToggleNightOff.isOn = true;
				}
			}
			else {
				if (!heroToggle1.isOn) {
					heroToggleNightOff.isOn = true;
				}
			}
		}
		else { //If the Sidekick is active in the scene
			if (activityText2.activeInHierarchy) {
				if (!heroToggle1.isOn && !heroToggle2.isOn) {
					heroToggleNightOff.isOn = true;
				}
				if (!sideToggle1.isOn && !sideToggle2.isOn) {
					sideToggleNightOff.isOn = true;
				}
			}
			else {
				if (!heroToggle1.isOn) {
					heroToggleNightOff.isOn = true;
				}
				if (!sideToggle1.isOn) {
					sideToggleNightOff.isOn = true;
				}
			}
		}
	}
}
