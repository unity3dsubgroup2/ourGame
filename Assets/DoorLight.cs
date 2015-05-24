using UnityEngine;
using System.Collections;

public class DoorLight : MonoBehaviour {
	public GameObject dlight;
	public AudioSource dsound;
	float emission;
	float emission1;
	// Use this for initialization
	void Start () {	
	
		}
	// Update is called once per frame
	void Update () {

		if (gameObject.GetComponent<BoxCollider> ().enabled == false) {
			gameObject.GetComponent<MeshRenderer> ().materials [0].SetColor ("_EmissionColor", new Color (emission, emission, emission));
			emission = Mathf.PingPong (Time.time, 1.0f);
			dlight.GetComponent<Light>().color = new Color (emission1,emission1,emission1);
			dsound.enabled = false;
		}else if(gameObject.GetComponent<BoxCollider> ().enabled == true){
			emission1 = Mathf.PingPong (Time.time, 1.0f);
			dlight.GetComponent<Light>().color = new Color (emission1,0,0);
			gameObject.GetComponent<MeshRenderer> ().materials [0].SetColor ("_EmissionColor", new Color (255, 0, 0));
			dsound.enabled = true;
		}
	}
}
