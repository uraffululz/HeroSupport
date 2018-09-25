using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class NightManager {

	public static int crimeRate1;
	public static int crimeRate2;
	public static int crimeRate3;

	public static string crimeStars1;
	public static string crimeStars2;
	public static string crimeStars3;


	public static string activity1;
	public static string activity2;
	public static string activity3;
	static string[] activities = new string[] { "Arson", "Assault", "Bank Robbery", "Burglary", "Kidnapping", "Mugging", "Robbery", "Runaway Train" };


	//Is this STILL updating every frame? (use Debug.Log to determine). If so, that's a lot of unnecessary calculation
	public static void SetCrimeRates () {
		crimeStars1 = "";
		crimeStars2 = "";
		crimeStars3 = "";

		crimeRate1 = Random.Range(1, 6);
		crimeRate2 = Random.Range(1, 6);
		crimeRate3 = Random.Range(1, 6);
/*
		Debug.Log(crimeRate1);
		Debug.Log(crimeRate2);
		Debug.Log(crimeRate3);
*/
		for (int i = 1; i <= crimeRate1; i++) {
			crimeStars1 += "*";
		}
		for (int i = 1; i <= crimeRate2; i++) {
			crimeStars2 += "*";
		}
		for (int i = 1; i <= crimeRate3; i++) {
			crimeStars3 += "*";
		}

		activity1 = activities[Random.Range(0, activities.Length)];
		activity2 = activities[Random.Range(0, activities.Length)];
		while (activity2 == activity1) {
			//Debug.Log("Activity2 is duplicate. Re-rolling");
			activity2 = activities[Random.Range(0, activities.Length)];
		}
		activity3 = activities[Random.Range(0, activities.Length)];
		while (activity3 == activity1 || activity3 == activity2) {
			//Debug.Log("Activity3 is duplicate. Re-rolling");
			activity3 = activities[Random.Range(0, activities.Length)];
		}
	}

}
