using UnityEngine;
using System.Collections;

public class winZone : MonoBehaviour
{

	public mnuMain menuMain;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			menuMain.GameOver (true);
		}
	}
}
