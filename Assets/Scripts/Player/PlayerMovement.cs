using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	private Animator myAnim;
	private Rigidbody myRigidbody;

	void Start ()
	{
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
}
