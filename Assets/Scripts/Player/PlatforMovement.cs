using UnityEngine;
using System.Collections;

public class PlatforMovement : MonoBehaviour
{
	public float speed = 6f;

	private Rigidbody myRigidbody;
	private Transform turret;
	private Vector3 turretPos;
	private Vector3 mousePos;


	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody> ();
		turret = transform.Find ("Turret").transform;
	}

	void FixedUpdate ()
	{
		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");
		myRigidbody.velocity = new Vector3 (h * speed, 0f, v * speed);
		transform.rotation = new Quaternion (v / speed, 0f, -h / speed, 1f);

		// rotate turret
		turretPos = Camera.main.WorldToScreenPoint (turret.position);
		turretPos = new Vector3 (turretPos.x, 0f, turretPos.y);
		mousePos = new Vector3 (Input.mousePosition.x, 0f, Input.mousePosition.y);
		turret.rotation = Quaternion.LookRotation (mousePos - turretPos);


	}
}