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
		// \/Keeps Hero from REDUCING fatigue during activities. May not be necessary, as he could definitely come home feeling good about his accomplishments
		//fatigueDamage = Mathf.Clamp(fatigueDamage, 0, int.MaxValue);

		//Damage the Hero's fatigue
		currentFatigue += fatigueDamage;
		Debug.Log(transform.name + "'s FATIGUE increased by " + fatigueDamage);

		//If the Hero's fatigue is drained, he becomes physically exhausted
		if (currentFatigue > (maxFatigue * .7)) {
			if (currentFatigue > maxFatigue) {
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
	}

	public void DamageStress(int stressDamage) {
		//Use intellect to resist incoming stress damage
		stressDamage -= intellect.GetValue();
		// \/Keeps Hero from REDUCING stress during activities. May not be necessary, as he could definitely come home feeling good about his accomplishments
		//stressDamage = Mathf.Clamp(stressDamage, 0, int.MaxValue);

		//Damage the Hero's stress
		currentStress += stressDamage;
		Debug.Log(transform.name + "'s STRESS increased by " + stressDamage);

		//If the Hero's stress is drained, he becomes mentally exhausted
		if (currentStress > (maxStress * .7)) {
			if (currentStress > maxStress) {
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
	}

//TODO Why "virtual"?
	//The Hero becomes physically exhausted
//TODO In this state, the character cannot participate in activities. Any activities they are still assigned to attempt during the night are cancelled
	public virtual void PhysicallyExhausted() {
		Debug.Log(transform.name + " is PHYSICALLY EXHAUSTED");
	}

//TODO Why "virtual"?
	//The Hero becomes mentally exhausted
//TODO In this state, the character cannot participate in activities. Any activities they are still assigned to attempt during the night are cancelled
	public virtual void MentallyExhausted() {
		Debug.Log(transform.name + " is MENTALLY EXHAUSTED");
	}

}
