using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityParticipation : MonoBehaviour {

	[SerializeField] Toggle toggle1;
	[SerializeField] Toggle toggle2;
	[SerializeField] Toggle toggle3;
	[SerializeField] Toggle toggleNightOff;


	public void ParticipatingInActivity1 (bool isParticipating1) {
		if (isParticipating1) {
			TakingNightOff(false);
			toggle1.isOn = true;
		} else {
			toggle1.isOn = false;
		}
		Debug.Log(transform.name + " is participating in activity 1? " + isParticipating1);
	}

	public void ParticipatingInActivity2 (bool isParticipating2) {
		if (isParticipating2) {
			TakingNightOff(false);
			toggle2.isOn = true;
		} else {
			toggle2.isOn = false;
		}
		Debug.Log(transform.name + " is participating in activity 2? " + isParticipating2);
	}

	public void ParticipatingInActivity3 (bool isParticipating3) {
		if (isParticipating3) {
			TakingNightOff(false);
			toggle3.isOn = true;
		} else {
			toggle3.isOn = false;
		}
		Debug.Log(transform.name + " is participating in activity 3? " + isParticipating3);
	}

	public void TakingNightOff (bool isTakingNightOff) {
		if (isTakingNightOff) {
			toggleNightOff.isOn = true;

			ParticipatingInActivity1(false);
			ParticipatingInActivity2(false);
			ParticipatingInActivity3(false);
		} else {
			toggleNightOff.isOn = false;
		}
	}

	public void SetTogglesToDefault() {
		toggle1.isOn = false;
		toggle2.isOn = false;
		toggle3.isOn = false;
		toggleNightOff.isOn = true;
	}
}
