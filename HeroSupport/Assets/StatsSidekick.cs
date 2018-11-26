using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSidekick : MonoBehaviour {

	//public static StatsSidekick instance;

	[SerializeField] ObjectSidekick sideObject;

	public static int valueStr;
	public static int valueAgi;
	public static int valueInt;

	static int minFatigue = 0;
	public static int maxFatigue;
	public static int currentFatigue { get; private set; }
	public static bool isFullyFatigued { get; private set; }

	static int minStress = 0;
	public static int maxStress;
	public static int currentStress { get; private set; }
	public static bool isFullyStressed { get; private set; }

	public static int damage;

	[SerializeField] Object primaryAttack;
	[SerializeField] Object secondaryAbility;


	void Awake () {
/*		DontDestroyOnLoad(gameObject);

		if (instance != null && instance != this) {
			Destroy(gameObject);
		}
		else {
			instance = this;
		}
*/
		GetComponent<MeshFilter>().mesh = sideObject.mesh;
		GetComponent<Animator>().runtimeAnimatorController = sideObject.animator;

		valueStr = sideObject.strength.GetValue();
		valueAgi = sideObject.agility.GetValue();
		valueInt = sideObject.intellect.GetValue();

		maxFatigue = sideObject.fatigueMax;
		maxStress = sideObject.StressMax;
		currentFatigue = minFatigue;
		currentStress = minStress;
		isFullyFatigued = false;
		isFullyStressed = false;

		damage = sideObject.attackDmg;

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
