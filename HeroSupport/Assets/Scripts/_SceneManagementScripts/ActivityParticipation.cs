using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityParticipation : MonoBehaviour {

	public Toggle toggle1;
	public Toggle toggle2;
	public Toggle toggle3;
	public Toggle toggleNightOff;


	public void SetTogglesToDefault() {
		toggle1.isOn = false;
		toggle2.isOn = false;
		toggle3.isOn = false;
		toggleNightOff.isOn = true;
	}

	public void ParticipatingInActivity1 (bool isParticipating1) {
		if (isParticipating1) {
			TakingNightOff(false);
			toggle1.isOn = true;
		} else {
			toggle1.isOn = false;
			if (!toggle2.isOn && !toggle3.isOn) {
				TakingNightOff(true);
			}
		}
		//Debug.Log(transform.name + " is participating in activity 1? " + isParticipating1);
	}

	public void ParticipatingInActivity2 (bool isParticipating2) {
		if (isParticipating2) {
			TakingNightOff(false);
			toggle2.isOn = true;
		} else {
			toggle2.isOn = false;
			if (!toggle1.isOn && !toggle3.isOn) {
				TakingNightOff(true);
			}
		}
		//Debug.Log(transform.name + " is participating in activity 2? " + isParticipating2);
	}

	public void ParticipatingInActivity3 (bool isParticipating3) {
		if (isParticipating3) {
			TakingNightOff(false);
			toggle3.isOn = true;
		} else {
			toggle3.isOn = false;
			if (!toggle1.isOn && !toggle2.isOn) {
				TakingNightOff(true);
			}
		}
		//Debug.Log(transform.name + " is participating in activity 3? " + isParticipating3);
	}

	public void TakingNightOff (bool isTakingNightOff) {
		if (isTakingNightOff) {
			toggleNightOff.isOn = true;
			//TODO Do not allow the player to toggle this box if it is on

			if (toggle1.isOn) {
				ParticipatingInActivity1(false);
			}
			if (toggle2.isOn) {
				ParticipatingInActivity2(false);
			}
			if (toggle3.isOn) {
				ParticipatingInActivity3(false);
			}
		} else {
			toggleNightOff.isOn = false;
			//If all other toggles are OFF, this one remains ON
			if (!toggle1.isOn && !toggle2.isOn && !toggle3.isOn) { 
				toggleNightOff.isOn = true;
			}
		}
	}
}
