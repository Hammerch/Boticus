using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public GameObject target;
	public float coolDown;
	public float attackTimer;
	private string[] attacks = new string[5] {"heavy", "light", "kick", "jump", "special"};
	
	// Use this for initialization
	void Start () {
		coolDown = 2.0f;
		attackTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(attackTimer>0){
			attackTimer-=Time.deltaTime;
		}
		if(attackTimer<0){
			attackTimer = 0;
		}
		
		if(attackTimer==0){
			Attack(randomAttack());
			attackTimer = coolDown;
		}
	}
	
	private string randomAttack(){
		return attacks[Random.Range(0, attacks.Length)];	
	}
	
	private void Attack(string attack){
		playerHealth ph = (playerHealth)target.GetComponent("playerHealth");
		
		// was there collision between weapon and target?
			// determine attack strength (power*attack type value*(weapon current/weapon max))
			// was blocked?
				// not blocked -- is armor?
					// yes armor -- Do armor damage (armor value - attack strength)
						// is there more damage than armor can prevent
							// yes - pass through remainder and change armor model to wrecked
							// no - return
					// no armor -- do target damage - return
				// yes blocked -- is shield enough strength
					// No -- damage shield - pass through damage to armor check
					// yes -- return
		
		ph.AdjustCurrentHealth(-20);
	}
}
