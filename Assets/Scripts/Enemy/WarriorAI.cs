using UnityEngine;
using System.Collections;

public class WarriorAI : MonoBehaviour
{
	public GameObject bullet;
	public GameObject explosion;

	private Transform player;
	private Animator myAnim;
	private NavMeshAgent navMeshAgent;
	private EnemyHealth myHealth;
	private float speed = 3f;
	private Transform weapon;
	private bool isActive = true;
	
	private float shotTimer = 0;
	
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		weapon = transform.Find ("Weapon").transform;
		weapon.GetComponent<AudioSource> ().pitch += Random.Range (-0.075f, 0.075f);
		myAnim = GetComponent<Animator> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		myHealth = GetComponent<EnemyHealth> ();
	}
	
	void Update ()
	{
		if (isActive) {
			if (myHealth.isAlive) {
				navMeshAgent.speed = speed;
		
				float angle = Vector3.Angle (transform.forward, (player.position - transform.position));
				if (angle < 15f && Vector3.Distance (transform.position, player.position) < myHealth.distanceToShot) {
					if (shotTimer > myHealth.shotRate && myHealth.isAlive) {
						if (myAnim != null) {
							myAnim.SetBool ("EnemyInSight", true);
						}
						Shot ();
						shotTimer = 0;
					}
				} else {
					if (myAnim != null) {
						myAnim.SetBool ("EnemyInSight", false);
					}
				}
				shotTimer += Time.deltaTime;
				if (myAnim != null) {
					myAnim.SetFloat ("Forward", 1f, 0.1f, Time.deltaTime);  // forward walk speed
				}
				if (Vector3.Distance (transform.position, player.position) < myHealth.distanceToShot) {
					GetComponent<Patrolling> ().patrolling = false;
					navMeshAgent.SetDestination (player.position);
				} else {
					GetComponent<Patrolling> ().ResumePatrolling ();
				}
			} else {
				Destroy ((GameObject)Instantiate (
					explosion, new Vector3 (transform.position.x, 1.5f, transform.position.z), Quaternion.identity), 1f);
				Destroy (gameObject);
			}
		}
	}

	void OnAnimatorMove ()
	{
		speed = myAnim.GetFloat ("ForwardSpeed");
	}

	void Shot ()
	{
		weapon.GetComponent<AudioSource> ().Play ();
		GameObject shot = (GameObject)Instantiate (bullet, weapon.position, Quaternion.identity);
		shot.GetComponent<Bullet> ().amount = myHealth.damage;
		shot.GetComponent<Bullet> ().owner = gameObject;
		Vector3 playerBody = new Vector3 (player.position.x, player.position.y + 1f, player.position.z);
		shot.GetComponent<Rigidbody> ().AddForce ((playerBody - shot.transform.position).normalized * 1000f);
	}


}
