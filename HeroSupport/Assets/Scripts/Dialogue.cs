using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

	//Name of the NPC being talked to
	public string name;

	//The array of sentences containing the NPC's dialogue progression
//OR dialogue OPTIONS, later
	[TextArea(3, 10)]
	public string[] sentences;
}
