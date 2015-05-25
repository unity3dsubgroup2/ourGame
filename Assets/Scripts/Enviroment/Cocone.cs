using UnityEngine;
using System.Collections;

public class Cocone : MonoBehaviour
{

	private Animator myAnim;
	
	void Start ()
	{
		myAnim = GetComponent<Animator> ();
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			myAnim.SetBool ("Open", true);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player") {
			myAnim.SetBool ("Open", false);
		}
	}
}
