using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityScript : MonoBehaviour {

	public bool isParticipating1 = false;
	public bool isParticipating2 = false;
	public bool isParticipating3 = false;


	public void ParticipatingInActivity1(bool doActivity1) {
		isParticipating1 = doActivity1;
		Debug.Log(transform.name + " is participating in activity 1? " + isParticipating1);
	}

	public void ParticipatingInActivity2(bool doActivity2) {
		isParticipating2 = doActivity2;
		Debug.Log(transform.name + " is participating in activity 2? " + isParticipating2);
	}

	public void ParticipatingInActivity3(bool doActivity3) {
		isParticipating3 = doActivity3;
		Debug.Log(transform.name + " is participating in activity 3? " + isParticipating3);
	}
}
