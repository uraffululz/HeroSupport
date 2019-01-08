using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerDisplay : MonoBehaviour {
	ActivityParticipation actPart;

	//[SerializeField] DayNightCycle lightCycle;
	//[SerializeField] GameObject hero;

	public Animator compAnim;

	//public Text heroStatus;
	public Text heroVitals;
	public Text gangName;
	public Text activity;
	//public Text activity2;

	[SerializeField] Toggle selectorNightOff;
	[SerializeField] Toggle selector1;
	//public Toggle selector2;
	public Button commenceButton;
	public Button backButton;

	bool highTierHappening = false;


	void Start () {
		actPart = GetComponent<ActivityParticipation>();
		compAnim = gameObject.GetComponent<Animator>();

		//hero = GameObject.FindGameObjectWithTag("Player");
	}


	void Update () {
		if (compAnim.GetBool("compActivated") == true) {
			UpdateCompUI();
		}

		if (compAnim.GetBool("compActivated") && Input.GetKeyDown(KeyCode.E)) {
			CloseDisplay();
		}
		else if (!compAnim.GetBool("compActivated") && Input.GetKeyDown(KeyCode.E)) {
			OpenCompDisplay();
		}
	}

	public void UpdateCompUI() {
			//These text components display the Hero's current stats and vitals
			//TODO Since I'm ACTIVELY playing as the Hero now, I don't really need this \/. However, I could use something similar for the sidekick
			//heroStatus.text = "Hero Status: " + hero.GetComponent<NPCHero>().myActiveState.ToString();

			heroVitals.text = "Hero Vitals: " + "S " + StatsPlayer.valueStr.ToString() + ", " +
				"A " + StatsPlayer.valueAgi.ToString() + ", " +
				"I " + StatsPlayer.valueInt.ToString() + ", " +
				"Fa " + StatsPlayer.currentFatigue.ToString() + ", " +
				"St " + StatsPlayer.currentStress.ToString();

			//Disable the Commence button if the hero is taking the night off
			if (selectorNightOff.isOn) {
				commenceButton.interactable = false;
			}
			else {
				commenceButton.interactable = true;
			}

			//These text components display the various activities available during the current night
			//TODO This doesn't seem to be updating while the player is standing at the location, even when a high-tier activity appears there. Find out why.
			if (highTierHappening != NightHighTierManager.isHighTierActivityHere) {
				highTierHappening = NightHighTierManager.isHighTierActivityHere;
				if (selector1.isOn) {
					selector1.isOn = false;
				}
			}

			if (NightHighTierManager.isEvent) {
				activity.text = ("Stop the " + NightHighTierManager.activity + NightHighTierManager.crimeStars);
				activity.color = Color.green;
			}
			else if (highTierHappening) {
				gangName.text = ("Gang: " + NightHighTierManager.gangInvolved);
				activity.text = ("Stop the " + NightHighTierManager.activity + NightHighTierManager.crimeStars);
				activity.color = Color.yellow;
			}
			else {
				gangName.text = ("Gang: " + NightManager.gangInvolved);
				activity.text = ("Stop the " + NightManager.activity + NightManager.crimeStars);
				activity.color = Color.white;
			}
	}

	public void OpenCompDisplay() {
		compAnim.SetBool("compActivated", true);
		actPart.SetTogglesToDefault();

		if (ClueMaster.eventOngoing && !ClueMaster.eventUncovered) {
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
