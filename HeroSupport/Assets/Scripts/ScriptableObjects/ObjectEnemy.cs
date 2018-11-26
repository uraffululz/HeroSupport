using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a class for the GENERAL STATS possessed by all Entities/Creatures/GameObjects in the play scene
//Specialized scripts will be created later

[CreateAssetMenu(fileName = "New Entity", menuName = "New Entity Object/New Enemy")]
public class ObjectEnemy: ScriptableObject {

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
