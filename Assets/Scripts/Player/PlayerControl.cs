using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public GameObject bullet;

//	private Transform player;
	int layerMask = 1 << 2; // IgnoreRayCast
	private Transform weapon;
	private Animator myAnim;
	private LineRenderer lazer;

//	bool isGrounded = true;
	float shotTimer;

	void Start ()
	{
//		player = GameObject.FindGameObjectWithTag ("Player").transform;
		layerMask = ~layerMask;
		myAnim = GetComponent<Animator> ();
		weapon = transform.Find ("Turret/Gun1/Weapon").transform;
		shotTimer = 0;
		lazer = transform.Find ("Turret/Laser").GetComponent<LineRenderer> ();
	}
	
	void Update ()
	{
		RaycastHit hitInfo;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Physics.Raycast (weapon.transform.position, weapon.forward, out hitInfo, Mathf.Infinity, layerMask);
/*		if (Physics.Raycast (ray, out hitInfo, 20f)) {
			if (hitInfo.collider.tag != "Enemy" && hitInfo.collider.tag != "Environment" && hitInfo.collider.tag != "Warrior") {
				Physics.Raycast (weapon.transform.position, weapon.forward, out hitInfo);
			}
		}
*/		// draw lazer
		if (lazer != null) {
			lazer.materials [0].mainTextureOffset += new Vector2 (Time.deltaTime * 0.1f, 0.0f);
			lazer.materials [0].SetTextureOffset ("_NoiseTex", new Vector2 (-Time.time, 0.0f));
			lazer.SetPosition (0, lazer.transform.position);
			lazer.SetPosition (1, hitInfo.point);
		}
		// shot
		if (Input.GetMouseButton (0) && shotTimer > PlayerHealth.playerHealth.rateFire) {
			if (hitInfo.collider.tag != "Player") {
				weapon.GetComponent<AudioSource> ().Play ();
				GameObject shot = (GameObject)Instantiate (bullet, weapon.position, Quaternion.identity);
				shot.GetComponent<Bullet> ().owner = gameObject;
				shot.GetComponent<Bullet> ().amount = PlayerHealth.playerHealth.damage;
				shot.GetComponent<Rigidbody> ().AddForce ((hitInfo.point - shot.transform.position).normalized * 1000f);
				shotTimer = 0;
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
