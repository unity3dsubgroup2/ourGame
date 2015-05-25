using UnityEngine;
using System.Collections;

public class Terminal : MonoBehaviour {
	public GameObject Door;
	public AudioSource tdoor;
	bool OnCol = false;
	bool OffOn = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space) && OnCol == true && OffOn) {			
			Door.GetComponent<BoxCollider>().enabled = false;
			tdoor.Play();
			print ("door is opened");
		}
		if (Input.GetKeyUp (KeyCode.Space) && OnCol) {			
			OffOn = !OffOn;
			print (OffOn);
		}
		if (Input.GetKeyDown (KeyCode.Space) && OnCol && !OffOn) {
			Door.GetComponent<BoxCollider> ().enabled = true;
			tdoor.Play();
			print ("door is closed");

		}
	}
	void OnTriggerStay(Collider col){
		if (col.tag == "Enemy") {
			OnCol = true;
			print ("col on");
		}
	}
	void OnTriggerExit(Collider other){
		OnCol = false;		
		print ("col off");		
	}
}
