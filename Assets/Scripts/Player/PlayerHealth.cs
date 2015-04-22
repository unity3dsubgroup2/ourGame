using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	float health = 100f;		// health (0-100)
	float armor = 0f;			// armor (0-100) 100 block 60% damage
	float damage = 0f;			// damage (0-100)
	float rateFire = 0f; 			// rate of fire (0-100) 
	float experience = 0f;		// experience for 0 on 1 level to 20000 on 20 level
	float fireDistance = 0f;	// ? distance to damage enemy

	public void TakeDamage (float amount)
	{
		// Decrement the player's health
		health -= amount - (amount * armor * 0.6f) / 100f;
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			print ("before " + health);
			TakeDamage (20f);
			print ("after " + health);
			health = 100f;
		}
		if (Input.GetMouseButtonDown (1)) {
			if (armor <= 90) {
				armor += 10f;
			}
			print ("armor " + armor);
		}
	}
}
