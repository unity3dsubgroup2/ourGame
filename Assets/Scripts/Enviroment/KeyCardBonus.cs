using UnityEngine;
using System.Collections;

public class KeyCardBonus : MonoBehaviour
{
	public enum Bonuses
	{
		Health = 0,
		Armor,
		Key
	}
	public Bonuses bonusType;
	public GameObject[] meshes = new GameObject[3];
	public float RotateSpeed = 200f;
	public float amount = 5f;
	GameObject mesh;

	void Start ()
	{
		mesh = (GameObject)Instantiate (meshes [(int)bonusType], Vector3.zero, Quaternion.identity);
		mesh.transform.parent = transform;
		mesh.transform.localPosition = Vector3.zero;
		mesh.transform.localScale = new Vector3 (20, 20, 20);
		mesh.transform.Rotate (10f, 10f, 10f, Space.World);
	}
	
	void FixedUpdate ()
	{
		if (mesh.GetComponent<MeshRenderer> ().isVisible) {
			mesh.transform.Rotate (0f, Time.deltaTime * RotateSpeed, 0f, Space.World);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			switch (bonusType) {
			case Bonuses.Health:
				PlayerHealth.playerHealth.Health += amount / PlayerHealth.playerHealth.difficulty;
				break;
			case Bonuses.Armor:
				PlayerHealth.playerHealth.Armor += amount / PlayerHealth.playerHealth.difficulty;
				break;
			case Bonuses.Key:
				PlayerHealth.playerHealth.hasKey = true;
				break;
			}
			GetComponent<AudioSource> ().Play ();
			mesh.SetActive (false);
			GetComponent<SphereCollider> ().enabled = false;
			Destroy (gameObject, 2f);
		}
	}
}
