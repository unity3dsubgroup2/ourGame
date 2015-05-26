﻿using UnityEngine;
using System.Collections;

public class Terminal : MonoBehaviour
{
	public GameObject door;
	public Renderer monitor;
	AudioSource sound;
	bool onCol = false;
	bool offOn = true;

	void Start ()
	{
		sound = GetComponent<AudioSource> ();
	}
	
	void Update ()
	{
		if (onCol && Input.GetKeyDown (KeyCode.Space)) {
			door.GetComponent<DoorLight> ().Effects (!door.GetComponent<BoxCollider> ().enabled);
			sound.Play ();
		}
		if (monitor)
			monitor.material.mainTextureOffset += new Vector2 (Time.deltaTime * 0.1f, 0.0f);
/*
		if (Input.GetKeyDown (KeyCode.Space) && OnCol == true && OffOn) {			
			door.GetComponent<BoxCollider> ().enabled = false;
			sound.Play ();
			print ("door is opened");
		}
		if (Input.GetKeyUp (KeyCode.Space) && OnCol) {			
			OffOn = !OffOn;
			print (OffOn);
		}
		if (Input.GetKeyDown (KeyCode.Space) && OnCol && !OffOn) {
			door.GetComponent<BoxCollider> ().enabled = true;
			sound.Play ();
			print ("door is closed");
		}
*/
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
