using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

	Light cycleLight;

//	Color nightColor;

	float transition = 0f;
	float transitionSpeed = .1f;

	float startYRot = 180f;
	float locYRot = 0f;

	bool sunRising = true;
	public bool dayTime = false;

	void Start () {
		cycleLight = GetComponent<Light>();
//		nightColor = new Color(.4f, .4f, 1f);
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
		if (transition >= .5f) {
			dayTime = true;
		}
		else {
			dayTime = false;
		}

		//If you want to rotate the local y-axis in a half-circle during the day, and then cut back across and repeat for the night cycle,
		//just set startYRot to 0 if daytime is false
		//NOTE: It's a bit jarring/distracting/breaks immersion, but maybe I can find a way to smooth it out. Maybe use 2 lights?

		float rotLerp = Mathf.Lerp(0f, 1f, transition);
		float rotLerpInv = Mathf.InverseLerp(1f, 0f, transition);

		if (sunRising && !dayTime) {
			locYRot = startYRot + (rotLerp * 180f);
		}
		else if (sunRising && dayTime) {
			locYRot = startYRot + (rotLerp * 180f);
		}
		else if (!sunRising && dayTime) {
			locYRot = startYRot + ((rotLerpInv +1) * 180f);
		}
		else if (!sunRising && ! dayTime) {
			locYRot = startYRot + ((rotLerpInv + 1) * 180f);
		}

		transform.localEulerAngles = new Vector3(45f, locYRot, 0f);
	}
}
