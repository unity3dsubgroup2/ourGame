using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{

	public GameObject spawn;
	public float prepareTime = 5f;
	public int reward = 20;
	private EnemyHealth myHealth;
	

	bool isActive = false;
	Transform player;
	float timer = 0;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		myHealth = GetComponent<EnemyHealth> ();
		
	}

	void Update ()
	{
		if (isActive && myHealth.isAlive) {
			if (timer > prepareTime) {
				GameObject obj = (GameObject)Instantiate (spawn, transform.position, Quaternion.identity);
				timer = 0;
			}
			timer += Time.deltaTime;
		}
	}

	public void Activate ()
	{
		isActive = true;
	}
}
