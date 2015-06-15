using UnityEngine;
using System.Collections;

public class BakeAI : MonoBehaviour
{
	public GameObject bullet;
	public GameObject explosion;

	private Transform player;
	private NavMeshAgent navMeshAgent;
	private EnemyHealth myHealth;
	private float speed = 2f;
	private Transform weapon;
	private bool isActive = true;
	
	private float shotTimer = 0;
	
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		weapon = transform.Find ("Weapon").transform;
		navMeshAgent = GetComponent<NavMeshAgent> ();
		myHealth = GetComponent<EnemyHealth> ();
	}
	
	void Update ()
	{
		if (isActive) {
			if (myHealth.isAlive) {
				navMeshAgent.speed = speed / PlayerHealth.playerHealth.difficulty;
				// shot
				float angle = Vector3.Angle (transform.forward, (player.position - transform.position));
				if (angle < 15f && Vector3.Distance (transform.position, player.position) < myHealth.distanceToShot) {
					if (shotTimer > myHealth.shotRate && myHealth.isAlive) {
						Shot ();
					}
				}
				shotTimer += Time.deltaTime;
				if (Vector3.Distance (transform.position, player.position) < myHealth.distanceToShot) {
					navMeshAgent.SetDestination (player.position);
				}
			} else {
				Destroy ((GameObject)Instantiate (explosion, new Vector3 (transform.position.x, 1.5f, transform.position.z), Quaternion.identity), 1f);
				Destroy (gameObject);
			}
		}
	}

	void Shot ()
	{
		shotTimer = 0;
		GameObject shot = (GameObject)Instantiate (bullet, weapon.position, Quaternion.identity);
		shot.GetComponent<Lighting> ().amount = myHealth.damage;
		shot.GetComponent<Lighting> ().owner = gameObject;
		shot.GetComponent<Lighting> ().lightingReceiver = player.gameObject;
		shot.transform.SetParent (transform);
	}


}
