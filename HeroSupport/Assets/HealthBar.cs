using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	//GameObject player;
	//StatsPlayer playerStats;

	[SerializeField] Image currentFatigueBar;
	[SerializeField] Text fatigueRatioText;
	

	void Start () {
		//player = GameObject.FindWithTag("Player");
		//playerStats = player.GetComponent<StatsPlayer>();
		UpdateFatigueBar();
	}


	void Update () {
		
	}

	public void UpdateFatigueBar () {
		float fatigueRatio = (float) StatsPlayer.currentFatigue / StatsPlayer.maxFatigue;

		//Debug.Log("Current fatigue: " + StatsPlayer.currentFatigue + "/ Max fatigue: " + StatsPlayer.maxFatigue);
		//Debug.Log("Fatigue Ratio: " + fatigueRatio);

		currentFatigueBar.rectTransform.localScale = new Vector3(fatigueRatio, 1, 1);
		fatigueRatioText.text = (fatigueRatio * 100).ToString("0") + "%";
	}
}
