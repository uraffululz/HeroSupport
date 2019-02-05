using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideoutManager : MonoBehaviour {
	[SerializeField] Image nightlyOverviewUI;

	GameObject player;
	GameObject sidekick;


	void Awake () {
		player = GameObject.FindWithTag("Player");
		sidekick = GameObject.FindWithTag("Sidekick");

		if (player != null) {
			player.transform.position = Vector3.zero;
			player.GetComponent<HeroActivity>().enabled = false;
			player.GetComponent<HeroAttack>().enabled = false;
			player.GetComponent<PlayerInteracting>().enabled = true;
		}

		if (sidekick != null) {
			sidekick.transform.position = Vector3.right;
		}


		nightlyOverviewUI.GetComponent<Animator>().SetBool("compActivated", true);
	}


	void Update () {
		
	}


	public void CloseNightlyOverviewUI() {
		nightlyOverviewUI.GetComponent<Animator>().SetBool("compActivated", false);
	}
}
