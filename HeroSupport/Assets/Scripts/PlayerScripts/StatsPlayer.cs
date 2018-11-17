using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayer : MonoBehaviour {

	public ObjectPlayer playerObject;

	public int valueStr;
	public int valueAgi;
	public int valueInt;

	public static int notoriety = 0;

	int minFatigue = 0;
	public int maxFatigue;
	public int currentFatigue { get; private set; }
	public bool isFullyFatigued { get; private set; }

	int minStress = 0;
	public int maxStress;
	public int currentStress { get; private set; }
	public bool isFullyStressed { get; private set; }

	public int damage;

	public Object primaryAttack;
	public Object secondaryAbility;


	void Awake () {
		GetComponent<MeshFilter>().mesh = playerObject.mesh;
		GetComponent<Animator>().runtimeAnimatorController = playerObject.animator;

		valueStr = playerObject.strength.GetValue();
		valueAgi = playerObject.agility.GetValue();
		valueInt = playerObject.intellect.GetValue();

		maxFatigue = playerObject.fatigueMax;
		maxStress = playerObject.StressMax;
		currentFatigue = minFatigue;
		currentStress = minStress;
		isFullyFatigued = false;
		isFullyStressed = false;

		damage = playerObject.attackDmg;

	}


	public void DamageFatigue(int fatigueDamage) {
		//Use strength to resist incoming fatigue damage
		fatigueDamage -= valueStr;
// TOMAYBEDO \/Keeps Hero from REDUCING fatigue during activities. May not be necessary, as he could definitely come home feeling good about his accomplishments
		//fatigueDamage = Mathf.Clamp(fatigueDamage, 0, int.MaxValue);

		//Damage the Hero's fatigue
		currentFatigue += fatigueDamage;

		//If the Hero's fatigue is drained, he becomes physically exhausted
		if (currentFatigue > (maxFatigue * .8)) {
			if (currentFatigue >= maxFatigue) {
//TOMAYBEDO If the character's fatigue reaches its max, maybe he suffers an EXTREME setback
//(Long-term/permanent injury, ability stat point loss, etc)
//If he is the Sidekick, maybe he quits the hero game and leaves
				currentFatigue = maxFatigue;
			}
			PhysicallyExhausted();
		}
		else if (currentFatigue < 0) {
			currentFatigue = 0;
		}
		Debug.Log("Hero fatigue:" + currentFatigue);
	}


	public void DamageStress(int stressDamage) {
		//Use intellect to resist incoming stress damage
		stressDamage -= valueInt;
// TOMAYBEDO \/Keeps Hero from REDUCING stress during activities. May not be necessary, as he could definitely come home feeling good about his accomplishments
		//stressDamage = Mathf.Clamp(stressDamage, 0, int.MaxValue);

		//Damage the Hero's stress
		currentStress += stressDamage;

		//If the Hero's stress is drained, he becomes mentally exhausted
		if (currentStress > (maxStress * .8)) {
			if (currentStress >= maxStress) {
//TOMAYBEDO If the character's stress reaches its max, maybe he suffers an EXTREME setback
//(PTSD, Long-term/permanent mental problems, ability stat point loss, etc)
//If he is the Sidekick, maybe he quits the hero game and leaves
				currentStress = maxStress;
			}
			MentallyExhausted();
		}
		else if (currentStress < 0) {
			currentStress = 0;
		}
		Debug.Log("Hero stress:" + currentStress);
	}

//TODO Why "virtual"? Consult Brackeys "RPG creator stats video"
	//The Hero becomes physically exhausted
//TODO In this state, the character cannot participate in activities. Any activities they are still assigned to attempt during the night are cancelled
	public virtual void PhysicallyExhausted() {
		Debug.Log(transform.name + " is PHYSICALLY EXHAUSTED");
		isFullyFatigued = true;
		GetComponent<PlayerMove>().myActiveState = PlayerMove.activityStates.Fatigued;
	}

//TODO Why "virtual"? Consult Brackeys "RPG creator stats video"
	//The Hero becomes mentally exhausted
//TODO In this state, the character cannot participate in activities. Any activities they are still assigned to attempt during the night are cancelled
	public virtual void MentallyExhausted() {
		Debug.Log(transform.name + " is MENTALLY EXHAUSTED");
		isFullyStressed = true;
		GetComponent<PlayerMove>().myActiveState = PlayerMove.activityStates.Stressed;
	}

}
