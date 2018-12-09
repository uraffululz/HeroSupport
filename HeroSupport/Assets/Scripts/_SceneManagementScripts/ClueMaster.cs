using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ClueMaster {

	public static bool eventOngoing = false;
	public static bool eventUncovered = false;

	public static bool matchLocation = false;
	public static bool matchTarget = false;
	public static bool matchAttackType = false;


	public static int maxNumberOfClues = 9;
	public static int numberOfCluesFound = 0;
	public static string mostRecentClue = "";
	public static List<int> clueTypes = new List<int>();

	public static string[] gangs = new string[] {"The Jackals", "The Clone Army", "The Ember-kin"};
	public static string gangInvolvedInEvent;

	//TODO I only really want to use Locations OR targets. Seems weird/redundant to use both.
	//What do I use in place of the other, then? I want to have at least 3 categories of clues.
	//Maybe instead of the target (WHO?), the hero focuses on the villain's motivation (WHY?). I mean, Batman tends to figure that shit out eventually

	//Declaring and initializing LOCATION clue variables
	static int whichLocation = 0;
//TODO Add more locations: Amphitheater, Sport Stadium, Clock Tower, Subway Terminal, Museum, Local News Station, General Hospital
	//TOPROBABLYDO I could clean this up a bit by just merging these two arrays \/ into a single 2-dimensional array,
	//with the location NAME taking the place of the [0] index of the second dimension, and the clues taking the places of [1] - [4]
	//I would just need to switch some 0's to 1's below, when "finding clues" and change the array references of the deleted array to the new one
	static string[] locations = new string[6]
		{"Bridge", "City Hall", "College Campus","First Bank", "International Corporation HQ", "Police Station" };
	static string[,] locationClues = new string[6/*Same # as locations[]*/,3]
		{/*[0] Bridge*/{"Bridge clue #1", "Bridge clue #2", "Bridge clue #3" },
			/*[1] City Hall*/{"Government Building", "City Hall clue #2", "City Hall clue #3" },
			/*[2] College Campus*/{"Expansive", "Populated Area", "Several buildings"},
			/*[3] First Bank*/{"Financial Building", "First Bank clue #2", "First Bank clue #3" },
			/*[4] International Corp. HQ*/{"Tall Building", "Financial Building", "Int. Corp. HQ clue #3" }, 
			/*[5] Police Station*/{"Government Building", "Police Station clue #2", "Police Station clue #3" }
		};
	public static List<string> relevantLocationclues = new List<string>();
	public static string[] knownLocationClues = new string[3] { "", "", "" };
	public static int locationCluesFound = 0;

	//Declaring and initializing TARGET clue variables
	static int whichTarget = 0;
	static string[] targets = new string[] {"Chief of Police", "District Attorney", "Judge", "Mayor", "Mob Boss" };
	static string[,] targetClues = new string[5/*Same # as targets[]*/, 3]
		{/*[0] Chief of Police */{"Chief clue #1","Chief clue #2","Chief clue #3"},
			/*[1] District Attorney*/ {"DA clue #1","DA clue #2","DA clue #3"},
			/*[2] Judge*/ {"Judge clue #1","Judge clue #2","Judge clue #3"},
			/*[3] Mayor*/ {"Mayor clue #1","Mayor clue #2","Mayor clue #3"},
			/*[4] Mob Boss*/ {"Mob Boss clue #1","Mob Boss clue #2","Mob Boss clue #3"}
		};
	public static List<string> relevantTargetclues = new List<string>();
	public static string[] knownTargetClues = new string[3] { "", "", "" };
	public static int targetCluesFound = 0;

	//Declaring and initializing ATTACKTYPE clue variables
	static int whichAttackType = 0;
	static string[] attackTypes = new string[] { "Bombing", "Chemical Attack", "Shooting", "Sniper" };
	static string[,] attackTypeClues = new string[4/*Same # as targets[]*/, 3]
		{/*[0] Bombing */{"Bombing clue #1","Bombing clue #2","Bombing clue #3"},
			/*[1] Chemical Attack*/ {"Chemical Attack clue #1","Chemical Attack clue #2","Chemical Attack clue #3"},
			/*[2] Shooting*/ {"Shooting clue #1","Shooting clue #2","Shooting clue #3"},
			/*[3] Sniper*/ {"Sniper clue #1","Sniper clue #2","Sniper clue #3"}
		};
	public static List<string> relevantAttackTypeclues = new List<string>();
	public static string[] knownAttackTypeClues = new string[3] { "", "", "" };
	public static int attackTypeCluesFound = 0;

	public static string location = "";
	public static string target = "";
	public static string attackType = "";


	private static void Start() {

	}


	//This is run when the player finds their FIRST CLUE during random nightly activities, and the event is initialized
	public static void ChooseEventParameters () {
		eventOngoing = true;

		//Determine WHO IS PLANNING the event
		gangInvolvedInEvent = gangs[Random.Range(0, gangs.Length)];

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
	}


	public static void GetAClue() {
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


	static void GetALocationClue() {
		//TOMAYBEDO Add all of the clues (each of which is probably relevant to at least 2 different buildings) to an array,
		//which are then passed to a list of the 3 location clues. Then, as this method is run, select and remove a random item from that list

		//TODO Make sure this method doesn't select the same clue multiple times

		int clueNum = Random.Range(0, relevantLocationclues.Count);
		knownLocationClues[locationCluesFound] = relevantLocationclues[clueNum];
		relevantLocationclues.RemoveAt(clueNum);

		mostRecentClue = knownLocationClues[locationCluesFound];
		Debug.Log("You found a LOCATION clue: " + knownLocationClues[locationCluesFound]);
		locationCluesFound++;
	}


	static void GetATargetClue() {
		int clueNum = Random.Range(0, relevantTargetclues.Count);
		knownTargetClues[targetCluesFound] = relevantTargetclues[clueNum];
		relevantTargetclues.RemoveAt(clueNum);

		mostRecentClue = knownTargetClues[targetCluesFound];
		Debug.Log("You found a TARGET clue: " + knownTargetClues[targetCluesFound]);
		targetCluesFound++;
	}


	static void GetAnAttackTypeClue() {
		int clueNum = Random.Range(0, relevantAttackTypeclues.Count);
		knownAttackTypeClues[attackTypeCluesFound] = relevantAttackTypeclues[clueNum];
		relevantAttackTypeclues.RemoveAt(clueNum);

		mostRecentClue = knownAttackTypeClues[attackTypeCluesFound];
		Debug.Log("You found an ATTACK TYPE clue: " + knownAttackTypeClues[attackTypeCluesFound]);
		attackTypeCluesFound++;
	}


	public static void EventUncovered() {
		Debug.Log("You uncovered the event!");
		eventUncovered = true;
	}


	public static void EventSuccess() {
		Debug.Log("You successfully completed the event");

		EndEvent();
	}


	public static void EventFailure() {
		Debug.Log("You failed the event");

		EndEvent();
	}


	public static void EndEvent() {
		//TODO Make sure that all event parameters are reset once the event is ended
		eventOngoing = false;
		eventUncovered = false;
		NightHighTierManager.isEvent = false;

		matchLocation = false;
		matchTarget = false;
		matchAttackType = false;

		numberOfCluesFound = 0;
		locationCluesFound = 0;
		targetCluesFound = 0;
		attackTypeCluesFound = 0;
		mostRecentClue = "";

		relevantLocationclues.Clear();
		relevantTargetclues.Clear();
		relevantAttackTypeclues.Clear();

		knownLocationClues = new string[3] { "", "", "" };
		knownTargetClues = new string[3] { "", "", "" };
		knownAttackTypeClues = new string[3] { "", "", "" };

		location = "";
		target = "";
		attackType = "";
	}
	
}
