using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
	public float amount = 200f;
	public GameObject target;
	public GameObject owner;

	private Rigidbody myBody;
	private float speed = 0.01f;
	private Vector3 hitPoint;
	
	void Start ()
	{
		myBody = GetComponent<Rigidbody> ();
		myBody.AddForce (new Vector3 (0f, 8f, 0f), ForceMode.Impulse);
	}
	
	void FixedUpdate ()
	{
		if (target.gameObject != null) {
			hitPoint = target.transform.position;
		} else if (Vector3.Distance (transform.position, hitPoint) < 1f) { // if the target destroyed - move to last known position and selfdestroy
			Destroy (gameObject);
		}
		speed += Time.deltaTime / 4f;
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (hitPoint - transform.position), 0.1f);
		transform.position = Vector3.MoveTowards (transform.position, hitPoint + new Vector3 (0f, 1f, 0f), speed);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			other.GetComponent<PlayerHealth> ().TakeDamage (amount);
		} else {
			if (other.GetComponent<EnemyHealth> () != null) {
				transform.position = new Vector3 (other.transform.position.x,
				                                  other.transform.position.y + 0.75f,
				                                  other.transform.position.z);
				other.GetComponent<EnemyHealth> ().TakeDamage (amount);
			}
		}
		Destroy (gameObject);
	}
}
