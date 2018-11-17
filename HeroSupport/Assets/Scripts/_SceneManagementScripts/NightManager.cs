using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public static class NightManager {

	public static string activitySceneToLoad;

	public static int crimeRate1;

	public static string crimeStars1;

	public static string activity1;
	static string[] activities = new string[] { "Arson", "Assault", "Bank Robbery", "Burglary", "Kidnapping", "Mugging", "Robbery", "Runaway Train" };

	public static int enemyHPBonus1 = 0;
	public static int enemyFPBonus1 = 0;
	public static int enemyStrBonus1 = 0;
	public static int enemyAgiBonus1 = 0;
	public static int enemyIntBonus1 = 0;

	public static int baseFatigueDmg1 = 0;
	public static int baseStressDmg1 = 0;

	public static int baseNotoriety1 = 0;


	public static void SetCrimeRates () {
		crimeStars1 = "";
	
		crimeRate1 = Random.Range(1, 6);
		
		for (int i = 1; i <= crimeRate1; i++) {
			crimeStars1 += "*";
		}
	
		activity1 = activities[Random.Range(0, activities.Length)];

		SetActivityParameters(activity1, enemyHPBonus1, enemyFPBonus1, enemyStrBonus1, enemyAgiBonus1, enemyIntBonus1,
			crimeRate1, baseFatigueDmg1, baseStressDmg1, baseNotoriety1);
		
		//Debug.Log(activity1 + " requires: " + reqStr1 + ", " + reqAgi1 + ", " + reqInt1 + ". Will damage Fatigue: " + baseFatigueDmg1 + " Stress: " + baseStressDmg1);
	}


	public static void SetActivityParameters(string thisActivity, int HPBonus, int FPBonus, int strBonus, int agiBonus, int intBonus, 
		int difficulty, int fatigue, int stress, int notoriety) {
		//TODO I don't think I need to pass in all these variables ^, since they don't immediately adjust when declaring the local-variable-versions
		//The locals could probably just be set at the start of this method (here), and later assigned (like they are now), without much difficulty
		//For now, I just want to keep going, but it's something to keep in mind
		
		switch (thisActivity) {
	/*		case "Arson":
				break;
			case "Assault":
				break;
			case "Bank Robbery":
				break;
			case "Burglary":
				break;
			case "Kidnapping":
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
				HPBonus += 10; FPBonus += 10;
				strBonus += 1; agiBonus += 1; intBonus += 1;
				fatigue += difficulty; stress += difficulty;
				notoriety = 10;
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
			enemyHPBonus1 = HPBonus;
			enemyFPBonus1 = FPBonus;
			enemyStrBonus1 = strBonus;
			enemyAgiBonus1 = agiBonus;
			enemyIntBonus1 = intBonus;
			baseFatigueDmg1 = fatigue;
			baseStressDmg1 = stress;
	}
}
