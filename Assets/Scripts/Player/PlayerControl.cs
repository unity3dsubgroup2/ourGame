using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public GameObject bullet;

	private Transform player;
	public Transform rightHand;
	private Animator myAnim;
	private Rigidbody myRigidbody;

	bool isGrounded = true;
	float shotTimer;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		myAnim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody> ();
		myRigidbody.constraints = 
			RigidbodyConstraints.FreezeRotationX |
			RigidbodyConstraints.FreezeRotationY |
			RigidbodyConstraints.FreezeRotationZ;
		shotTimer = 0;
	}
	
	void FixedUpdate ()
	{
		float v = Input.GetAxis ("Vertical") * 0.5f;
		float h = Input.GetAxis ("Horizontal") * 0.5f;
		if (Input.GetKey (KeyCode.LeftShift)) { //run mode
			v *= 2f;
			h *= 2f;
		}
		myAnim.SetFloat ("Forward", v, 0.1f, Time.deltaTime);
		myAnim.SetFloat ("Turn", h, 0.1f, Time.deltaTime);

	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0) && shotTimer > PlayerHealth.playerHealth.rateFire) { //Shot
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 20f)) {
				if (hit.collider.tag == "Enemy") {
					float angle = Vector3.Angle (transform.forward, (hit.transform.position - transform.position));
					print ("angle=" + (angle).ToString ());
					if (angle < 15f) {
//						myAnim.SetFloat ("Turn", angle, 0.01f, Time.deltaTime);
						GameObject shot = (GameObject)Instantiate (bullet, rightHand.position, Quaternion.identity);
						shot.GetComponent<Rigidbody> ().AddForce ((hit.point - shot.transform.position).normalized * 1000f);
						shotTimer = 0;

					}

				}
			}
		}
		shotTimer += Time.deltaTime;

		if (PlayerHealth.playerHealth.health <= 0) {
			myAnim.SetBool ("EnemyInSight", false);
			myAnim.SetBool ("Dead", true);
		} else {
			myAnim.SetBool ("EnemyInSight", true);
			myAnim.SetBool ("Dead", false);
		}
	}
}
