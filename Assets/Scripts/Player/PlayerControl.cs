using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public GameObject bullet;

//	private Transform player;
	private Transform weapon;
	private Animator myAnim;

//	bool isGrounded = true;
	float shotTimer;

	void Start ()
	{
//		player = GameObject.FindGameObjectWithTag ("Player").transform;
		myAnim = GetComponent<Animator> ();
		weapon = transform.Find ("Turret/Weapon").transform;
		shotTimer = 0;
	}
	
	void Update ()
	{
		if (Input.GetMouseButton (0) && shotTimer > PlayerHealth.playerHealth.rateFire) { //Shot
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 20f)) {
				if (hit.collider.tag != "Building" && hit.collider.tag != "Player") {
					GameObject shot = (GameObject)Instantiate (bullet, weapon.position, Quaternion.identity);
					shot.GetComponent<Bullet> ().owner = gameObject;
					shot.GetComponent<Bullet> ().amount = PlayerHealth.playerHealth.damage;
					shot.GetComponent<Rigidbody> ().AddForce ((hit.point - shot.transform.position).normalized * 1000f);
					shotTimer = 0;
				}
			}
		}
		shotTimer += Time.deltaTime;
		if (myAnim) {
			if (PlayerHealth.playerHealth.health <= 0) {
				myAnim.SetBool ("EnemyInSight", false);
				myAnim.SetBool ("Dead", true);
			} else {
				myAnim.SetBool ("EnemyInSight", true);
				myAnim.SetBool ("Dead", false);
			}
		}
	}
}
