using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	//Used to contain the NPC's dialogue options from the "sentences" array of Dialogue.cs
	//[Side note: A "Queue" is like a list, but it is a "FIFO" (First In, First Out), meaning it cycles through the array progressively,
	//removing the first object and "bumping" all other objects up in the "list" until the Queue is eventually empty]
	private Queue<string> sentences;

	//The NPC's name, as the text displayed on the DialogueBox gameobject
	public Text nameText;
	//The NPC's dialogue, as the text displayed on the DialogueBox gameobject
	public Text dialogueText;

	//The Animator component used to "slide" the DialogueBox into/out of view
	public Animator animator;

	void Start () {
		sentences = new Queue<string>();
	}

	//Begins the conversation with the NPC
	public void StartDialogue(Dialogue dialogue) {
		animator.SetBool("isOpen", true);
		nameText.text = dialogue.name;

		//Clears previously "unused" sentences from dialogue queue
		sentences.Clear();

		//Repopulates dialogue queue with sentences from the NPC's list
		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		//Displays the first sentence in the NPC's "sentences" array (from Dialogue.cs)
		DisplayNextSentence();
	}

	//Progresses through the NPC's dialogue options until their "sentences" array reaches its end, and then ends the conversation
	public void DisplayNextSentence(){
		if (sentences.Count == 0) {
			EndDialogue();
			return;
		}

		//Removes the current "sentence" from the NPC's "sentences" array, and moves the next "sentence" to the top of the array
		string sentence = sentences.Dequeue();

		//Ensures that multiple "TypeSentence" Coroutines are not called simultaneously, which could cause dialogue to become jumbled and illegible
		StopAllCoroutines();
		//Starts a new instance of the TypeSentence Coroutine
		StartCoroutine(TypeSentence(sentence));
	}

	//Types each sentence into the DialogueBox progressively, one letter at a time
	IEnumerator TypeSentence (string sentence) {
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			yield return null;
		}
	}

	//"Closes" DialogueBox when the conversation is over
	void EndDialogue() {
		animator.SetBool("isOpen", false);

		Debug.Log("End of conversation.");
	}
	
}
