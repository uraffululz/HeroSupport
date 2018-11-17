using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "New Entity Object/New Hero")]
public class ObjectPlayer : ScriptableObject {

	public Mesh mesh;
	public RuntimeAnimatorController animator;

	public Stat strength;
	public Stat agility;
	public Stat intellect;

//TOMAYBEDO Need a separate "Notoriety" variable for each hero? Are the sidekicks using this same object?
//If so, they may need the stat if they are going to participate in activities alone.
//Alternatively, Notoriety could be a global stat/variable, used by all heroes

	public int fatigueMax;
	public int StressMax;

	public int attackDmg;

	public Object primaryAttackScript;
	public Object secondaryAbilityScript;

}
