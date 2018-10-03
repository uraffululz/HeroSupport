using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommenceNightly : MonoBehaviour {

	[SerializeField] GameObject hero;
	[SerializeField] GameObject sidekick;

	//Hero script references
	ActivityParticipation heroActPart;
	CharacterStats heroStats;

	//Sidekick script references
	ActivityParticipation sideActPart;
	CharacterStats sideStats;

	//Local participation bools
	bool heroDidIt = false;
	bool sideDidIt = false;


	private void Start() {
//TOMAYBEDO Just declare these in the inspector?
		heroActPart = hero.GetComponent<ActivityParticipation>();
		heroStats = hero.GetComponent<CharacterStats>();

		sideActPart = sidekick.GetComponent<ActivityParticipation>();
		sideStats = sidekick.GetComponent<CharacterStats>();
	}


	public void CommenceActivities () {
		//Check toggles to determine which, if any, heroes are participating in the night's activities
		if (heroActPart.toggleNightOff.isOn && sideActPart.toggleNightOff.isOn) {
			//Neither of the heroes are participating in the night's activities. They are both taking the night off
			print("The heroes are taking the night off. Good luck, citizens!");
		}
//Just an "else" statement? If there are only two options
		else if (!heroActPart.toggleNightOff.isOn || !sideActPart.toggleNightOff.isOn) {
			StartCoroutine ("KickSomeAss");
		}
	}


	IEnumerator KickSomeAss() {
		//Determine in which activities the hero and/or sidekick will be partipating
		//TOMAYBEDO Do I want the sidekick to be able to participate in a particular activity if the hero is not also participating in it?

		yield return new WaitForSeconds(3.0f);
		ProcessActivity(heroActPart.toggle1, sideActPart.toggle1, NightManager.activity1,
			NightManager.reqStr1, NightManager.reqAgi1, NightManager.reqInt1, NightManager.baseFatigueDmg1, NightManager.baseStressDmg1);

		yield return new WaitForSeconds(3.0f);
		ProcessActivity(heroActPart.toggle2, sideActPart.toggle2, NightManager.activity2,
			NightManager.reqStr2, NightManager.reqAgi2, NightManager.reqInt2, NightManager.baseFatigueDmg2, NightManager.baseStressDmg2);

		yield return new WaitForSeconds(3.0f);
		ProcessActivity(heroActPart.toggle3, sideActPart.toggle3, NightManager.activity3,
			NightManager.reqStr3, NightManager.reqAgi3, NightManager.reqInt3, NightManager.baseFatigueDmg3, NightManager.baseStressDmg3);
	}


	void ProcessActivity (Toggle heroTog, Toggle sideTog, string activitySelected, int strReq, int agiReq, int intReq, int fatigueDmg, int stressDmg) {
		if (heroTog.isOn || sideTog.isOn) {
			//Just declaring these to get started
//TOMAYBEDO Do I need to add "1" to these variables to explicitly declare them as part of their own method, so they don't interfere with the other activities?
	//If it's only processing one at a time, it probably doesn't matter. Otherwise, yeah, add those numerical suffixes
	//Let me try doing this in coroutines before I delete this idea, as I might need to do it then
			int totalStr = 0;
			int totalAgi = 0;
			int totalInt = 0;

			if (heroTog.isOn && sideTog.isOn) {
				heroDidIt = true;
				sideDidIt = true;

				//Combining and randomizing stats from participating heroes
				totalStr = (int)((heroStats.strength.GetValue() + sideStats.strength.GetValue()) * Random.Range(0.75f, 1.25f));
				totalAgi = (int)((heroStats.agility.GetValue() + sideStats.agility.GetValue()) * Random.Range(0.75f, 1.25f));
				totalInt = (int)((heroStats.intellect.GetValue() + sideStats.intellect.GetValue()) * Random.Range(0.75f, 1.25f));

				Debug.Log(totalStr);
				Debug.Log(totalAgi);
				Debug.Log(totalInt);
				Debug.Log("Requirements: " + strReq + ", " + agiReq + ", " + intReq);
				Debug.Log(activitySelected + ": Hero did it? " + heroDidIt + ", Sidekick did it? " + sideDidIt); //Both should be true
			}
			else if (heroTog.isOn && !sideTog.isOn) {
				heroDidIt = true;

//TODO This is where I should set the Hero's status to "Crimefighting", and keep the Sidekick(s) "Idle" (or whatever he's already doing)

				//Combining and randomizing stats from participating heroes
				totalStr = (int)(heroStats.strength.GetValue() * Random.Range(0.75f, 1.25f));
				totalAgi = (int)(heroStats.agility.GetValue() * Random.Range(0.75f, 1.25f));
				totalInt = (int)(heroStats.intellect.GetValue() * Random.Range(0.75f, 1.25f));

				Debug.Log(totalStr);
				Debug.Log(totalAgi);
				Debug.Log(totalInt);
				Debug.Log("Requirements: " + strReq + ", " + agiReq + ", " + intReq);
				Debug.Log(activitySelected + ": Hero did it? " + heroDidIt + ", Sidekick did it? " + sideDidIt); //Hero should be true, Side should be false
			}
			else if (!heroTog.isOn && sideTog.isOn) {
				sideDidIt = true;

//TODO This is where I should set the Hero's status to "Idle", and keep the Sidekick(s) "Crimefighting" (or whatever he's already doing)

				//Combining and randomizing stats from participating heroes
				totalStr = (int)(sideStats.strength.GetValue() * Random.Range(0.75f, 1.25f));
				totalAgi = (int)(sideStats.agility.GetValue() * Random.Range(0.75f, 1.25f));
				totalInt = (int)(sideStats.intellect.GetValue() * Random.Range(0.75f, 1.25f));

				Debug.Log(totalStr);
				Debug.Log(totalAgi);
				Debug.Log(totalInt);
				Debug.Log("Requirements: " + strReq + ", " + agiReq + ", " + intReq);
				Debug.Log(activitySelected + ": Hero did it? " + heroDidIt + ", Sidekick did it? " + sideDidIt); //Hero should be false, Side should be true
			}
			//After determining the total stats for the characters
			//If any of the total stats is below "1", set them to "1". They should never be "0", probably.
//TOMAYBEDO Probably redundant. If I keep the "ranges" tight, the results will always be >= 1
			if (totalStr <= 0) {
				totalStr = 1;
			}
			if (totalAgi <= 0) {
				totalAgi = 1;
			}
			if (totalInt <= 0) {
				totalInt = 1;
			}

			//Determine whether the hero(es) fail or succeed at the activity
			if (totalStr < strReq || totalAgi < agiReq || totalInt < intReq) {
				MissionFailed(activitySelected, totalStr, totalAgi, totalInt, strReq, agiReq, intReq, fatigueDmg, stressDmg);
			}
			else {
				MissionAccomplished(activitySelected, fatigueDmg, stressDmg);
			}
		}

		//Resetting local participation bools
		heroDidIt = false;
		sideDidIt = false;
		//Debug.Log(activitySelected + ": Hero did it? " + heroDidIt + ", Sidekick did it? " + sideDidIt); //Both should always be false at this point
	}


	void MissionAccomplished (string activityName, int baseFatigueDamage, int baseStressDamage) {
		//Activity victory results
		print ("You successfully stopped the " + activityName);

		//If the Hero participated in the activity, increase their fatigue and stress accordingly
		if (heroDidIt) {
			DamageHero(baseFatigueDamage, baseStressDamage, Random.Range(0, .5f));
		}
		//If the Sidekick participated in the activity, increase their fatigue and stress accordingly
		if (sideDidIt) {
			DamageSidekick(baseFatigueDamage, baseStressDamage, Random.Range(0, .5f));
		}
	}


	void MissionFailed (string activityName, int strTotal, int agiTotal, int intTotal, int vsStr, int vsAgi, int vsInt, int baseFatigueDmg, int baseStressDmg) {
		//Activity defeat results
		
		bool strFailed = false;
		bool agiFailed = false;
		bool intFailed = false;

		if (strTotal < vsStr) {
			strFailed = true;
		}
		if (agiTotal < vsAgi) {
			agiFailed = true;
		}
		if (intTotal < vsInt) {
			intFailed = true;
		}

//TOMAYBEDO Is there an easier or more efficient way to do this?
		//Determine the results of the failings of the hero's stats, and display via flavor-text
//TOMAYBEDO Should they be able to fail "by degrees", or is it a strict pass/fail system?
		//Maybe failing with Strength reduces their ability to resist fatigue in the next activity
		//Thus failing with Agility might reduce their reaction time or ability to get around town quickly
			//How to gamify this?
		//Failing with Intellect would reduce their ability to resist stress in the next activity
		//Maybe these failings also affect what they can do at home

		//If they fail with all stats
		if (strFailed && agiFailed && intFailed) {
			print("You suck at everything");
		}

		//If they fail "first" with strength
		else if (strFailed && !agiFailed && !intFailed) {
			print("You were not strong enough to stop the " + activityName);
		}
		else if (strFailed && agiFailed && !intFailed) {
			print("You were not strong or fast enough to stop the " + activityName);
		}
		else if (strFailed && !agiFailed && intFailed) {
			print("You were not strong or smart enough to stop the " + activityName);
		}

		//If they fail "first" with agility
		else if (!strFailed && agiFailed && !intFailed) {
			print("You were not fast enough to stop the " + activityName);
		}
		else if (!strFailed && agiFailed && intFailed) {
			print("You were not fast or smart enough to stop the " + activityName);
		}

		//If they fail "first" with intellect
		else if (!strFailed && !agiFailed && intFailed) {
			print("You were not smart enough to stop the " + activityName);
		}

		//If nothing fails (This shouldn't happen here)
		else if (!strFailed && !agiFailed && !intFailed) {
			print("This shouldn't be here. You fucked something up. SEE ME");
		}

		//If the Hero participated in the activity, increase their fatigue and stress accordingly
		if (heroDidIt) {
			DamageHero(baseFatigueDmg, baseStressDmg, Random.Range(.5f, 1.5f));
		}
		//If the Sidekick participated in the activity, increase their fatigue and stress accordingly
		if (sideDidIt) {
			DamageSidekick(baseFatigueDmg, baseStressDmg, Random.Range(.5f, 1.5f));
		}
	}


//TODO These "Damaging" methods \/ are both very similar. They could probably be combined into one, unless I diversify them somehow
	void DamageHero (int damageBaseFatigue, int damageBaseStress, float rate) {
		heroStats.DamageFatigue((int)(damageBaseFatigue * rate));

//TODO Factor in the number of casualties caused by the character's actions (none, some, many, lots?)
		heroStats.DamageStress((int)(damageBaseStress * rate));
	}


	void DamageSidekick(int damageBaseFatigue, int damageBaseStress, float rate) {
//TOMAYBEDO If I want the sidekick to take more (on average) fatigue/stress than the hero, this is where to do it
	//This would ensure that the sidekick can't be used every night, for every activity
		sideStats.DamageFatigue((int)(damageBaseFatigue * rate));

//TODO Factor in the number of casualties caused by the character's actions (none, some, many, lots?)
		sideStats.DamageStress((int)(damageBaseStress * rate));
	}
}
