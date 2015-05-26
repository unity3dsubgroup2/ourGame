using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
	public float amount = 200f;
	public GameObject target;
	public GameObject owner;

	private Rigidbody myBody;
	private float speed = 0.01f;
	
	void Start ()
	{
		myBody = GetComponent<Rigidbody> ();
		myBody.AddForce (new Vector3 (0f, 8f, 0f), ForceMode.Impulse);
	}
	
	void Update ()
	{
		speed += Time.deltaTime / 4f;
		//transform.LookAt (target.transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (target.transform.position - transform.position), 0.1f);
		transform.position = Vector3.MoveTowards (transform.position, target.transform.position + new Vector3 (0f, 1f, 0f), speed);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag != owner.tag && other.tag != "Terminal") {
			if (other.tag == "Enemy") {
				other.GetComponent<Enemy> ().TakeDamage (amount);
			}
			if (other.tag == "Respawn") {
				other.GetComponent<Respawn> ().TakeDamage (amount);
			}
			if (other.tag == "Player") {
				other.GetComponent<PlayerHealth> ().TakeDamage (amount);
			}
			Destroy (gameObject);
		}
	}
}
