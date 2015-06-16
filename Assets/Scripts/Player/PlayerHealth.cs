using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public float health = 100f;		// health (0-100)
	public float Health {
		get {
			return health;
		}
		set {
			health = value;
			if (health > 100f)
				health = 100f;
			if (health < 0f)
				health = 0f;
			if (health <= 0f)
				isAlive = false;
		}
	}
	public float armor = 100f;				// armor (0-100) 100 block 60% damage
	public float Armor {
		get {
			return armor;
		}
		set {
			armor = value;
			if (armor > 100)
				armor = 100;
			if (armor < 0)
				armor = 0;
		}
	}
	public bool isAlive = true;
	public float damage = 0f;		// damage (0-100)
	public float rateFire = 0.5f; 	// rate of fire (0-100) 
	public float rateMissile = 2f;  // time between of missiles lunching (in seconds)
	public float experience = 0f;	// experience for 0 on 1 level to 20000 on 20 level
	public float difficulty = 1f;		// 0.5 - Easy; 1 - Normal (default); 2 - Hard
	public bool hasKey = false;
	public bool gameStarted = false;
	Vector3 startPoint;

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
			startPoint = transform.position;
			DontDestroyOnLoad (_playerObj.gameObject);
		} else if (this != _playerObj) {
			Destroy (this.gameObject);
		}
	}

	public void TakeDamage (float amount)
	{
		amount *= difficulty;
		// Decrement the player's health
		Health -= amount * 0.75f - (amount * Armor * 0.6f) / 100f;
		Armor -= amount * (Armor / 100f);
	}

	public void GetReward (float amount)
	{
		// get reward points if kill the enemy
		experience += amount;
	}

	public void RestartLevel ()
	{
		gameStarted = false;
		health = 100f;
		armor = 100f;
		experience = 0f;
		difficulty = 1f;
		hasKey = false;
		isAlive = true;
		transform.position = startPoint;
		GetComponent<PlayerControl> ().isWeapon1Active = false;
		GetComponent<PlayerControl> ().isWeapon2Active = false;
		GetComponent<PlayerControl> ().isMissileActive = false;
		Application.LoadLevel (0);
	}
}
