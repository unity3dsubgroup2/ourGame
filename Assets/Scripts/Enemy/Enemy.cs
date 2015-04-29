using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	public float health = 100f;
	public bool isAlive = true;
	public float armor = 0f;
	public float damage = 20f;
	public int reward = 10;
	public float shotRate = 3f;

	private Animator myAnim;

	void Start ()
	{
		myAnim = GetComponent<Animator> ();
	}

	public void TakeDamage (float amount)
	{
		// Decrement health
		if (isAlive) {
			health -= amount - (amount * armor * 0.6f) / 100f;
			if (health <= 0) {
				PlayerHealth.playerHealth.GetReward (reward);
				if (myAnim)
					myAnim.SetBool ("Dead", true);
				isAlive = false;
			}
		}
	}


}
