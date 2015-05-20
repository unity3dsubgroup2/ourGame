using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	public float amount = 20f;
	public GameObject owner;
	private float timeToLife = 2f;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject != owner) {
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			if (other.tag == "Enemy") {
				transform.position = new Vector3 (other.transform.position.x,
			                                 other.transform.position.y + 0.75f,
			                                 other.transform.position.z);
				other.GetComponent<Enemy> ().TakeDamage (amount);
			}
			if (other.tag == "Player") {
				other.GetComponent<PlayerHealth> ().TakeDamage (amount);
			}


			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<ParticleSystem> ().Emit (40);
			Destroy (gameObject, 0.5f);
		}
	}

	void Start ()
	{
		Destroy (gameObject, timeToLife);
	}
}
