using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueUIManager : MonoBehaviour {

	//[SerializeField] GameObject hero;
	//[SerializeField] GameObject sidekick;

	//Hero script references
	//ActivityParticipation heroActPart;
	//StatsPlayer heroStats;

	//Sidekick script references
	//ActivityParticipation sideActPart;
	//StatsPlayer sideStats;

	[SerializeField] Image clueDisplay;
	[SerializeField] Text gangText;
	[SerializeField] Text locationClueText1;
	[SerializeField] Text locationClueText2;
	[SerializeField] Text locationClueText3;
	[SerializeField] Text targetClueText1;
	[SerializeField] Text targetClueText2;
	[SerializeField] Text targetClueText3;
	[SerializeField] Text attackTypeClueText1;
	[SerializeField] Text attackTypeClueText2;
	[SerializeField] Text attackTypeClueText3;
	[SerializeField] Image solveDisplay1;
	[SerializeField] Image solveDisplay2;
	[SerializeField] Image solveDisplay3;
	[SerializeField] Toggle[] solveLocationToggles;
	[SerializeField] Toggle[] solveTargetToggles;
	[SerializeField] Toggle[] solveAttackTypeToggles;


	void Start () {
		if (ClueMaster.eventOngoing) {
			gangText.text = (ClueMaster.gangInvolvedInEvent + " is planning something");
		}
		locationClueText1.text = ClueMaster.knownLocationClues[0];
		locationClueText2.text = ClueMaster.knownLocationClues[1];
		locationClueText3.text = ClueMaster.knownLocationClues[2];
		targetClueText1.text = ClueMaster.knownTargetClues[0];
		targetClueText2.text = ClueMaster.knownTargetClues[1];
		targetClueText3.text = ClueMaster.knownTargetClues[2];
		attackTypeClueText1.text = ClueMaster.knownAttackTypeClues[0];
		attackTypeClueText2.text = ClueMaster.knownAttackTypeClues[1];
		attackTypeClueText3.text = ClueMaster.knownAttackTypeClues[2];
	}


//TODO Create a listener to call this function whenever any ClueMaster clues are found
	void UpdateClueUI() {
		//Update Location clue texts
//TODO I really don't think I need all of these if-statements, as it's probably not too much effort just to update each text every time
		if (locationClueText1.text == "") {
			locationClueText1.text = ClueMaster.knownLocationClues[0];
		}
		else if (locationClueText2.text == "") {
			locationClueText2.text = ClueMaster.knownLocationClues[1];
		}
		else {
			locationClueText3.text = ClueMaster.knownLocationClues[2];
		}

		//Update Target clue texts
		if (targetClueText1.text == "") {
			targetClueText1.text = ClueMaster.knownTargetClues[0];
		}
		else if (targetClueText2.text == "") {
			targetClueText2.text = ClueMaster.knownTargetClues[1];
		}
		else {
			targetClueText3.text = ClueMaster.knownTargetClues[2];
		}

		//Update AttackType clue texts
		if (attackTypeClueText1.text == "") {
			attackTypeClueText1.text = ClueMaster.knownAttackTypeClues[0];
		}
		else if (attackTypeClueText2.text == "") {
			attackTypeClueText2.text = ClueMaster.knownAttackTypeClues[1];
		}
		else {
			attackTypeClueText3.text = ClueMaster.knownAttackTypeClues[2];
		}
	}



	public void OpenClueDisplay() {
		clueDisplay.GetComponent<Animator>().SetBool("compActivated", true);
	}


	public void CloseClueDisplay() {
		clueDisplay.GetComponent<Animator>().SetBool("compActivated", false);
	}


	public void OpenSolver() {
		clueDisplay.GetComponent<Animator>().SetBool("compActivated", false);
		StartCoroutine("OpenSolveDisplay");
	}


	public IEnumerator OpenSolveDisplay() {
		solveDisplay1.GetComponent<Animator>().SetBool("compActivated", true);
		yield return new WaitForSeconds(0.1f);
		solveDisplay2.GetComponent<Animator>().SetBool("compActivated", true);
		yield return new WaitForSeconds(0.1f);
		solveDisplay3.GetComponent<Animator>().SetBool("compActivated", true);
	}


	public void CloseSolver() {
		clueDisplay.GetComponent<Animator>().SetBool("compActivated", true);
		StartCoroutine("CloseSolveDisplay");
	}


	public IEnumerator CloseSolveDisplay() {
		foreach (Toggle locToggle in solveLocationToggles) {
			locToggle.isOn = false;
		}
		foreach (Toggle tarToggle in solveTargetToggles) {
			tarToggle.isOn = false;
		}
		foreach (Toggle atkToggle in solveAttackTypeToggles) {
			atkToggle.isOn = false;
		}
		solveDisplay1.GetComponent<Animator>().SetBool("compActivated", false);
		yield return new WaitForSeconds(0.1f);
		solveDisplay2.GetComponent<Animator>().SetBool("compActivated", false);
		yield return new WaitForSeconds(0.1f);
		solveDisplay3.GetComponent<Animator>().SetBool("compActivated", false);
	}


	public void CompareEventSolution() {
		print("Comparing event solution...");

		foreach (Toggle locationToggle in solveLocationToggles) {
			if (locationToggle.isOn) {
				string locationText = locationToggle.GetComponentInChildren<Text>().text;
				if (locationText == ClueMaster.location) {
					ClueMaster.matchLocation = true;
				}
				print(locationText + ": " + ClueMaster.location);
			}
		}
		foreach (Toggle targetToggle in solveTargetToggles) {
			if (targetToggle.isOn) {
				string targetText = targetToggle.GetComponentInChildren<Text>().text;
				if (targetText == ClueMaster.target) {
					ClueMaster.matchTarget = true;
				}
				print(targetText + ": " + ClueMaster.target);
			}
		}
		foreach (Toggle attackTypeToggle in solveAttackTypeToggles) {
			if (attackTypeToggle.isOn) {
				string attackTypeText = attackTypeToggle.GetComponentInChildren<Text>().text;
				if (attackTypeText == ClueMaster.attackType) {
					ClueMaster.matchAttackType = true;
				}
				print(attackTypeText + ": " + ClueMaster.attackType);
			}
		}

		if (ClueMaster.matchLocation && ClueMaster.matchTarget && ClueMaster.matchAttackType) {
			ClueMaster.EventUncovered();
			EndEventUI();
		}
		else {
			ClueMaster.EventFailure();
			EndEventUI();
		}
	}


	void EndEventUI() {
		StartCoroutine("CloseSolveDisplay");
	}
}
