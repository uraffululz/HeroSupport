using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CivHPBar : MonoBehaviour {

	public Transform myCiv;

	//Vector2 posCorrection = new Vector2(0, 100);
	RectTransform targetCanvas;
	//RectTransform HPBarTransform;

	StatsCivilian civStats;

	[SerializeField] Image currentHPBar;
	[SerializeField] Text HPRatioText;


	void Start() {
		civStats = myCiv.GetComponent<StatsCivilian>();

		InitializeHPBar();
		UpdateHPBar();
	}


	void LateUpdate() {
		if (myCiv != null) {
			//RepositionHPBar();
			Vector3 HPBarLookTarget = new Vector3(myCiv.transform.position.x, targetCanvas.transform.position.y, 100);
			targetCanvas.LookAt(HPBarLookTarget, transform.up);
		}
		else {
			gameObject.SetActive(false);
		}
	}


	void InitializeHPBar() {
		targetCanvas = GetComponentInParent<RectTransform>();
		//HPBarTransform = GetComponent<RectTransform>();
	}


	public void UpdateHPBar() {
		if (civStats != null) {
			float HPRatio = (float)civStats.currentHP / civStats.maxHP;

			currentHPBar.rectTransform.localScale = new Vector3(HPRatio, 1, 1);
			HPRatioText.text = (HPRatio * 100).ToString("0") + "%";

			//Debug.Log("Enemy HP: " + enemyStats.currentHP + " / " + enemyStats.maxHP + " | Ratio: " + HPRatio);
		}
	}

////TODO This doesn't work as intended. Get back to work on it
	////Position the enemy's HP bar over their head
	//void RepositionHPBar() {
	//	Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(myCiv.position);
	//	Vector2 worldObject_ScreenPos = new Vector2(
	//		((ViewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * .5f)),
	//		((ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * .5f)));

	//	//Vector2 worldObject_ScreenPos = new Vector2(ViewportPosition.x * (targetCanvas.sizeDelta.x * 5), ViewportPosition.y * (targetCanvas.sizeDelta.y * 5));
	//	HPBarTransform.anchoredPosition3D = worldObject_ScreenPos;
	//}
}
