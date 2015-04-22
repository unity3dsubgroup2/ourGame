using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	public float speed = 1f;

	private Transform player;
	private Animator anim;

	bool isGrounded = true;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		anim = GetComponent<Animator> ();
	}
	
	void FixedUpdate ()
	{
		float v = Input.GetAxis ("Vertical");

	}

	void Update ()
	{

	}

	void UpdateAnimator ()
	{

	}
}
