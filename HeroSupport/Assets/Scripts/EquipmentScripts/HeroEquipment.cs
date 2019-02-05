using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroEquipment : MonoBehaviour {

	public static GameObject equippedGadget;
	public GameObject myGadget;
	public static GameObject equippedAmmo;

	public Transform holster;
	public Transform equipRightHand;


	void Start () {
		if (equippedGadget != null && holster.childCount == 0) {
			myGadget = Instantiate(equippedGadget, holster.position, holster.rotation, holster);
		}
		
	}


	void Update () {
		
	}
}
