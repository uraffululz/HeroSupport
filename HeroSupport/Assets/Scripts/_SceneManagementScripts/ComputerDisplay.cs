﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerDisplay : MonoBehaviour {
	ActivityParticipation actPart;

	[SerializeField] DayNightCycle lightCycle;
	[SerializeField] GameObject hero;
	[SerializeField] StatsPlayer heroStats;
	Animator compAnim;

	//public Text heroStatus;
	public Text heroVitals;
	public Text activity;
	public Text activity2;
	public Text activity3;

	//public Toggle selector1;
	//public Toggle selector2;
	//public Toggle selector3;
	public Button commenceButton;
	public Button backButton;


	void Start () {
		actPart = GetComponent<ActivityParticipation>();
		compAnim = gameObject.GetComponent<Animator>();

		hero = GameObject.FindGameObjectWithTag("Player");
		heroStats = hero.GetComponent<StatsPlayer>();
	}


	void Update () {
		if (compAnim.GetBool("compActivated") == true) {
			//These text components display the Hero's current stats and vitals
//TODO Since I'm ACTIVELY playing as the Hero now, I don't really need this. However, I could use something similar for the sidekick
			//heroStatus.text = "Hero Status: " + hero.GetComponent<NPCHero>().myActiveState.ToString();

			heroVitals.text = "Hero Vitals: " + "S " + StatsPlayer.valueStr.ToString() + ", " + 
				"A " + StatsPlayer.valueAgi.ToString() +  ", " +
				"I " + StatsPlayer.valueInt.ToString() + ", " +
				"Fa " + StatsPlayer.currentFatigue.ToString() + ", " +
				"St " + StatsPlayer.currentStress.ToString();

			//These text components display the various activities available during the current night
			if (NightHighTierManager.activity != null && NightHighTierManager.activity != "Blank") {
				activity.text = ("Activity 1: Stop the " + NightHighTierManager.activity + NightHighTierManager.crimeStars);
			}
			else {
				activity.text = ("Activity 1: Stop the " + NightManager.activity + NightManager.crimeStars);
			}
			//activity2.text = ("Activity 2: " + NightManager.activity2 + NightManager.crimeStars2);
			//activity3.text = ("Activity 3: " + NightManager.activity3 + NightManager.crimeStars3);

			//TODO If the hero and sidekick's "Taking night off" toggles are BOTH on, disable the commenceButton
		}
	}


	public void OpenCompDisplay() {
		GetComponent<Animator>().SetBool("compActivated", true);
		actPart.SetTogglesToDefault();

		if (ClueMaster.eventOngoing) {
			actPart.activityText2.SetActive(true);
		}
		else {
			actPart.activityText2.SetActive(false);
		}
	}


	//Close the display box and return it to its "closed" position, offscreen
	public void CloseDisplay() {
		compAnim.SetBool("compActivated", false);
	}
}
