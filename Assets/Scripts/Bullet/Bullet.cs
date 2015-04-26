using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	public float amount = 20f;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Enemy") {
			other.GetComponent<Enemy> ().TakeDamage (amount);
		}
		if (other.tag != "Player") {
			Destroy (gameObject);
		}
	}
}
