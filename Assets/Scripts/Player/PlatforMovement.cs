using UnityEngine;
using System.Collections;

public class PlatforMovement : MonoBehaviour
{
	public float speed = 1f;

	private ParticleSystem pUp;
	private ParticleSystem pDown;
	private ParticleSystem pLeft;
	private ParticleSystem pRight;
	private Rigidbody myRigidbody;

	void Start ()
	{
		pUp = transform.Find ("Platform/Jets/Up/pUp").GetComponent<ParticleSystem> ();
		pDown = transform.Find ("Platform/Jets/Down/pDown").GetComponent<ParticleSystem> ();
		pLeft = transform.Find ("Platform/Jets/Left/pLeft").GetComponent<ParticleSystem> ();
		pRight = transform.Find ("Platform/Jets/Right/pRight").GetComponent<ParticleSystem> ();

		myRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");
		myRigidbody.velocity = new Vector3 (h * speed, 0f, v * speed);
		transform.rotation = new Quaternion (v / speed, 0f, -h / speed, 1f);
	}
}
