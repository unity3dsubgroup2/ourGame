﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	public float amount = 20f;
	private float timeToLife = 2f;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Enemy") {
			other.GetComponent<Enemy> ().TakeDamage (amount);
		}
		if (other.tag == "Player") {
			other.GetComponent<PlayerHealth> ().TakeDamage (amount);
		}
		Destroy (gameObject);
	}

	void Start ()
	{
		Destroy (gameObject, timeToLife);
	}
}
