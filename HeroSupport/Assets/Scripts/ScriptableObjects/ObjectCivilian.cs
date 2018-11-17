using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "New Entity Object/New Civilian")]
public class ObjectCivilian : ScriptableObject {

	public Mesh mesh;

	public int health;
	public int focus; //Stamina/charge/power/MP/whatever
	public float focusRegenRate;

	public float moveSpeed;

	public int attackDmg;
	public float attackRate;
	public float meleeDist;
	public float rangedDist;

	public int notoriety;

	public Object primaryAttackScript;
	public Object secondaryAbilityScript;

}
