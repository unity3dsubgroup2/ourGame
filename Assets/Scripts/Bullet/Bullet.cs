using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	public float amount = 20f;
	public GameObject owner;
	private float timeToLife = 2f;

	void Start ()
	{
		Destroy (gameObject, timeToLife);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag != owner.tag && other.tag != "Terminal" && other.tag != "Sensor" && other.tag != "Weapon") {
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
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

			GetComponent<MeshRenderer> ().enabled = false;
			GetComponent<ParticleSystem> ().Emit (40);
			GetComponent<AudioSource> ().pitch += Random.Range (-0.075f, 0.075f);
			GetComponent<AudioSource> ().Play ();
			Destroy (gameObject, 0.5f);
		}
	}
}
