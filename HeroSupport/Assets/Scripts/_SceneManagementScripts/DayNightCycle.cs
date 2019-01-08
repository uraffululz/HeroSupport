using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {
	static DayNightCycle instance;

	Light cycleLight;
	//[SerializeField] GameObject hero;
	//[SerializeField] GameObject sidekick;

//	Color nightColor;

	public float transition = 0f;
	float transitionSpeed = .05f;

	float startYRot = 180f;
	float locYRot = 0f;

	public bool sunRising = true;
	public bool dayTime = false;
	public bool isDawn = false;
	public bool isDusk = false;


	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		}
		else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}


	void Start () {
		//hero = GameObject.FindGameObjectWithTag("Player");
		//sidekick = GameObject.FindGameObjectWithTag("Sidekick");

		cycleLight = GetComponent<Light>();
		//		nightColor = new Color(.4f, .4f, 1f);

//TODO When loading the scene, set the game time/"sun's transition" to just before Dawn
		//transition = .4f;
	}


	void Update () {
		//Increase "transition" over time according to "transitionSpeed", from a value of 0 to 1, and back again
		transition += (sunRising) ? transitionSpeed * Time.deltaTime : -transitionSpeed * Time.deltaTime;

		//When "transition" reaches its low-point (at a value of 0 (midnight)), the "sun" begins to "rise"
		//When "transition" reaches its peak (at a value of 1 (noon), it begins to "set"
		if (transition < 0f || transition > 1f) {
			sunRising = !sunRising;
		}

		//Transitions light intensity and color between dark blue (night) and bright white (day), determined by value of "transition"
		cycleLight.intensity = transition;
		cycleLight.color = Color.Lerp(Color.blue, Color.white, transition);

		//"dayTime" bool becomes true while light is mostly white, and false while it is mostly blue
		if (transition >= .5f) {
			dayTime = true;
			//During the frame in which it actually BECOMES Day
			if (!isDawn) {
//TODO Here is where I should trigger a "DaylightCome" event for all scripts listening (MapSceneManager, NodeManager)
//It should also disable/reset any of the still-active high-tier activities going on in the city
				isDawn = true;
				isDusk = false;
			}
		}
		else {
			dayTime = false;
			//During the frame in which it actually BECOMES night
			if (!isDusk) {
				isDusk = true;
				isDawn = false;
			}
		}

		//If you want to rotate the local y-axis in a half-circle during the day, and then cut back across and repeat for the night cycle,
		//just set startYRot to 0 if daytime is false
		//NOTE: It's a bit jarring/distracting/breaks immersion, but maybe I can find a way to smooth it out. Maybe use 2 lights?

		//Using Mathf.InverseLerp because otherwise, during the "night", the light reverses direction and rotates back to the starting point
		//This way, it continues in a full circle while "sunRising" is false
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
