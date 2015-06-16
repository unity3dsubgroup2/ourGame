using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
	public float amount = 200f;
	public GameObject target;
	public GameObject owner;
	public GameObject explosion;

	private Rigidbody myBody;
	private float speed = 0.01f;
	private Vector3 hitPoint;
	
	void Start ()
	{
		myBody = GetComponent<Rigidbody> ();
		myBody.AddForce (new Vector3 (0f, 10f, 0f), ForceMode.Impulse);
	}
	
	void FixedUpdate ()
	{
		if (target.gameObject != null) {
			hitPoint = target.transform.position + new Vector3 (0f, 0.75f, 0f);
		} else if (Vector3.Distance (transform.position, hitPoint) < 1f) { // if the target destroyed - move to last known position and selfdestroy
			Destroy ((GameObject)Instantiate (explosion, new Vector3 (transform.position.x, 1.5f, transform.position.z), Quaternion.identity), 1f);
			Destroy (gameObject);
		}
		speed += Time.deltaTime / 4f;
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (hitPoint - transform.position), 0.1f);
		transform.position = Vector3.MoveTowards (transform.position, hitPoint + new Vector3 (0f, 1f, 0f), speed);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other != null && other.tag != owner.tag && other.tag != "Sensor") { // ignore the sensor's collider
			if (other.tag == "Player") {
				other.GetComponent<PlayerHealth> ().TakeDamage (amount);
			} else {
				if (other.GetComponent<EnemyHealth> () != null) {
					other.GetComponent<EnemyHealth> ().TakeDamage (amount);
				}
			}
			Destroy ((GameObject)Instantiate (explosion, new Vector3 (transform.position.x, 1.5f, transform.position.z), Quaternion.identity), 1f);
			Destroy (gameObject);
		}
	}
}
