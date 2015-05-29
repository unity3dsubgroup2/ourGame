using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{

	public GameObject spawn;
	public float prepareTime = 5f;
	public int spawnsPreWarm = 3;
	public int reward = 20;
	private EnemyHealth myHealth;
	

	bool isActive = false;
	Transform player;
	float timer = 0;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		myHealth = GetComponent<EnemyHealth> ();
		// Generate prewarmed spawns
		for (int i = 0; i < spawnsPreWarm; i++) {
			InstantiateSpawn ();
		}
		
	}

	void Update ()
	{
		if (isActive && myHealth.isAlive) {
			if (timer > prepareTime) {
				InstantiateSpawn ();
				timer = 0;
			}
			timer += Time.deltaTime;
		}
	}

	public void Activate ()
	{
		isActive = true;
	}

	void InstantiateSpawn ()
	{
		GameObject obj = (GameObject)Instantiate (spawn, transform.position, Quaternion.identity);
		if (obj.GetComponent<AudioSource> () != null) { // if the object have walk sound - set random pitch offset
			obj.GetComponent<AudioSource> ().pitch += Random.Range (-0.1f, 0.1f);
		}
	}
}
