using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHero : MonoBehaviour {

	CharacterStats charStats;

	DayNightCycle lightCycle;

	public enum activityStates{idle, busy, patrolling};
	public activityStates myActiveState;


	void Start () {
		lightCycle = GameObject.Find("Day-Night Light").GetComponent<DayNightCycle>();
	}


	void Update () {
		if (lightCycle.dayTime) {
			myActiveState = activityStates.idle;
		}
		else {
			myActiveState = activityStates.patrolling;
		}
	}
}
