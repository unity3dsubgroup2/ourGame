using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public float speed = 1f;

	private Transform player;
	private Animator myAnim;
	private Rigidbody myRigidbody;

	bool isGrounded = true;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		myAnim = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody> ();
		myRigidbody.constraints = 
			RigidbodyConstraints.FreezeRotationX |
			RigidbodyConstraints.FreezeRotationY |
			RigidbodyConstraints.FreezeRotationZ;
	}
	
	void FixedUpdate ()
	{
		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");
		if (Input.GetKey (KeyCode.LeftShift)) {
			v *= 0.5f;
			h *= 0.5f;
		}
		myAnim.SetFloat ("Forward", v, 0.1f, Time.deltaTime);
		myAnim.SetFloat ("Turn", h, 0.1f, Time.deltaTime);

	}

	void Update ()
	{

	}
}
