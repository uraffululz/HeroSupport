using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityParticipation : MonoBehaviour {

	[SerializeField] GameObject hero;
	[SerializeField] GameObject sidekick;

	public GameObject activityText1;
	public GameObject activityText2;
//	public GameObject activityText3;

	public Toggle heroToggle1;
	public Toggle sideToggle1;
	public Toggle heroToggle2;
	public Toggle sideToggle2;
//	public Toggle heroToggle3;
//	public Toggle sideToggle3;
	public Toggle heroToggleNightOff;
	public Toggle sideToggleNightOff;


	public void Start() {
		hero = GameObject.FindWithTag("Player");
		sidekick = GameObject.FindWithTag("Sidekick");
	}


	public void SetTogglesToDefault() {
		heroToggle1.isOn = false;
		sideToggle1.isOn = false;
		heroToggle2.isOn = false;
		sideToggle2.isOn = false;
//		heroToggle3.isOn = false;
//		sideToggle3.isOn = false;
		heroToggleNightOff.isOn = true;
		sideToggleNightOff.isOn = true;
	}


	public void HeroParticipatingInActivity1 (bool isHeroParticipating1) {
		//If the character is fully fatigued or fully stressed, do not allow this toggle to be turned on
		if (hero.GetComponent<StatsPlayer>().isFullyFatigued || hero.GetComponent<StatsPlayer>().isFullyStressed) {
			isHeroParticipating1 = false;
			Debug.Log(hero.name + " unable to participate in activity.");
		}
		if (!isHeroParticipating1) {
			heroToggle1.isOn = false;
			if (!heroToggle2.isOn /* && !toggle3.isOn */) {
				HeroTakingNightOff(true);
			}
		}
		//Debug.Log(transform.name + " is participating in activity 1? " + isParticipating1);
	}


	public void SideParticipatingInActivity1 (bool isSideParticipating1) {
		if (sidekick.GetComponent<StatsPlayer>().isFullyFatigued || sidekick.GetComponent<StatsPlayer>().isFullyStressed) {
			isSideParticipating1 = false;
			Debug.Log(sidekick.name + " unable to participate in activity.");
		}
		if (!isSideParticipating1) {
			sideToggle1.isOn = false;
			if (!sideToggle2.isOn /* && !toggle3.isOn */) {
				SideTakingNightOff(true);
			}
		}
	}


	public void HeroParticipatingInActivity2 (bool isParticipating2) {
		//If the character (this script is on) is fully fatigued or fully stressed, do not allow this toggle to be turned on
		if (hero.GetComponent<StatsPlayer>().isFullyFatigued || hero.GetComponent<StatsPlayer>().isFullyStressed) {
			isParticipating2 = false;
			Debug.Log(hero.name + " unable to participate in activity.");
		}

		if (!isParticipating2) {
			heroToggle2.isOn = false;
			if (!heroToggle1.isOn /* && !toggle3.isOn */) {
				HeroTakingNightOff(true);
			}
		}
		//Debug.Log(transform.name + " is participating in activity 2? " + isParticipating2);
	}


	public void SideParticipatingInActivity2(bool isSideParticipating2) {
		//If the character (this script is on) is fully fatigued or fully stressed, do not allow this toggle to be turned on
		if (sidekick.GetComponent<StatsPlayer>().isFullyFatigued || sidekick.GetComponent<StatsPlayer>().isFullyStressed) {
			isSideParticipating2 = false;
			Debug.Log(sidekick.name + " unable to participate in activity.");
		}

		if (!isSideParticipating2) {
			sideToggle2.isOn = false;
			if (!sideToggle1.isOn /* && !toggle3.isOn */) {
				SideTakingNightOff(true);
			}
		}
		//Debug.Log(transform.name + " is participating in activity 2? " + isParticipating2);
	}


	/*
	public void ParticipatingInActivity3 (bool isParticipating3) {
		//If the character (this script is on) is fully fatigued or fully stressed, do not allow this toggle to be turned on
		if (gameObject.GetComponent<StatsPlayer>().isFullyFatigued || gameObject.GetComponent<StatsPlayer>().isFullyStressed) {
			isParticipating3 = false;
			Debug.Log(gameObject.name + " unable to participate in activity.");
		}

		if (isParticipating3) {
			TakingNightOff(false);
			toggle3.isOn = true;
		} else {
			toggle3.isOn = false;
			if (!toggle1.isOn && !toggle2.isOn) {
				TakingNightOff(true);
			}
		}
		//Debug.Log(transform.name + " is participating in activity 3? " + isParticipating3);
	}
*/


	public void HeroTakingNightOff (bool isHeroTakingNightOff) {
		if (isHeroTakingNightOff) {
			heroToggleNightOff.isOn = true;
			//TODO Do not allow the player to toggle this box if it is on

			if (heroToggle1.isOn) {
				HeroParticipatingInActivity1(false);
			}
			if (heroToggle2.isOn) {
				HeroParticipatingInActivity2(false);
			}
/*			if (toggle3.isOn) {
				ParticipatingInActivity3(false);
			}
*/		} else {
			heroToggleNightOff.isOn = false;
			//If all other toggles are OFF, this one remains ON
			if (!heroToggle1.isOn && !heroToggle2.isOn /* && !toggle3.isOn */) {
				heroToggleNightOff.isOn = true;
			}
		}
	}


	public void SideTakingNightOff(bool isSideTakingNightOff) {
		if (isSideTakingNightOff) {
			sideToggleNightOff.isOn = true;
			//TODO Do not allow the player to toggle this box if it is on

			if (sideToggle1.isOn) {
				SideParticipatingInActivity1(false);
			}
			if (sideToggle2.isOn) {
				SideParticipatingInActivity2(false);
			}
			/*			if (toggle3.isOn) {
							ParticipatingInActivity3(false);
						}
			*/
		}
		else {
			sideToggleNightOff.isOn = false;
			//If all other toggles are OFF, this one remains ON
			if (!sideToggle1.isOn && !sideToggle2.isOn /* && !toggle3.isOn */) {
				sideToggleNightOff.isOn = true;
			}
		}
	}
}
