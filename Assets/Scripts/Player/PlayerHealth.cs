using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public float health = 100f;		// health (0-100)
	float armor = 0f;				// armor (0-100) 100 block 60% damage
	public float damage = 0f;		// damage (0-100)
	public float rateFire = 0.5f; 	// rate of fire (0-100) 
	public float experience = 0f;	// experience for 0 on 1 level to 20000 on 20 level

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
		// Decrement the player's health
		health -= amount - (amount * armor * 0.6f) / 100f;
	}

	public void GetReward (float amount)
	{
		// get reward points if kill the enemy
		experience += amount;
	}
}
