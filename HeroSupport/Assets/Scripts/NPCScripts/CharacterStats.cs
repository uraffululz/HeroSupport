using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public Stat strength;
	public Stat agility;
	public Stat intellect;

	public int minFatigue = 0;
	public int maxFatigue = 100;
	public int currentFatigue { get; private set; }

	public int minStress = 0;
	public int maxStress = 100;
	public int currentStress {get; private set; }

/*TOMAYBEDO Perhaps the Sidekick(s) get fatigued/stressed faster than the Hero, in which case:
	public float fatigueRate;
	public float stressRate;
*/


	private void Awake() {
		currentFatigue = minFatigue;
		currentStress = minStress;
	}

	private void Update() {
/*FOR TESTING PURPOSES
		if (Input.GetKeyDown(KeyCode.T)) {
			DamageFatigue(20);
		}
		if (Input.GetKeyDown(KeyCode.G)) {
			DamageStress(20);
		}
*/
	}

	public void DamageFatigue (int fatigueDamage) {
		//Use strength to resist incoming fatigue damage
		fatigueDamage -= strength.GetValue();
		// \/Keeps Hero from GAINING fatigue during activities. May not be necessary, as he could definitely come home feeling good about his accomplishments
		//fatigueDamage = Mathf.Clamp(fatigueDamage, 0, int.MaxValue);

		//Damage the Hero's fatigue
		currentFatigue += fatigueDamage;
		Debug.Log(transform.name + "'s FATIGUE reduced by " + fatigueDamage);

		//If the Hero's fatigue is drained, he becomes physically exhausted
		if (currentFatigue >= maxFatigue) {
			currentFatigue = maxFatigue;
			PhysicallyExhausted();
		}
	}

	public void DamageStress(int stressDamage) {
		//Use intellect to resist incoming stress damage
		stressDamage -= intellect.GetValue();
		// \/Keeps Hero from GAINING stress during activities. May not be necessary, as he could definitely come home feeling good about his accomplishments
		//stressDamage = Mathf.Clamp(stressDamage, 0, int.MaxValue);

		//Damage the Hero's stress
		currentStress += stressDamage;
		Debug.Log(transform.name + "'s STRESS reduced by " + stressDamage);

		//If the Hero's stress is drained, he becomes mentally exhausted
		if (currentStress >= maxStress) {
			currentStress = maxStress;
			MentallyExhausted();
		}
	}

//TODO Why "virtual"?
	//The Hero becomes physically exhausted
	public virtual void PhysicallyExhausted() {
		//This method is meant to be overwritten
		Debug.Log(transform.name + " is PHYSICALLY EXHAUSTED");
	}

//TODO Why "virtual"?
	//The Hero becomes mentally exhausted
	public virtual void MentallyExhausted() {
		//This method is meant to be overwritten
		Debug.Log(transform.name + " is MENTALLY EXHAUSTED");
	}

}
