using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class txtUpdate : MonoBehaviour
{

	float playerHealth = -1;
	float enemyHealth = -1;

	Text txtPlayerHealth;
	Text txtEnemyHealth;
	void Start ()
	{
		txtPlayerHealth = transform.FindChild ("playerHealth").GetComponent<Text> ();
		txtEnemyHealth = transform.FindChild ("enemyHealth").GetComponent<Text> ();
	}
	
	void Update ()
	{
		if (playerHealth != PlayerHealth.playerHealth.health) {
			playerHealth = PlayerHealth.playerHealth.health;
			txtPlayerHealth.text = "Player Health = " + playerHealth;
		}
	}
}
