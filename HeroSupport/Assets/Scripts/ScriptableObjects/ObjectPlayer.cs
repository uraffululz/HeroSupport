using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "New Entity Object/New Hero")]
public class ObjectPlayer : ScriptableObject {

	public string heroName;

	public Mesh mesh;
	public RuntimeAnimatorController animator;

	public Stat strength;
	public Stat agility;
	public Stat intellect;

	public int fatigueMax;
	public int StressMax;

	public int attackDmg;

	public Object primaryAttackScript;
	public Object secondaryAbilityScript;

}
