﻿using UnityEngine;
using System.Collections;

public class ActSensor : MonoBehaviour
{
	public GameObject[] objects;

	bool activated = false;

	void OnTriggerEnter (Collider other)
	{
		if (!activated && other.tag == "Player") {
			for (int i = 0; i < objects.Length; i++) {
				switch (objects [i].tag) {
				case "Door":
					objects [i].GetComponent<DoorLight> ().Activate ();
					break;
				case "Respawn":
					objects [i].GetComponent<Respawn> ().Activate ();
					break;
				}
			}
			activated = true;
		}
	}
}
