using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{

	public float health = 100f;
	public bool isAlive = true;
	public float armor = 0f;
	public float damage = 20f;
	public int reward = 10;
	public float shotRate = 3f;
	public float distanceToShot = 15f;

	public void TakeDamage (float amount)
	{
		amount /= PlayerHealth.playerHealth.difficulty;
		// Decrement health
		if (isAlive) {
			health -= amount - (amount * armor * 0.6f) / 100f;
			if (health <= 0) {
				PlayerHealth.playerHealth.GetReward (reward);
				isAlive = false;
			}
		}
	}
}
