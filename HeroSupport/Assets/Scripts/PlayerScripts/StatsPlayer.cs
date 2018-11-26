using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayer : MonoBehaviour {
	[SerializeField] ObjectPlayer playerObject;

//Functional Stat Variables (for calling/comparing via other scripts)
	public static int valueStr { get; private set; }
	public static int valueAgi { get; private set; }
	public static int valueInt { get; private set; }
	static int baseStr;
	static int baseAgi;
	static int baseInt;

	int minFatigue = 0;
	static int baseMaxFatigue;
	public static int maxFatigue { get; private set; }

	int minStress = 0;
	static int baseMaxStress;
	public static int maxStress { get; private set; }

	static int baseDmg;
	public static int damage { get; private set; }

	public Object primaryAttack { get; private set; }
	public Object secondaryAbility { get; private set; }


//Persistent, Updateable Stats (for changing via other scripts)
	public static int alteredStr;
	public static int alteredAgi;
	public static int alteredInt;

	public static int notoriety;

	public static int fatigueMaxUp;
	public static int currentFatigue { get; private set; }
	public static bool isFullyFatigued { get; private set; }

	public static int stressMaxUp;
	public static int currentStress { get; private set; }
	public static bool isFullyStressed { get; private set; }

	public static int alteredDmg;


	void Awake () {
		InitializeStats();
		UpdateStats();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.G)) {
			alteredDmg += 10;
			UpdateStats();
			Debug.Log(damage);
		}
	}

	void InitializeStats() {
		GetComponent<MeshFilter>().mesh = playerObject.mesh;
		GetComponent<Animator>().runtimeAnimatorController = playerObject.animator;

		baseStr = playerObject.strength.GetValue();
		baseAgi = playerObject.agility.GetValue();
		baseInt = playerObject.intellect.GetValue();

		baseMaxFatigue = playerObject.fatigueMax;
		//currentFatigue = maxFatigue;
		baseMaxStress = playerObject.StressMax;
		//currentStress = minStress;
		//isFullyFatigued = false;
		//isFullyStressed = false;

		baseDmg = playerObject.attackDmg;
	}


	public static void UpdateStats() {
//Call this function if I need to update/re-evaluate variable values (especially mid-scene)
		valueStr = baseStr + alteredStr;
		valueAgi = baseAgi + alteredAgi;
		valueInt = baseInt + alteredInt;

		maxFatigue = baseMaxFatigue + fatigueMaxUp;
		maxStress = baseMaxStress + stressMaxUp;

		damage = baseDmg + (valueStr * 2) + alteredDmg;

		//Debug.Log("Str: " + valueStr + " Agi: " + valueAgi + " Int: " + valueInt);
		//Debug.Log("Fatigue: " + currentFatigue + " Stress: " + currentStress);
	}


	public static void DamageFatigue(int fatigueDamage) {
		//Use strength to resist incoming fatigue damage
		fatigueDamage -= valueStr;
// TOMAYBEDO \/Keeps Hero from REDUCING fatigue during activities. May not be necessary, as he could definitely come home feeling good about his accomplishments
		//fatigueDamage = Mathf.Clamp(fatigueDamage, 0, int.MaxValue);

		//Damage the Hero's fatigue
		currentFatigue += fatigueDamage;

		//If the Hero's fatigue is drained, he becomes physically exhausted
		if (currentFatigue >= maxFatigue) {
//TOMAYBEDO If the character's fatigue reaches its max, maybe he suffers an EXTREME setback
//(Long-term/permanent injury, ability stat point loss, etc)
//If he is the Sidekick, maybe he quits the hero game and leaves
			currentFatigue = maxFatigue;
			PhysicallyExhausted();
		}
		else if (currentFatigue < 0) {
			currentFatigue = 0;
			isFullyFatigued = false;
		}
		else {
			isFullyFatigued = false;
		}
		Debug.Log("Hero fatigue:" + currentFatigue);
	}


	public static void DamageStress(int stressDamage) {
		//Use intellect to resist incoming stress damage
		stressDamage -= valueInt;
// TOMAYBEDO \/Keeps Hero from REDUCING stress during activities. May not be necessary, as he could definitely come home feeling good about his accomplishments
		//stressDamage = Mathf.Clamp(stressDamage, 0, int.MaxValue);

		//Damage the Hero's stress
		currentStress += stressDamage;

		//If the Hero's stress is drained, he becomes mentally exhausted
		if (currentStress >= maxStress) {
//TOMAYBEDO If the character's stress reaches its max, maybe he suffers an EXTREME setback
//(PTSD, Long-term/permanent mental problems, ability stat point loss, etc)
//If he is the Sidekick, maybe he quits the hero game and leaves
			currentStress = maxStress;
			MentallyExhausted();
		}
		else if (currentStress < 0) {
			currentStress = 0;
			isFullyStressed = false;
		}
		else {
			isFullyStressed = false;
		}
		Debug.Log("Hero stress:" + currentStress);
	}


	public static /*virtual*/ void PhysicallyExhausted() {
		Debug.Log("You are PHYSICALLY EXHAUSTED");
		isFullyFatigued = true;
	}


	public static /*virtual*/ void MentallyExhausted() {
		Debug.Log("You are MENTALLY EXHAUSTED");
		isFullyStressed = true;
	}

}
