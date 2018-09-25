using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerDisplay : MonoBehaviour {

	[SerializeField] DayNightCycle lightCycle;

	Animator compAnim;
	GameObject hero;

	public Text heroStatus;
	public Text heroVitals;
	public Text activity1;
	public Text activity2;
	public Text activity3;
	public Button backButton;


	void Start () {
		compAnim = gameObject.GetComponent<Animator>();
		hero = GameObject.Find("Hero");
	}


	void Update () {
		if (compAnim.GetBool("compActivated") == true) {
			heroStatus.text = "Hero Status: " + hero.GetComponent<NPCHero>().myActiveState.ToString();
			heroVitals.text = "Hero Vitals: " + "TBD";
			
			activity1.text = ("Activity 1: " + NightManager.activity1 + NightManager.crimeStars1);
			activity2.text = ("Activity 2: " + NightManager.activity2 + NightManager.crimeStars2);
			activity3.text = ("Activity 3: " + NightManager.activity3 + NightManager.crimeStars3);

		}
	}


	//Close the display box and return it to its "closed" position, offscreen
	public void CloseDisplay() {
		compAnim.SetBool("compActivated", false);
	}
}
