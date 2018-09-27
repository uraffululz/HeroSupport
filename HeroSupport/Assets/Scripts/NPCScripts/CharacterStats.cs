using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public Stat strength;
	public Stat agility;
	public Stat intellect;

	public int maxFatigue = 100;
	public int currentFatigue { get; private set; }

	public int maxStress = 100;
	public int currentStress {get; private set; }


	private void Awake() {
		currentFatigue = maxFatigue;
		currentStress = maxStress;
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
		currentFatigue -= fatigueDamage;
		Debug.Log(transform.name + "'s FATIGUE reduced by " + fatigueDamage);

		//If the Hero's fatigue is drained, he becomes physically exhausted
		if (currentFatigue <= 0) {
			currentFatigue = 0;
			PhysicallyExhausted();
		}
	}

	public void DamageStress(int stressDamage) {
		//Use intellect to resist incoming stress damage
		stressDamage -= intellect.GetValue();
		// \/Keeps Hero from GAINING stress during activities. May not be necessary, as he could definitely come home feeling good about his accomplishments
		//stressDamage = Mathf.Clamp(stressDamage, 0, int.MaxValue);

		//Damage the Hero's stress
		currentStress -= stressDamage;
		Debug.Log(transform.name + "'s STRESS reduced by " + stressDamage);

		//If the Hero's stress is drained, he becomes mentally exhausted
		if (currentStress <= 0) {
			currentStress = 0;
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
