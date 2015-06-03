using UnityEngine;
using System.Collections;

public class Terminal : MonoBehaviour
{
	public GameObject door;
	public Renderer monitor;
	AudioSource sound;
	bool onCol = false;
	Color emitColor = Color.red;

	void Start ()
	{
		sound = GetComponent<AudioSource> ();
	}
	
	void Update ()
	{
		if (onCol && Input.GetKeyDown (KeyCode.Space)) {
			door.GetComponent<DoorLight> ().Effects (!door.GetComponent<BoxCollider> ().enabled);
			emitColor = (door.GetComponent<BoxCollider> ().enabled) ? Color.red : emitColor = Color.green;
			sound.Play ();
		}
		if (monitor) {
			monitor.material.mainTextureOffset += new Vector2 (Time.deltaTime * 0.1f, 0.0f);
			monitor.material.SetColor ("_EmissionColor", emitColor);
		}
	}

	void OnTriggerStay (Collider col)
	{
		if (col.tag == "Player" && !onCol) {
			onCol = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		onCol = false;		
	}
}
