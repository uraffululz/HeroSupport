using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

	//Name of the NPC being talked to
	//Keep public, as it is referenced elsewhere
	public string name;

	//The array of sentences containing the NPC's dialogue progression
	//OR dialogue OPTIONS, later, as determined by which NPC is being spoken to, as well as a reference to their current "state" (idle, busy, hero-ing, etc)
	[TextArea(3, 10)]
	public string[] sentences;
}
