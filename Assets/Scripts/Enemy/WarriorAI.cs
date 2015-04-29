using UnityEngine;
using System.Collections;

public class WarriorAI : MonoBehaviour
{
	public Transform weapon;
	public GameObject bullet;

	private Transform player;
	private Animator myAnim;
	private NavMeshAgent navMeshAgent;
	private Enemy myHealth;
	private float speed;

	private float shotTimer = 0;
	
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		myAnim = GetComponent<Animator> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		myHealth = GetComponent<Enemy> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (myHealth.isAlive) {
			navMeshAgent.speed = speed;
		
			float angle = Vector3.Angle (transform.forward, (player.position - transform.position));
			if (angle < 10f) {
				if (shotTimer > myHealth.shotRate && myHealth.isAlive) {
					myAnim.SetBool ("EnemyInSight", true);
					Shot ();
					shotTimer = 0;
				}
			} else {
				myAnim.SetBool ("EnemyInSight", false);
			}
			shotTimer += Time.deltaTime;
			myAnim.SetFloat ("Forward", 0.5f, 0.1f, Time.deltaTime);
			navMeshAgent.SetDestination (player.position);
		} else {
			navMeshAgent.Stop ();
			myAnim.SetBool ("EnemyInSight", false);
		}
	}

	void OnAnimatorMove ()
	{
		//	Vector3 newPosition = transform.position;
		//	newPosition.z += myAnim.GetFloat ("ForwardSpeed") * Time.deltaTime; 
		//	transform.position = newPosition;
		speed = myAnim.GetFloat ("ForwardSpeed");

	}

	void Shot ()
	{
		GameObject shot = (GameObject)Instantiate (bullet, weapon.position, Quaternion.identity);
		shot.GetComponent<Bullet> ().amount = PlayerHealth.playerHealth.damage;
		Vector3 playerBody = new Vector3 (player.position.x, player.position.y + 1f, player.position.z);
		shot.GetComponent<Rigidbody> ().AddForce ((playerBody - shot.transform.position).normalized * 1000f);
	}


}
