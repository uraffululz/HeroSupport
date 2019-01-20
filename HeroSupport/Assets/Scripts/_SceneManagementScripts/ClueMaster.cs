using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ClueMaster {

	public static bool eventOngoing = false;
	public static bool eventUncovered = false;

	public static int nightsUntilEventEnds = 7;
	public static bool notifyResultsOfEventFailure = true;

	public static bool matchLocation = false;
	public static bool matchGang = false;
	public static bool matchAttackType = false;

	public static int maxNumberOfClues = 9;
	public static int numberOfCluesFound = 0;
	public static string mostRecentClue = "";
	public static List<int> clueTypes = new List<int>();
	
	//Declaring and initializing LOCATION clue variables
	static int whichLocation = 0;
//TOMAYBEDO Add more locations: Subway Terminal, etc.
	//TOPROBABLYDO I could clean this up a bit by just merging these two arrays \/ into a single 2-dimensional array,
	//with the location NAME taking the place of the [0] index of the second dimension, and the clues taking the places of [1] - [4]
	//I would just need to switch some 0's to 1's below, when "finding clues" and change the array references of the deleted array to the new one
	static string[] locations = new string[]
		{"Amphitheater", "City Hall", "Clock Tower", "College Campus","First Bank", "Hospital", "International Corporation HQ",
			"Museum", "News Station", "Police Station" };
	static string[,] locationClues = new string[10/*Same # as locations[]*/,3]
		{/*[0] Amphitheater*/{"Amphitheater clue #1", "Amphitheater clue #2", "Amphitheater clue #3" },
			/*[1] City Hall*/{"Government Building", "City Hall clue #2", "City Hall clue #3" },
			/*[2] Clock Tower*/{"Clock Tower clue #1",  "Clock Tower clue #2", "Clock Tower clue #3"},
			/*[3] College Campus*/{"Expansive", "Populated Area", "Several buildings"},
			/*[4] First Bank*/{"Financial Building", "First Bank clue #2", "First Bank clue #3" },
			/*[5] Hospital*/{"Medical Building", "Hospital clue #2", "Hospital clue #3" },
			/*[6] International Corp. HQ*/{"Tall Building", "Financial Building", "Int. Corp. HQ clue #3" }, 
			/*[7] Museum*/{"Museum clue #1", "Museum clue #2", "Museum clue #3" },
			/*[8] News Station*/{"News Station clue #1", "News Station clue #2", "News Station clue #3" },
			/*[9] Police Station*/{"Government Building", "Police Station clue #2", "Police Station clue #3" }
		};
	public static List<string> relevantLocationclues = new List<string>();
	public static string[] knownLocationClues = new string[3] { "", "", "" };
	public static int locationCluesFound = 0;

	//Declaring and initializing GANG INVOLVED clue variables
	static int whichGang = 0;
	public static string[] gangs = new string[] { "The Jackals", "The Clone Army", "The Ember-kin", "The Lezzies" };
	public static string gangInvolvedInEvent;
	static string[,] gangClues = new string[4/*Same as gangs[]*/, 3] 
		{/*[0] The Jackals*/ {"The Jackals Clue #1", "The Jackals Clue #2", "The Jackals Clue #3"},
			/*[1] The Clone Army*/ {"The Clone Army Clue #1", "The Clone Army Clue #2", "The Clone Army Clue #3"},
			/*[2] The Ember-kin*/ {"The Ember-kin Clue #1", "The Ember-kin Clue #2", "The Ember-kin Clue #3"},
			/*[3] The Lezzies*/ {"The Lezzies Clue #1", "The Lezzies Clue #2", "The Lezzies Clue #3"}
		};
	public static List<string> relevantGangClues = new List<string>();
	public static string[] knownGangClues = new string[3] { "", "", "" };
	public static int gangCluesFound = 0;

	//Declaring and initializing ATTACKTYPE clue variables
	static int whichAttackType = 0;
	static string[] attackTypes = new string[] {"Arson", "Bomb Threat", "Chemical Attack", "Hostage Situation", "Kidnapping",
												"Mass Shooting", "Robbery"};
	static string[,] attackTypeClues = new string[7/*Same # as attackTypes[]*/, 3]
		{/*[0] Arson*/{"Arson clue #1","Arson clue #2","Arson clue #3"},
			/*[1] Bomb Threat */{"Bomb Threat clue #1","Bomb Threat clue #2","Bomb Threat clue #3"},
			/*[2] Chemical Attack*/ {"Chemical Attack clue #1","Chemical Attack clue #2","Chemical Attack clue #3"},
			/*[3] Hostage Situation*/ {"Hostage Situation clue #1","Hostage Situation clue #2","Hostage Situation clue #3"},
			/*[4] Kidnapping*/ {"Kidnapping clue #1", "Kidnapping clue #2", "Kidnapping clue #3"},
			/*[5] Mass Shooting*/ {"Mass Shooting clue #1","Mass Shooting clue #2","Mass Shooting clue #3"},
			/*[6] Robbery*/ {"Robbery clue #1","Robbery clue #2","Robbery clue #3"}
		};
	public static List<string> relevantAttackTypeclues = new List<string>();
	public static string[] knownAttackTypeClues = new string[3] { "", "", "" };
	public static int attackTypeCluesFound = 0;

	public static string location = "";
	public static string gang = "";
	public static string attackType = "";


	private static void Start() {

	}


	//This is run when the player finds their FIRST CLUE during random nightly activities, and the event is initialized
	public static void ChooseEventParameters () {
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

		//Determine WHICH GANG is planning the event
		whichGang = Random.Range(0, gangs.Length);
		gangInvolvedInEvent = gangs[whichGang];
		for (int i = 0; i < 3; i++) {
			relevantGangClues.Add(gangClues[whichGang, i]);
		}

		//Determine HOW the attack will happen
		whichAttackType = Random.Range(0, attackTypes.Length);
		attackType = attackTypes[whichAttackType];
		for (int i = 0; i < 3; i++) {
			relevantAttackTypeclues.Add(attackTypeClues[whichAttackType, i]);
		}

		Debug.Log("Location: " + location + ", Gang: " + gangInvolvedInEvent + ", Attack Type: " + attackType);
	}


	public static void GetAClue() {
		if (locationCluesFound < 3) {
			clueTypes.Add(1);
		}
		if (gangCluesFound < 3) {
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
				GetAGangClue();
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


	static void GetAGangClue() {
		int clueNum = Random.Range(0, relevantGangClues.Count);
		knownGangClues[gangCluesFound] = relevantGangClues[clueNum];
		relevantGangClues.RemoveAt(clueNum);

		mostRecentClue = knownGangClues[gangCluesFound];
		Debug.Log("You found a GANG clue: " + knownGangClues[gangCluesFound]);
		gangCluesFound++;
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

		notifyResultsOfEventFailure = false;

		EndEvent();
	}


	public static void EventFailure() {
		Debug.Log("You failed the event");

		notifyResultsOfEventFailure = true;

		EndEvent();
	}


	public static void EndEvent() {
		//TODO Make sure that all event parameters are reset once the event is ended
		eventOngoing = false;
		eventUncovered = false;
		NightHighTierManager.isEvent = false;

		nightsUntilEventEnds = 7;

		matchLocation = false;
		matchGang = false;
		matchAttackType = false;

		numberOfCluesFound = 0;
		locationCluesFound = 0;
		gangCluesFound = 0;
		attackTypeCluesFound = 0;
		mostRecentClue = "";

		relevantLocationclues.Clear();
		relevantGangClues.Clear();
		relevantAttackTypeclues.Clear();

		knownLocationClues = new string[3] { "", "", "" };
		knownGangClues = new string[3] { "", "", "" };
		knownAttackTypeClues = new string[3] { "", "", "" };

		location = "";
		gangInvolvedInEvent = "";
		attackType = "";
	}
	
}
