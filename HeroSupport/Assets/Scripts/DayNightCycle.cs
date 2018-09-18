using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

	Light cycleLight;

	float transition = 0f;
	float transitionSpeed = .1f;

	bool sunRising = true;
	public bool dayTime = false;

	void Start () {
		cycleLight = GetComponent<Light>();
	}


	void Update () {
		//Increase "transition" over time according to "transitionSpeed", from a value of 0 to 1, and back again
		transition += (sunRising) ? transitionSpeed * Time.deltaTime : -transitionSpeed * Time.deltaTime;

		//When "transition" reaches its low-point (at a value of 0 (midnight)), the "sun" begins to "rise"
		//When "transition" reaches its peak (at a value of 1 (noon), it begins to "set"
		if (transition < 0f | transition > 1f) {
			sunRising = !sunRising;
		}

		//Transitions light intensity and color between dark blue (night) and bright white (day), determined by value of "transition"
		cycleLight.intensity = transition;
		cycleLight.color = Color.Lerp(Color.blue, Color.white, transition);

		//"dayTime" bool becomes true while light is mostly white, and false while it is mostly blue
		if (transition >= .4f) {
			dayTime = true;
		}
		else {
			dayTime = false;
		}
	}
}
