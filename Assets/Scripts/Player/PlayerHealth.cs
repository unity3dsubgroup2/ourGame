using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public float health = 100f;		// health (0-100)
	public bool isAlive = true;
	public float armor = 0f;				// armor (0-100) 100 block 60% damage
	public float damage = 0f;		// damage (0-100)
	public float rateFire = 0.5f; 	// rate of fire (0-100) 
	public float rateMissile = 2f;  // time between of missiles lunching (in seconds)
	public float experience = 0f;	// experience for 0 on 1 level to 20000 on 20 level
	public float difficulty = 1f;		// 0.5 - Easy; 1 - Normal (default); 2 - Hard
	public bool gameStarted = false;

	private static PlayerHealth _playerObj;

	public static PlayerHealth playerHealth {
		get {
			if (_playerObj == null) {
				_playerObj = GameObject.FindObjectOfType<PlayerHealth> ();
				DontDestroyOnLoad (_playerObj.gameObject);
			}
			return _playerObj;
		}
	}

	void Awake ()
	{
		if (_playerObj == null) {
			_playerObj = this;
			DontDestroyOnLoad (_playerObj.gameObject);
		} else if (this != _playerObj) {
			Destroy (this.gameObject);
		}
	}

	public void TakeDamage (float amount)
	{
		amount *= difficulty;
		// Decrement the player's health
		health -= amount * 0.75f - (amount * armor * 0.6f) / 100f;
		armor = armor - (amount * (armor / 100f));
		//	armor -= amount * 0.5f - (amount * 0.6f) / 100f;
		if (armor < 0f)
			armor = 0f;
		if (health <= 0f)
			isAlive = false;
	}

	public void GetReward (float amount)
	{
		// get reward points if kill the enemy
		experience += amount;
		health += amount / 10f;
		if (health > 100f)
			health = 100f;
		armor += amount * 5f / 10f;
		if (armor > 100f)
			armor = 100f;
	}

}
