using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public static class NightManager {

	public static string activitySceneToLoad;

	public static string gangInvolved;

	public static int crimeRate;

	public static string crimeStars;

	public static string activity;
	static string[] activities = new string[] { "Assault", "Burglary", "Car Theft", "Gang War", "Mugging", "Robbery", "Runaway Train" };

	public static int enemyHPBonus = 0;
	public static int enemyFPBonus = 0;
	public static int enemyStrBonus = 0;
	public static int enemyAgiBonus = 0;
	public static int enemyIntBonus = 0;

	public static int baseFatigueDmg = 0;
	public static int baseStressDmg = 0;

	public static int baseNotoriety = 0;


	public static void SetCrimeRates (string gangInControl) {
		gangInvolved = gangInControl;

		crimeStars = "";
		crimeRate = Random.Range(1, 6);
		for (int i = 1; i <= crimeRate; i++) {
			crimeStars += "*";
		}
	
		activity = activities[Random.Range(0, activities.Length)];
		Debug.Log(activity);

		SetActivityParameters(activity, enemyHPBonus, enemyFPBonus, enemyStrBonus, enemyAgiBonus, enemyIntBonus,
			crimeRate, baseFatigueDmg, baseStressDmg, baseNotoriety);
		
		//Debug.Log(activity1 + " requires: " + reqStr1 + ", " + reqAgi1 + ", " + reqInt1 + ". Will damage Fatigue: " + baseFatigueDmg1 + " Stress: " + baseStressDmg1);
	}


	public static void SetActivityParameters(string thisActivity, int HPBonus, int FPBonus, int strBonus, int agiBonus, int intBonus, 
		int difficulty, int fatigue, int stress, int notoriety) {
		//TODO I don't think I need to pass in all these variables ^, since they don't immediately adjust when declaring the local-variable-versions
		//The locals could probably just be set at the start of this method (here), and later assigned (like they are now), without much difficulty
		//For now, I just want to keep going, but it's something to keep in mind
		
		switch (thisActivity) {
	/*		case "Assault":
				break;
			case "Burglary":
				break;
			case "Mugging":
				break;
			case "Robbery":
				break;
			case "Runaway Train":
				break;
*/
			default:
				activitySceneToLoad = "SampleActivityArena";
				HPBonus = 10; FPBonus = 10; //Later, formulate these based on the activity's difficulty
				//Not really sure if enemies should have these stats: strBonus = 1; agiBonus = 1; intBonus = 1;
				fatigue = difficulty; stress = difficulty;
				notoriety = 10 * difficulty;
				break;
		}


/*		if (thisActivity == "Arson") {
			reqStr = 1; reqAgi = 2; reqInt = 3; fatigue = 5 * difficulty; stress = 2 * difficulty;
		}
		if (thisActivity == "Assault") {
			reqStr = 3; reqAgi = 2; reqInt = 1; fatigue = 2 * difficulty; stress = 1 * difficulty;
		}
		if (thisActivity == "Bank Robbery") {
			reqStr = 1; reqAgi = 1; reqInt = 3; fatigue = 3 * difficulty; stress = 3 * difficulty;
		}
		if (thisActivity == "Burglary") {
			reqStr = 1; reqAgi = 2; reqInt = 3; fatigue = 1 * difficulty; stress = 3 * difficulty;
		}
		if (thisActivity == "Kidnapping") {
			reqStr = 1; reqAgi = 1; reqInt = 3; fatigue = 1 * difficulty; stress = 10 * difficulty;
		}
		if (thisActivity == "Mugging") {
			reqStr = 2; reqAgi = 2; reqInt = 1; fatigue =  2 * difficulty; stress = 2 * difficulty;
		}
		if (thisActivity == "Robbery") {
			reqStr = 2; reqAgi = 2; reqInt = 1; fatigue = 3 * difficulty; stress = 1 * difficulty;
		}
		if (thisActivity == "Runaway Train") {
			//Stuff like this might be considered "mass casualties", which may severely damage the character's stress
			reqStr = 1; reqAgi = 1; reqInt = 3; fatigue = 5 * difficulty; stress = 5 * difficulty;
		}

		reqStr += difficulty;
		reqAgi += difficulty;
		reqInt += difficulty;
*/

//TOMAYBEDO The required stats can be further randomized by Random.Range-ing them between 50% and 150% (or whatever range works)

//TODO Actually, if getting rid of the activity2 and activity3 variables, this \/ may be obsolete
//TODO See if there is a more efficient way to return/apply these values to their proper variables
//Maybe by altering the return-type of the function or whatever. This works for now.
			enemyHPBonus = HPBonus;
			enemyFPBonus = FPBonus;
			enemyStrBonus = strBonus;
			enemyAgiBonus = agiBonus;
			enemyIntBonus = intBonus;
			baseFatigueDmg = fatigue;
			baseStressDmg = stress;

		//Debug.Log("Activity parameters set");
	}


	static void ResetValues() {
		enemyHPBonus = 0;
		enemyFPBonus = 0;

		enemyStrBonus = 0;
		enemyAgiBonus = 0;
		enemyIntBonus = 0;

		baseFatigueDmg = 0;
		baseStressDmg = 0;

		baseNotoriety = 0;
	}
}
