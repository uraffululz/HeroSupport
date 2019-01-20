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
	[SerializeField] Text gangClueText1;
	[SerializeField] Text gangClueText2;
	[SerializeField] Text gangClueText3;
	[SerializeField] Text attackTypeClueText1;
	[SerializeField] Text attackTypeClueText2;
	[SerializeField] Text attackTypeClueText3;

	[SerializeField] Image solveDisplay1;
	[SerializeField] Image solveDisplay2;
	[SerializeField] Image solveDisplay3;
	[SerializeField] Toggle[] solveLocationToggles;
	[SerializeField] Toggle[] solveGangToggles;
	[SerializeField] Toggle[] solveAttackTypeToggles;

	[SerializeField] Text eventFailedText;


	void Start () {
		if (ClueMaster.eventOngoing) {
			gangText.text = ("One of the city's gangs is planning something big");
		}
		else { //If the computer IS notifying the player of their FAILURE TO COMPLETE THE EVENT
			gangText.text = "";
		}

		locationClueText1.text = ClueMaster.knownLocationClues[0];
		locationClueText2.text = ClueMaster.knownLocationClues[1];
		locationClueText3.text = ClueMaster.knownLocationClues[2];
		gangClueText1.text = ClueMaster.knownGangClues[0];
		gangClueText2.text = ClueMaster.knownGangClues[1];
		gangClueText3.text = ClueMaster.knownGangClues[2];
		attackTypeClueText1.text = ClueMaster.knownAttackTypeClues[0];
		attackTypeClueText2.text = ClueMaster.knownAttackTypeClues[1];
		attackTypeClueText3.text = ClueMaster.knownAttackTypeClues[2];
	}


//APPARENTLY AT SOME POINT THIS FUNCTION BECAME TOTALLY IRRELEVANT. STILL KEEPING IT AROUND FOR NOW JUST IN CASE
/*
//TODO Create a listener to call this function whenever any ClueMaster clues are found
	void UpdateClueUI() {
		//Update LOCATION clue texts
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

		//Update GANG INVOLVED clue texts
		if (gangClueText1.text == "") {
			gangClueText1.text = ClueMaster.knownGangClues[0];
		}
		else if (gangClueText2.text == "") {
			gangClueText2.text = ClueMaster.knownGangClues[1];
		}
		else {
			gangClueText3.text = ClueMaster.knownGangClues[2];
		}

		//Update ATTACKTYPE clue texts
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
*/


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
		foreach (Toggle gangToggle in solveGangToggles) {
			gangToggle.isOn = false;
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
		foreach (Toggle gangToggle in solveGangToggles) {
			if (gangToggle.isOn) {
				string gangText = gangToggle.GetComponentInChildren<Text>().text;
				if (gangText == ClueMaster.gangInvolvedInEvent) {
					ClueMaster.matchGang = true;
				}
				print(gangText + ": " + ClueMaster.gangInvolvedInEvent);
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

		if (ClueMaster.matchLocation && ClueMaster.matchGang && ClueMaster.matchAttackType) {
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
