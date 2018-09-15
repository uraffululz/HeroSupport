using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;


	//Triggers the DialogueManager script to start the conversation with the chosen NPC
	public void TriggerDialogue() {
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

		
	}
}
