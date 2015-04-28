using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	public float health = 100f;
	public float armor = 0f;
	public float damage = 20f;
	public int reward = 10;
	public float shotRate = 3f;

	private float shotTimer = 0;

	public void TakeDamage (float amount)
	{
		// Decrement the player's health
		health -= amount - (amount * armor * 0.6f) / 100f;
		if (health <= 0) {
			PlayerHealth.playerHealth.GetReward (reward);
			health = 100f;
		}
	}

	void Update ()
	{

		if (shotTimer > shotRate) {
			PlayerHealth.playerHealth.TakeDamage (damage);
			shotTimer = 0;
		}

		shotTimer += Time.deltaTime;
	}
}
