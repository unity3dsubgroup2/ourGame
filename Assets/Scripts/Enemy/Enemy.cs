using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	public float health = 100f;
	public float armor = 0f;
	public float damage = 20f;
	public int amount = 10;

	public void TakeDamage (float amount)
	{
		// Decrement the player's health
		health -= amount - (amount * armor * 0.6f) / 100f;
	}
}
