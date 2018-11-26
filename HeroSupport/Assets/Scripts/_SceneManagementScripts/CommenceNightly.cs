using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CommenceNightly : MonoBehaviour {

	ActivityParticipation actPart;

	//[SerializeField] GameObject hero;
	//[SerializeField] GameObject sidekick = null;

	//Hero script references
	//StatsPlayer heroStats;

	//Sidekick script references
	//StatsPlayer sideStats;

	//Local participation bools
	//bool heroDidIt = false;
	//bool sideDidIt = false;


	private void Start() {
		actPart = GetComponent<ActivityParticipation>();
		//hero = GameObject.FindGameObjectWithTag("Player");
		//heroStats = hero.GetComponent<StatsPlayer>();

		//sidekick = GameObject.FindGameObjectWithTag("Sidekick");
		//if (sidekick != null) {
			//sideStats = sidekick.GetComponent<StatsPlayer>();
		//}
		
	}


	public void CommenceActivities () {
		//Check toggles to determine which, if any, heroes are participating in the night's activities
		if (actPart.heroToggleNightOff.isOn && actPart.sideToggleNightOff.isOn) {
			//Neither of the heroes are participating in the night's activities. They are both taking the night off
			print("The heroes are taking the night off. Good luck, citizens!");
		}
//Just an "else" statement? If there are only two options -- This way seems ok to me
		else {
			if (actPart.heroToggle1.isOn || actPart.sideToggle1.isOn) {
				KickSomeAss(NightManager.activitySceneToLoad);
			}
			if (actPart.heroToggle2.isOn || actPart.sideToggle2.isOn) {
				SearchForClues(actPart.heroToggle2, actPart.sideToggle2);
			}
		}
	}


	void KickSomeAss(string activityScene) {
		//Determine in which activities the hero and/or sidekick will be partipating
//TOMAYBEDO Do I want the sidekick to be able to participate in a particular activity if the hero is not also participating in it?

		SceneManager.LoadScene(activityScene);
	}


	public void SearchForClues(Toggle heroDetecting, Toggle sideDetecting) {
		if (ClueMaster.numberOfCluesFound < ClueMaster.maxNumberOfClues) {
			Debug.Log("Obtaining additional clues");

			//The characters' ability to find more clues is based on their intellect vs. some kind of difficulty
			int clueDifficulty = Random.Range(0, 5); //0 to 9? 10? 11? Should it be less random, and more calculated, like the activities?

			int heroRolledInt = 0;
			int sideRolledInt = 0;
			
			//If the Hero is obtaining clues
			if (heroDetecting.isOn) {
				heroRolledInt = (int)(StatsPlayer.valueInt * Random.Range(.5f, 1.5f));
			}
			//If the Sidekick is obtaining clues
			if (sideDetecting.isOn) {
				sideRolledInt = (int)(StatsSidekick.valueInt * Random.Range(.5f, 1.5f));
			}

			int totalRolledInt = (heroRolledInt + sideRolledInt);

			if (totalRolledInt >= clueDifficulty) {
				ClueMaster.GetAClue();
			}
			else {
				Debug.Log("You failed to find another clue");
			}

			//TOMAYBEDO Alternatively, they are guaranteed to find a clue, but there is some equal trade-off to not participating in the current activity
			//Takes more time to do (longer waitforseconds), less/no experience boost, more mental stress?

			//Debug.Log(ClueMaster.numberOfCluesFound);
		}
		else {
			Debug.Log("You have obtained the maximum number of clues to assist in your investigation");
		}
	}
}
