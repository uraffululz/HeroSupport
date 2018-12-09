using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Location Object", menuName = "New Map Object/New Location Object")]
public class ObjectLocation : ScriptableObject {

	public string gangInCharge;

	public string[] landmarks;

	public int lm1HPBonus;
	public int lm1FPBonus;

	public int lm2HPBonus;
	public int lm2FPBonus;
}
