using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;


	public void SetupDialogue() {
		if (gameObject.GetComponent<NPCHero>() != null) {
			NPCHero heroScript = gameObject.GetComponent<NPCHero>();

			//Dialogue options for Deek
			if (dialogue.name == "Deek") {
				//During Idle state
				if (heroScript.myActiveState == NPCHero.activityStates.Idle) {
					int randomDialogue = Random.Range(0, 3);

					switch (randomDialogue) {
						case 0:
							dialogue.sentences = new string[1] { "got it?" };
							break;
						case 1:
							dialogue.sentences = new string[2] { "Do you have any idea how fucking long it takes", "to get shit stains out of a pair of dockers?" };
							break;
						case 2:
							dialogue.sentences = new string[1] { "Good deal. About time" };
							break;
					}
				}
			}

			//Dialogue options for Tiffany
			if (dialogue.name == "Tiffany") {
				//During Idle state
				if (heroScript.myActiveState == NPCHero.activityStates.Idle) {
					int randomDialogue = Random.Range(0, 3);
					switch (randomDialogue) {
						case 0:
							dialogue.sentences = new string[1] { "My turn now?" };
							break;
						case 1:
							dialogue.sentences = new string[2] { "Ouch!", "Fuck!" };
							break;
						case 2:
							dialogue.sentences = new string[1] { "Moving on..." };
							break;
					}
				}
			}

			TriggerDialogue();
		}
	}

	//Triggers the DialogueManager script to start the conversation with the chosen NPC
	public void TriggerDialogue() {
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
}
