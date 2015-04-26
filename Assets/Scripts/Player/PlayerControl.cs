using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public GameObject bullet;
	LineRenderer laser;

	private Transform player;
	public Transform rightHand;
	private Animator myAnim;
	private Rigidbody myRigidbody;

	bool isGrounded = true;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		laser = GetComponent<LineRenderer> ();
		myAnim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody> ();
		myRigidbody.constraints = 
			RigidbodyConstraints.FreezeRotationX |
			RigidbodyConstraints.FreezeRotationY |
			RigidbodyConstraints.FreezeRotationZ;
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
		if (Input.GetMouseButtonDown (0)) { //Shot
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 20f)) {
				if (hit.collider.tag == "Enemy") {
					laser.SetPosition (0, rightHand.position);
					laser.SetPosition (1, hit.point);

					GameObject shot = (GameObject)Instantiate (bullet, rightHand.position, Quaternion.identity);
//					shot.GetComponent<Rigidbody> ().MovePosition (hit.point);
//					shot.GetComponent<Rigidbody> ().AddForce (hit.point * 12f);
					shot.GetComponent<Rigidbody> ().AddForce ((hit.point - shot.transform.position).normalized * 120f);
//					shot.GetComponent<Rigidbody> ().MovePosition (hit.point);

				}
			}
		}
	}
}
