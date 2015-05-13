using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public GameObject bullet;
	
	private Transform player;
	private ParticleSystem jet;
	private NavMeshAgent navMeshAgent;
	private Enemy myHealth;
	private float speed = 3f;
	private Transform weapon;
	
	private float shotTimer = 0;
	
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		weapon = transform.Find ("Weapon").transform;
		navMeshAgent = GetComponent<NavMeshAgent> ();
		myHealth = GetComponent<Enemy> ();
		jet = transform.Find ("Jet").GetComponent<ParticleSystem> ();
	}
	
	void Update ()
	{
		if (myHealth.isAlive) {
			navMeshAgent.speed = speed;
			
			float angle = Vector3.Angle (transform.forward, (player.position - transform.position));
			if (angle < 15f) {
				if (shotTimer > myHealth.shotRate && myHealth.isAlive) {
					Shot ();
					shotTimer = 0;
				}
			}
			shotTimer += Time.deltaTime;
			navMeshAgent.SetDestination (player.position);
		} else {
			navMeshAgent.Stop ();
			jet.enableEmission = false;
		}
	}
	
	void Shot ()
	{
		GameObject shot = (GameObject)Instantiate (bullet, weapon.position, Quaternion.identity);
		shot.GetComponent<Bullet> ().amount = myHealth.damage;
		shot.GetComponent<Bullet> ().owner = gameObject;
		Vector3 playerBody = new Vector3 (player.position.x, player.position.y, player.position.z);
		shot.GetComponent<Rigidbody> ().AddForce ((playerBody - shot.transform.position).normalized * 1000f);
	}
	

}
