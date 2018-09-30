using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHero : MonoBehaviour {

	CharacterStats charStats;

	DayNightCycle lightCycle;

	public enum activityStates{Idle, Busy, Patrolling};
	public activityStates myActiveState;


	void Start () {
		lightCycle = GameObject.Find("Day-Night Light").GetComponent<DayNightCycle>();
	}


	void Update () {
		if (lightCycle.dayTime) {
			myActiveState = activityStates.Idle;
		}
		else {
			myActiveState = activityStates.Patrolling;
		}
	}
}
