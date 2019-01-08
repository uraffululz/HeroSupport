using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class NightHighTierManager {
	public static bool isHighTierActivityHere = false;
	public static bool isEvent = false;

	public static string locationName;
	public static string activitySceneToLoad;

	public static string gangInvolved;

	public static int crimeRate;
	static bool HTCrimeRateSet = false;
	static int eventCrimeRate;
	static bool eventCrimeRateSet = false;

	public static string crimeStars;

	public static string activity;

	public static int enemyHPBonus = 0;
	public static int enemyFPBonus = 0;
	public static int enemyStrBonus = 0;
	public static int enemyAgiBonus = 0;
	public static int enemyIntBonus = 0;

	public static int baseFatigueDmg = 0;
	public static int baseStressDmg = 0;

	public static int baseNotoriety = 0;


	public static void SetHTCrimeRates(string myHTActivity, bool isAnEvent, string gangInControl, string HTActivityLocation) {
		isEvent = isAnEvent;


		if (isEvent) {
			if (!eventCrimeRateSet) {
				eventCrimeRate = Random.Range(1, 6);
				activity = myHTActivity;
				locationName = HTActivityLocation;
				gangInvolved = gangInControl;

				eventCrimeRateSet = true;
			}
			crimeStars = "";
			for (int i = 1; i <= eventCrimeRate; i++) {
				crimeStars += "*";
			}
		}
		else {
			if (isHighTierActivityHere) {
				if (!HTCrimeRateSet) {
					crimeRate = Random.Range(1, 6);
					activity = myHTActivity;
					locationName = HTActivityLocation;
					gangInvolved = gangInControl;

					HTCrimeRateSet = true;
				}
				crimeStars = "";
				for (int i = 1; i <= crimeRate; i++) {
					crimeStars += "*";
				}
			}
			else {
				//Abort, because this script shouldn't have been called in the first place if there is no HTActivity or Event here
				Debug.Log("This script shouldn't have been called. Find out why.");
				return;
			}
		}
		


		SetHTActivityParameters(activity, enemyHPBonus, enemyFPBonus, enemyStrBonus, enemyAgiBonus, enemyIntBonus,
			crimeRate, baseFatigueDmg, baseStressDmg, baseNotoriety);
	}


	public static void SetHTActivityParameters(string thisActivity, int HPBonus, int FPBonus, int strBonus, int agiBonus, int intBonus,
		int difficulty, int fatigue, int stress, int notoriety) {
		//TODO I don't think I need to pass in all these variables ^, since they don't immediately adjust when declaring the local-variable-versions
		//The locals could probably just be set at the start of this method (here), and later assigned (like they are now), without much difficulty
		//For now, I just want to keep going, but it's something to keep in mind

		switch (thisActivity) {
			/*		case "Arson":
						break;
					case "Bank Robbery":
						break;
					case "Bomb Threat":
						break;
					case "Chemical Attack":
						break;
					case "Kidnapping":
						break;
					case "Mass Shooting":
						break;
			*/
			default:
				if (isEvent) {
					//activitySceneToLoad = "SampleEventActivityScene";
					//TODO Adjust other "stat bonuses" and variables
					HPBonus = 30; FPBonus = 30; notoriety = 30 * difficulty;
				}
				else {
					activitySceneToLoad = "SampleActivityArena";
					HPBonus = 20; FPBonus = 20; //Later, formulate these based on the activity's difficulty
//Not really sure if enemies should have these stats: strBonus = 1; agiBonus = 1; intBonus = 1;
					//fatigue = difficulty; stress = difficulty;
					notoriety = 10 * difficulty;
				}
				
				break;
		}

		if (isEvent) {
			Debug.Log("An event is happening here! Get ready!");
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
		//baseFatigueDmg = fatigue;
		//baseStressDmg = stress;

		//Debug.Log("High-Tier parameters set");
	}


	public static void ResetValues() {
		isHighTierActivityHere = false;
		locationName = "";
		activity = "";
		HTCrimeRateSet = false;
		eventCrimeRateSet = false;

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
