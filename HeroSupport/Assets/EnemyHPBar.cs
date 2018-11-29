using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour {

	public Transform myEnemy;

	Vector2 posCorrection = new Vector2(0, 100);
	RectTransform targetCanvas;
	RectTransform HPBarTransform;

	StatsEnemy enemyStats;
	//EnemyActivity enemyAct;

	[SerializeField] Image currentHPBar;
	[SerializeField] Text HPRatioText;


	void Start () {
		enemyStats = myEnemy.GetComponent<StatsEnemy>();
		//enemyAct = GetComponentInParent<EnemyActivity>();

		InitializeHPBar();
		UpdateHPBar();
	}


	void LateUpdate() {
		if (myEnemy != null) {
			RepositionHPBar();
		}
		else {
			gameObject.SetActive(false);
		}
	}


	void InitializeHPBar () {
		targetCanvas = GetComponentInParent<RectTransform>();
		HPBarTransform = GetComponent<RectTransform>();
	}


	public void UpdateHPBar () {
		if (enemyStats != null) {
			float HPRatio = (float) enemyStats.currentHP / enemyStats.maxHP;

			currentHPBar.rectTransform.localScale = new Vector3(HPRatio, 1, 1);
			HPRatioText.text = (HPRatio * 100).ToString("0") + "%";

			Debug.Log("Enemy HP: " + enemyStats.currentHP + " / " + enemyStats.maxHP + " | Ratio: " + HPRatio);
		}
	}

//TODO This doesn't work as intended. Get back to work on it
	//Position the enemy's HP bar over their head
	void RepositionHPBar () {
		Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(myEnemy.position);
				Vector2 worldObject_ScreenPos = new Vector2(
					((ViewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * .5f)),
					((ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * .5f)));
		
		//Vector2 worldObject_ScreenPos = new Vector2(ViewportPosition.x * (targetCanvas.sizeDelta.x * 5), ViewportPosition.y * (targetCanvas.sizeDelta.y * 5));
		HPBarTransform.anchoredPosition3D = worldObject_ScreenPos;
	}
}
