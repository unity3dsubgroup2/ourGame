﻿using UnityEngine;
using System.Collections;

public class Terminal : MonoBehaviour
{
	public GameObject door;
	public Renderer monitor;
	public bool needKey = false;
	public AudioClip okSound;
	public AudioClip notOkSound;
	AudioSource sound;
	bool onCol = false;
	Color emitColor = Color.red;

	void Start ()
	{
		sound = GetComponent<AudioSource> ();
	}
	
	void Update ()
	{
		if (onCol && (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.E))) {
			if (!needKey || PlayerHealth.playerHealth.hasKey) {
				door.GetComponent<DoorLight> ().Effects (!door.GetComponent<BoxCollider> ().enabled);
				emitColor = (door.GetComponent<BoxCollider> ().enabled) ? Color.red : emitColor = Color.green;
				monitor.material.SetColor ("_EmissionColor", emitColor);
				sound.clip = okSound;
				sound.Play ();
			} else {
				sound.clip = notOkSound;
				sound.Play ();
			}
		}
	}

	void FixedUpdate ()
	{
		if (monitor && monitor.GetComponent<MeshRenderer> ().isVisible) {
			monitor.material.mainTextureOffset += new Vector2 (Time.deltaTime * 0.1f, 0.0f);
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
