using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCHero : MonoBehaviour {
	StatsPlayer charStats;

	DayNightCycle lightCycle;

	public enum activityStates{Idle, Busy, Patrolling, Crimefighting, Fatigued, Stressed};
	public activityStates myActiveState;


	void Start () {
		//lightCycle = GameObject.Find("Day-Night Light").GetComponent<DayNightCycle>();

		transform.position = Vector3.right;
	}


	void Update () {
/*		if (lightCycle.dayTime) {
			myActiveState = activityStates.Idle;
		}
		else {
			myActiveState = activityStates.Patrolling;
		}
*/
	}

}
