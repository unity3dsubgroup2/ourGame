using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{

	public GameObject spawn;
	public float prepareTime = 5f;
	public int spawnsPreWarm = 3;
	public int maxClones = 10;
	public int reward = 20;
	public GameObject cloneEffects;
	public GameObject breakEffects;
	private EnemyHealth myHealth;

	bool isActive = false;
	bool dead = false;
	float timer = 0;
	int clonesCount = 0;

	void Start ()
	{
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
		} else if (!myHealth.isAlive && !dead) {
			dead = true;
			Dead ();
		}
	}

	void Dead ()
	{
		transform.Find ("Particle System").gameObject.SetActive (false);
		GameObject glow = (GameObject)Instantiate (breakEffects, transform.position, Quaternion.identity);
		glow.transform.Rotate (Vector3.up);
		glow.transform.SetParent (transform);
		GetComponent<CapsuleCollider> ().enabled = false;
	}

	public void Activate ()
	{
		GetComponent<AudioSource> ().Play ();
		isActive = true;
	}

	void InstantiateSpawn ()
	{
		if (clonesCount < maxClones) {
			Vector3 spawnPoint = new Vector3 (transform.position.x + Random.Range (1f, 2f), 0f, transform.position.z + Random.Range (1f, 2f));
			// show spawn effects (particle system)
			if (cloneEffects != null) {
				Destroy ((GameObject)Instantiate (cloneEffects, spawnPoint, Quaternion.identity), 1.5f);
			}
			GameObject obj = (GameObject)Instantiate (spawn, spawnPoint, Quaternion.identity);
			if (obj.GetComponent<AudioSource> () != null) { // if the object have walk sound - set random pitch offset
				obj.GetComponent<AudioSource> ().pitch += Random.Range (-0.1f, 0.1f);
				if (obj.GetComponent<NavMeshAgent> ())
					obj.GetComponent<NavMeshAgent> ().avoidancePriority = clonesCount * 2;
				if (obj.GetComponent<Patrolling> ())
					obj.GetComponent<Patrolling> ().patrolling = true;
			}
			clonesCount++;
		}
	}
}
