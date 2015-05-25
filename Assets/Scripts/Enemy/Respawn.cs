using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{

	public GameObject spawn;
	public float prepareTime = 5f;
	public float health = 100f;
	public float armor = 50f;
	public int reward = 20;
	public bool isAlive = true;

	Transform player;
	float timer = 0;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	void Update ()
	{
		if (isAlive) {
			if (timer > prepareTime && Vector3.Distance (transform.position, player.position) < 15) {
				GameObject obj = (GameObject)Instantiate (spawn, transform.position, Quaternion.identity);
				timer = 0;
			}
			timer += Time.deltaTime;
		}
	}

	public void TakeDamage (float amount)
	{
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
