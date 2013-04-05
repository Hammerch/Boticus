using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public GameObject target;
	public float coolDown;
	public float attackTimer;
	
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
		
		// If user is attacking do damage to targeted enemy
		if(Input.GetButtonDown("Fire1"))
		{
			if(attackTimer==0){
				// heavy Melee 
				Attack("heavy");
				attackTimer = coolDown;
			}
		}
		if(Input.GetButtonDown("Fire2"))
		{
			if(attackTimer==0){
				//light Melee
				Attack("light");
				attackTimer = coolDown;
			}
		}
		if(Input.GetButtonDown("Fire3"))
		{
			if(attackTimer==0){
				// kick
				Attack("kick");
				attackTimer = coolDown;
			}
		}
	}
	
	private void Attack(string attack){
		enemyHealth eh = (enemyHealth)target.GetComponent("enemyHealth");
		
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
		
		eh.AdjustCurrentHealth(-20);
	}
}
