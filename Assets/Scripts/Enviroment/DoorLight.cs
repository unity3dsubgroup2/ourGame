using UnityEngine;
using System.Collections;

public class DoorLight : MonoBehaviour
{
	Light dlight;
	AudioSource dsound;
	public MeshRenderer door;
	float emission;

	void Start ()
	{	
		dsound = GetComponent<AudioSource> ();
		dlight = GetComponent<Light> ();
	}

	void FixedUpdate ()
	{
		if (gameObject.GetComponent<BoxCollider> ().enabled && door.GetComponent<Renderer> ().isVisible) {
			emission = Mathf.PingPong (Time.time, 1.0f);
			dlight.GetComponent<Light> ().color = new Color (emission, 0, 0);
			door.material.SetColor ("_EmissionColor", new Color (emission, 0, 0));
		}
	}

	public void Effects (bool state)
	{
		door.enabled = state;
		if (dsound)
			dsound.enabled = state;
		GetComponent<BoxCollider> ().enabled = state;
	}

	public void Activate ()
	{
		Effects (true);
	}
}
