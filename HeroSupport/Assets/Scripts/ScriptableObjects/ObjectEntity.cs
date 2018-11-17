using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "New Entity Object/New Entity (General)")]
public class ObjectEntity : ScriptableObject {


	public Mesh mesh;

	public int attackDmg;

	public Object primaryAttackScript;
	public Object secondaryAbilityScript;

}
