using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerDisplay : MonoBehaviour {

	[SerializeField] DayNightCycle lightCycle;
	[SerializeField] GameObject hero;
	[SerializeField] CharacterStats heroStats;
	Animator compAnim;

	public Text heroStatus;
	public Text heroVitals;
	public Text activity1;
	public Text activity2;
	public Text activity3;

	public Toggle selector1;
	//public Toggle selector2;
	//public Toggle selector3;
	public Button commenceButton;
	public Button backButton;


	void Start () {
		compAnim = gameObject.GetComponent<Animator>();
	}


	void Update () {
		if (compAnim.GetBool("compActivated") == true) {
			//These text components display the Hero's current stats and vitals
			heroStatus.text = "Hero Status: " + hero.GetComponent<NPCHero>().myActiveState.ToString();
			heroVitals.text = "Hero Vitals: " + "S " + heroStats.strength.GetValue().ToString() + ", " + 
				"A " + heroStats.agility.GetValue().ToString() +  ", " +
				"I " + heroStats.intellect.GetValue().ToString() + ", " +
				"Fat " + heroStats.currentFatigue.ToString() + ", " +
				"Str " + heroStats.currentStress.ToString();
			
			//These text components display the various activities available during the current night
			activity1.text = ("Activity 1: Stop the " + NightManager.activity1 + NightManager.crimeStars1);
			//activity2.text = ("Activity 2: " + NightManager.activity2 + NightManager.crimeStars2);
			//activity3.text = ("Activity 3: " + NightManager.activity3 + NightManager.crimeStars3);

//TODO If the hero and sidekick's "Taking night off" toggles are BOTH on, disable the commenceButton
		}
	}


	//Close the display box and return it to its "closed" position, offscreen
	public void CloseDisplay() {
		compAnim.SetBool("compActivated", false);
	}
}
