using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueMaster : MonoBehaviour {

	[SerializeField] GameObject hero;
	[SerializeField] GameObject sidekick;

	public bool eventOngoing = false;

	[SerializeField] Image compDisplay;
	[SerializeField] Image clueDisplay;
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

	bool matchLocation = false;
	bool matchTarget = false;
	bool matchAttackType = false;

	//Hero script references
	ActivityParticipation heroActPart;
	CharacterStats heroStats;

	//Sidekick script references
	ActivityParticipation sideActPart;
	CharacterStats sideStats;

	int maxNumberOfClues = 9;
	int numberOfCluesFound = 0;
	List<int> clueTypes = new List<int>();

	//TODO I only really want to use Locations OR targets. Seems weird/redundant to use both.
	//What do I use in place of the other, then? I want to have at least 3 categories of clues.
	//Maybe instead of the target (WHO?), the hero focuses on the villain's motivation (WHY?). I mean, Batman tends to figure that shit out eventually

	//Declaring and initializing LOCATION clue variables
	int whichLocation = 0;
//TOPROBABLYDO I could clean this up a bit by just merging these two arrays \/ into a single 2-dimensional array,
	//with the location NAME taking the place of the [0] index of the second dimension, and the clues taking the places of [1] - [4]
	//I would just need to switch some 0's to 1's below, when "finding clues" and change the array references of the deleted array to the new one
	string[] locations = new string[6]
		{"Bridge", "City Hall", "College Campus","First Bank", "Int. Corp. HQ", "Police Station" };
	string [,] locationClues = new string[6/*Same # as locations[]*/,3]
		{/*[0] Bridge*/{"Bridge clue #1", "Bridge clue #2", "Bridge clue #3" },
			/*[1] City Hall*/{"Government Building", "City Hall clue #2", "City Hall clue #3" },
			/*[2] College Campus*/{"Expansive", "Populated Area", "Several buildings"},
			/*[3] First Bank*/{"Financial Building", "First Bank clue #2", "First Bank clue #3" },
			/*[4] International Corp. HQ*/{"Tall Building", "Financial Building", "Int. Corp. HQ clue #3" }, 
			/*[5] Police Station*/{"Government Building", "Police Station clue #2", "Police Station clue #3" }
		};
	List<string> relevantLocationclues = new List<string>();
	public string[] knownLocationClues = new string[3] { "", "", "" };
	int locationCluesFound = 0;

	//Declaring and initializing TARGET clue variables
	int whichTarget = 0;
	string[] targets = new string[] {"Chief of Police", "District Attorney", "Judge", "Mayor", "Mob Boss" };
	string[,] targetClues = new string[5/*Same # as targets[]*/, 3]
		{/*[0] Chief of Police */{"Chief clue #1","Chief clue #2","Chief clue #3"},
			/*[1] District Attorney*/ {"DA clue #1","DA clue #2","DA clue #3"},
			/*[2] Judge*/ {"Judge clue #1","Judge clue #2","Judge clue #3"},
			/*[3] Mayor*/ {"Mayor clue #1","Mayor clue #2","Mayor clue #3"},
			/*[4] Mob Boss*/ {"Mob Boss clue #1","Mob Boss clue #2","Mob Boss clue #3"}
		};
	List<string> relevantTargetclues = new List<string>();
	public string[] knownTargetClues = new string[3] { "", "", "" };
	int targetCluesFound = 0;

	//Declaring and initializing ATTACKTYPE clue variables
	int whichAttackType = 0;
	string[] attackTypes = new string[] { "Bombing", "Chemical Attack", "Shooting", "Sniper" };
	string[,] attackTypeClues = new string[4/*Same # as targets[]*/, 3]
		{/*[0] Bombing */{"Bombing clue #1","Bombing clue #2","Bombing clue #3"},
			/*[1] Chemical Attack*/ {"Chemical Attack clue #1","Chemical Attack clue #2","Chemical Attack clue #3"},
			/*[2] Shooting*/ {"Shooting clue #1","Shooting clue #2","Shooting clue #3"},
			/*[3] Sniper*/ {"Sniper clue #1","Sniper clue #2","Sniper clue #3"}
		};
	List<string> relevantAttackTypeclues = new List<string>();
	public string[] knownAttackTypeClues = new string[3] { "", "", "" };
	int attackTypeCluesFound = 0;

	public string location = "";
	public string target = "";
	public string attackType = "";


	private void Start() {
		heroActPart = hero.GetComponent<ActivityParticipation>();
		heroStats = hero.GetComponent<CharacterStats>();

		sideActPart = sidekick.GetComponent<ActivityParticipation>();
		sideStats = sidekick.GetComponent<CharacterStats>();

		locationClueText1.text = "";
		locationClueText2.text = "";
		locationClueText3.text = "";
		targetClueText1.text = "";
		targetClueText2.text = "";
		targetClueText3.text = "";
		attackTypeClueText1.text = "";
		attackTypeClueText2.text = "";
		attackTypeClueText3.text = "";
	}


	//This is run when the player finds their FIRST CLUE during random nightly activities, and the event is initialized
	public void ChooseEventParameters () {
		eventOngoing = true;
		//Determine WHERE the event will take place
		whichLocation = Random.Range(0, locations.Length);
		//Debug.Log("Location # " + whichLocation);
		location = locations[whichLocation];
		//Debug.Log("Location: " + location);
		for (int i = 0; i < 3; i++) {
			relevantLocationclues.Add(locationClues[whichLocation, i]);
			//Debug.Log(locationClues[whichLocation, i]);
		}

		//Determine WHO the target is
		whichTarget = Random.Range(0, targets.Length);
		target = targets[whichTarget];
		for (int i = 0; i < 3; i++) {
			relevantTargetclues.Add(targetClues[whichTarget, i]);
		}

		//Determine HOW the attack will happen
		whichAttackType = Random.Range(0, attackTypes.Length);
		attackType = attackTypes[whichAttackType];
		for (int i = 0; i < 3; i++) {
			relevantAttackTypeclues.Add(attackTypeClues[whichAttackType, i]);
		}

		Debug.Log("Location: " + location + ", Target: " + target + ", Attack Type: " + attackType);

		//Enable the toggles associated with this activity (which are then used below in ObtainAdditionalClues to pass into SearchForClues)
		heroActPart.activityText2.SetActive(true);
		sideActPart.activityText2.SetActive(true);
	}


	public IEnumerator ObtainAdditionalClues () {
		//The player can only find up to 9 clues to assist in their investigation
		if (numberOfCluesFound < maxNumberOfClues) {
			Debug.Log("Obtaining additional clues");
			yield return new WaitForSeconds(1f);
			SearchForClues(heroActPart.toggle2, sideActPart.toggle2);
		}
		else {
			Debug.Log("You have obtained the maximum number of clues to assist in your investigation");
		}
		
	}


	public void SearchForClues(Toggle heroDetecting, Toggle sideDetecting) {
		//Basically the same as CommenceNightly.ProcessActivity(), but with different parameters

		//The characters' ability to find more clues is based on their intellect vs. some kind of difficulty
		int clueDifficulty = Random.Range(0, 5); //0 to 9? 10? 11? Should it be less random, and more calculated, like the activities?

		int heroRolledInt = 0;
		int sideRolledInt = 0;

		//If BOTH characters are obtaining more clues
		if (heroDetecting.isOn && sideDetecting.isOn) {
			heroRolledInt = (int) (heroStats.intellect.GetValue() * Random.Range(.5f, 1.5f));
			sideRolledInt = (int) (sideStats.intellect.GetValue() * Random.Range(.5f, 1.5f));
		}
		//If the Hero is obtaining clues alone
		else if (heroDetecting.isOn && !sideDetecting.isOn) {
			heroRolledInt = (int)(heroStats.intellect.GetValue() * Random.Range(.5f, 1.5f));
			//sideRolledInt = 0;
		}
		//If the Sidekick is obtaining clues alone
		else if (!heroDetecting.isOn && sideDetecting.isOn) {
			//heroRolledInt = 0;
			sideRolledInt = (int)(sideStats.intellect.GetValue() * Random.Range(.5f, 1.5f));
		}

		int totalRolledInt = (heroRolledInt + sideRolledInt);

		if (totalRolledInt >= clueDifficulty) {
			GetAClue();
		}
		else {
			Debug.Log("You failed to find another clue");
		}


		//TOMAYBEDO Alternatively, they are guaranteed to find a clue, but there is some equal trade-off to not participating in the current activity
		//Takes more time to do (longer waitforseconds), less/no experience boost, more mental stress?


		Debug.Log(numberOfCluesFound);
	}


	public void GetAClue () {
		if (locationCluesFound < 3) {
			clueTypes.Add(1);
		}
		if (targetCluesFound < 3) {
			clueTypes.Add(2);
		}
		if (attackTypeCluesFound < 3) {
			clueTypes.Add(3);
		}

		int clueType = clueTypes[Random.Range(0, clueTypes.Count)];

			switch (clueType) {
				case 1:
					GetALocationClue();
					break;
				case 2:
					GetATargetClue();
					break;
				case 3:
					GetAnAttackTypeClue();
					break;
			}

		clueTypes.Clear();
		numberOfCluesFound++;
	}


	void GetALocationClue() {
//TOMAYBEDO Add all of the clues (each of which is probably relevant to at least 2 different buildings) to an array,
//which are then passed to a list of the 3 location clues. Then, as this method is run, select and remove a random item from that list

//TODO Make sure this method doesn't select the same clue multiple times

		int clueNum = Random.Range(0, relevantLocationclues.Count);
		knownLocationClues[locationCluesFound] = relevantLocationclues[clueNum];
		relevantLocationclues.RemoveAt(clueNum);

		if (locationClueText1.text == "") {
			locationClueText1.text = knownLocationClues[locationCluesFound];
		}
		else if (locationClueText2.text == "") {
			locationClueText2.text = knownLocationClues[locationCluesFound];
		}
		else {
			locationClueText3.text = knownLocationClues[locationCluesFound];
		}

		Debug.Log("You found a LOCATION clue: " + knownLocationClues[locationCluesFound]);
		locationCluesFound++;
	}


	void GetATargetClue() {
		int clueNum = Random.Range(0, relevantTargetclues.Count);
		knownTargetClues[targetCluesFound] = relevantTargetclues[clueNum];
		relevantTargetclues.RemoveAt(clueNum);

		if (targetClueText1.text == "") {
			targetClueText1.text = knownTargetClues[targetCluesFound];
		}
		else if (targetClueText2.text == "") {
			targetClueText2.text = knownTargetClues[targetCluesFound];
		}
		else {
			targetClueText3.text = knownTargetClues[targetCluesFound];
		}

		Debug.Log("You found a TARGET clue: " + knownTargetClues[targetCluesFound]);
		targetCluesFound++;
	}


	void GetAnAttackTypeClue() {
		int clueNum = Random.Range(0, relevantAttackTypeclues.Count);
		knownAttackTypeClues[attackTypeCluesFound] = relevantAttackTypeclues[clueNum];
		relevantAttackTypeclues.RemoveAt(clueNum);

		if (attackTypeClueText1.text == "") {
			attackTypeClueText1.text = knownAttackTypeClues[attackTypeCluesFound];
		}
		else if (attackTypeClueText2.text == "") {
			attackTypeClueText2.text = knownAttackTypeClues[attackTypeCluesFound];
		}
		else {
			attackTypeClueText3.text = knownAttackTypeClues[attackTypeCluesFound];
		}

		Debug.Log("You found an ATTACK TYPE clue: " + knownAttackTypeClues[attackTypeCluesFound]);
		attackTypeCluesFound++;
	}


	public void OpenClueDisplay () {
		clueDisplay.GetComponent<Animator>().SetBool("compActivated", true);
		compDisplay.GetComponent<Animator>().SetBool("compActivated", false);
	}


	public void CloseClueDisplay () {
		clueDisplay.GetComponent<Animator>().SetBool("compActivated", false);
		compDisplay.GetComponent<Animator>().SetBool("compActivated", true);
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


	public void CloseSolver () {
		clueDisplay.GetComponent<Animator>().SetBool("compActivated", true);
		StartCoroutine("CloseSolveDisplay");
	}


	public IEnumerator CloseSolveDisplay() {
		solveDisplay1.GetComponent<Animator>().SetBool("compActivated", false);
		yield return new WaitForSeconds(0.1f);
		solveDisplay2.GetComponent<Animator>().SetBool("compActivated", false);
		yield return new WaitForSeconds(0.1f);
		solveDisplay3.GetComponent<Animator>().SetBool("compActivated", false);
	}

	public void DeselectOtherToggles () {
		
	}


	public void CompareEventSolution() {
		print("Comparing event solution...");

		foreach (Toggle locationToggle in solveLocationToggles) {
			if (locationToggle.isOn) {
				string locationText = locationToggle.GetComponentInChildren<Text>().text;
				if (locationText == location) {
					matchLocation = true;
				}
				print(locationText + ": " + location);
			}
		}
		foreach (Toggle targetToggle in solveTargetToggles) {
			if (targetToggle.isOn) {
				string targetText = targetToggle.GetComponentInChildren<Text>().text;
				if (targetText == target) {
					matchTarget = true;
				}
				print(targetText + ": " + target);
			}
		}
		foreach (Toggle attackTypeToggle in solveAttackTypeToggles) {
			if (attackTypeToggle.isOn) {
				string attackTypeText = attackTypeToggle.GetComponentInChildren<Text>().text;
				if (attackTypeText == attackType) {
					matchAttackType = true;
				}
				print(attackTypeText + ": " + attackType);
			}
		}

		if (matchLocation && matchTarget && matchAttackType) {
			EventSuccess();
		}
		else {
			EventFailure();
		}
	}


	void EventSuccess() {
		print("You completed the event!");

		EndEvent();
	}


	void EventFailure() {
		print("You failed the event");

		EndEvent();
	}

	void EndEvent() {
		matchLocation = false;
		matchTarget = false;
		matchAttackType = false;
		eventOngoing = false;

		heroActPart.activityText2.SetActive(false);
		sideActPart.activityText2.SetActive(false);

		StartCoroutine("CloseSolveDisplay");
		compDisplay.GetComponent<Animator>().SetBool("compActivated", true);
	}
}
