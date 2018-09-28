using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public static int reqStr1 = 1;
	public static int reqAgi1 = 1;
	public static int reqInt1 = 1;

	public static int reqStr2 = 1;
	public static int reqAgi2 = 1;
	public static int reqInt2 = 1;

	public static int reqStr3 = 1;
	public static int reqAgi3 = 1;
	public static int reqInt3 = 1;


	//Is this STILL updating every frame? (use Debug.Log to determine). If so, that's a lot of unnecessary calculation
	public static void SetCrimeRates () {
		crimeStars1 = "";
		crimeStars2 = "";
		crimeStars3 = "";

		crimeRate1 = Random.Range(1, 6);
		crimeRate2 = Random.Range(1, 6);
		crimeRate3 = Random.Range(1, 6);

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

		SetActivityRequirements(activity1, reqStr1, reqAgi1, reqInt1, crimeRate1);
		SetActivityRequirements(activity2, reqStr2, reqAgi2, reqInt2, crimeRate2);
		SetActivityRequirements(activity3, reqStr3, reqAgi3, reqInt3, crimeRate3);

		Debug.Log(activity1 + " requires: " + reqStr1 + ", " + reqAgi1 + ", " + reqInt1);
		Debug.Log(activity2 + " requires: " + reqStr2 + ", " + reqAgi2 + ", " + reqInt2);
		Debug.Log(activity3 + " requires: " + reqStr3 + ", " + reqAgi3 + ", " + reqInt3);

	}


	public static void SetActivityRequirements (string thisActivity, int reqStr, int reqAgi, int reqInt, int difficulty) {
		if (thisActivity == "Arson") {
			reqStr = 1; reqAgi = 2; reqInt = 3;
		}
		if (thisActivity == "Assault") {
			reqStr = 3; reqAgi = 2; reqInt = 1;
		}
		if (thisActivity == "Bank Robbery") {
			reqStr = 1; reqAgi = 1; reqInt = 3;
		}
		if (thisActivity == "Burglary") {
			reqStr = 1; reqAgi = 2; reqInt = 3;
		}
		if (thisActivity == "Kidnapping") {
			reqStr = 1; reqAgi = 1; reqInt = 3;
		}
		if (thisActivity == "Mugging") {
			reqStr = 2; reqAgi = 2; reqInt = 1;
		}
		if (thisActivity == "Robbery") {
			reqStr = 2; reqAgi = 2; reqInt = 1;
		}
		if (thisActivity == "Runaway Train") {
			reqStr = 1; reqAgi = 1; reqInt = 3;
		}

		reqStr += difficulty;
		reqAgi += difficulty;
		reqInt += difficulty;

//TOMAYBEDO The required stats can be further randomized by Random.Range-ing them between 50% and 150% (or whatever range works)


//TODO See if there is a more efficient way to return/apply these values to their proper variables
//Maybe by altering the return-type of the function or whatever. This works for now.
		if (thisActivity == activity1) {
			reqStr1 = reqStr;
			reqAgi1 = reqAgi;
			reqInt1 = reqInt;
		}
		else if (thisActivity == activity2) {
			reqStr2 = reqStr;
			reqAgi2 = reqAgi;
			reqInt2 = reqInt;
		}
		else if (thisActivity == activity3) {
			reqStr3 = reqStr;
			reqAgi3 = reqAgi;
			reqInt3 = reqInt;
		}

		//Debug.Log(thisActivity + " requires: " + reqStr + ", " + reqAgi + ", " + reqInt);
	}
}
