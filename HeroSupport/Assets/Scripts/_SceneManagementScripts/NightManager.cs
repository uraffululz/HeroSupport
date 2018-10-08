using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public static class NightManager {

	public static int crimeRate1;
	//public static int crimeRate2;
	//public static int crimeRate3;

	public static string crimeStars1;
	//public static string crimeStars2;
	//public static string crimeStars3;

	public static string activity1;
	//public static string activity2;
	//public static string activity3;
	static string[] activities = new string[] { "Arson", "Assault", "Bank Robbery", "Burglary", "Kidnapping", "Mugging", "Robbery", "Runaway Train" };

	public static int reqStr1 = 1;
	public static int reqAgi1 = 1;
	public static int reqInt1 = 1;

	//public static int reqStr2 = 1;
	//public static int reqAgi2 = 1;
	//public static int reqInt2 = 1;

	//public static int reqStr3 = 1;
	//public static int reqAgi3 = 1;
	//public static int reqInt3 = 1;

	public static int baseFatigueDmg1 = 0;
	//public static int baseFatigueDmg2 = 0;
	//public static int baseFatigueDmg3 = 0;

	public static int baseStressDmg1 = 0;
	//public static int baseStressDmg2 = 0;
	//public static int baseStressDmg3 = 0;


	public static void SetCrimeRates () {
		crimeStars1 = "";
		//crimeStars2 = "";
		//crimeStars3 = "";

		crimeRate1 = Random.Range(1, 6);
		//crimeRate2 = Random.Range(1, 6);
		//crimeRate3 = Random.Range(1, 6);

		for (int i = 1; i <= crimeRate1; i++) {
			crimeStars1 += "*";
		}
		/*
		for (int i = 1; i <= crimeRate2; i++) {
			crimeStars2 += "*";
		}
		for (int i = 1; i <= crimeRate3; i++) {
			crimeStars3 += "*";
		}
		*/
		activity1 = activities[Random.Range(0, activities.Length)];
		/*activity2 = activities[Random.Range(0, activities.Length)];
		while (activity2 == activity1) {
			//Debug.Log("Activity2 is duplicate. Re-rolling");
			activity2 = activities[Random.Range(0, activities.Length)];
		}
		activity3 = activities[Random.Range(0, activities.Length)];
		while (activity3 == activity1 || activity3 == activity2) {
			//Debug.Log("Activity3 is duplicate. Re-rolling");
			activity3 = activities[Random.Range(0, activities.Length)];
		}
		*/

		SetActivityRequirements(activity1, reqStr1, reqAgi1, reqInt1, crimeRate1, baseFatigueDmg1, baseStressDmg1);
		//SetActivityRequirements(activity2, reqStr2, reqAgi2, reqInt2, crimeRate2, baseFatigueDmg2, baseStressDmg2);
		//SetActivityRequirements(activity3, reqStr3, reqAgi3, reqInt3, crimeRate3, baseFatigueDmg3, baseStressDmg3);

		//Debug.Log(activity1 + " requires: " + reqStr1 + ", " + reqAgi1 + ", " + reqInt1 + ". Will damage Fatigue: " + baseFatigueDmg1 + " Stress: " + baseStressDmg1);
		//Debug.Log(activity2 + " requires: " + reqStr2 + ", " + reqAgi2 + ", " + reqInt2 + ". Will damage Fatigue: " + baseFatigueDmg2 + " Stress: " + baseStressDmg2);
		//Debug.Log(activity3 + " requires: " + reqStr3 + ", " + reqAgi3 + ", " + reqInt3 + ". Will damage Fatigue: " + baseFatigueDmg3 + " Stress: " + baseStressDmg3);
	}


	public static void SetActivityRequirements (string thisActivity, int reqStr, int reqAgi, int reqInt, int difficulty, int fatigue, int stress) {
//TODO I don't think I need to pass in all these variables ^, since they don't immediately adjust when declaring the local-variable-versions
		//The locals could probably just be set at the start of this method (here), and later assigned (like they are now), without much difficulty
		//For now, I just want to keep going, but it's something to keep in mind

		//Use to adjust the stress of the activity, according to how many people die through action/inaction
		int casualties = 0;

		//Declaring values for the activity's requirements, and subsequent "base damage" to fatigue and stress
//TODO If the activity requires more STRENGTH (OR AGILITY?), it should cause more FATIGUE DAMAGE
	//If it requires more INTELLECT, it should cause more STRESS
		if (thisActivity == "Arson") {
			reqStr = 1; reqAgi = 2; reqInt = 3; fatigue = 5 * difficulty; stress = 2 * difficulty; casualties = 20 * difficulty;
		}
		if (thisActivity == "Assault") {
			reqStr = 3; reqAgi = 2; reqInt = 1; fatigue = 2 * difficulty; stress = 1 * difficulty; casualties = (1 * difficulty) / 2;
		}
		if (thisActivity == "Bank Robbery") {
			reqStr = 1; reqAgi = 1; reqInt = 3; fatigue = 3 * difficulty; stress = 3 * difficulty; casualties = 2 * difficulty;
		}
		if (thisActivity == "Burglary") {
			reqStr = 1; reqAgi = 2; reqInt = 3; fatigue = 1 * difficulty; stress = 3 * difficulty; casualties = (1 * difficulty) / 2;
		}
		if (thisActivity == "Kidnapping") {
			reqStr = 1; reqAgi = 1; reqInt = 3; fatigue = 1 * difficulty; stress = 10 * difficulty; casualties = 1;
		}
		if (thisActivity == "Mugging") {
			reqStr = 2; reqAgi = 2; reqInt = 1; fatigue =  2 * difficulty; stress = 2 * difficulty; casualties = 1 * difficulty;
		}
		if (thisActivity == "Robbery") {
			reqStr = 2; reqAgi = 2; reqInt = 1; fatigue = 3 * difficulty; stress = 1 * difficulty; casualties = 1 * difficulty;
		}
		if (thisActivity == "Runaway Train") {
			//Stuff like this might be considered "mass casualties", which may severely damage the character's stress
			reqStr = 1; reqAgi = 1; reqInt = 3; fatigue = 5 * difficulty; stress = 5 * difficulty; casualties = 100 * difficulty;
		}

		reqStr += difficulty;
		reqAgi += difficulty;
		reqInt += difficulty;

//TOMAYBEDO The required stats can be further randomized by Random.Range-ing them between 50% and 150% (or whatever range works)

//TODO Actually, if getting rid of the activity2 and activity3 variables, this \/ may be obsolete
//TODO See if there is a more efficient way to return/apply these values to their proper variables
//Maybe by altering the return-type of the function or whatever. This works for now.
		if (thisActivity == activity1) {
			reqStr1 = reqStr;
			reqAgi1 = reqAgi;
			reqInt1 = reqInt;
			baseFatigueDmg1 = fatigue;
			baseStressDmg1 = stress;
		}
		/*else if (thisActivity == activity2) {
			reqStr2 = reqStr;
			reqAgi2 = reqAgi;
			reqInt2 = reqInt;
			baseFatigueDmg2 = fatigue;
			baseStressDmg2 = stress;
		}
		else if (thisActivity == activity3) {
			reqStr3 = reqStr;
			reqAgi3 = reqAgi;
			reqInt3 = reqInt;
			baseFatigueDmg3 = fatigue;
			baseStressDmg3 = stress;
		}
		*/
	}
}
