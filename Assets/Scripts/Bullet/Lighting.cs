using UnityEngine;
using System.Collections;

public class Lighting : MonoBehaviour
{
	
	public GameObject lightingReceiver;
	public int sections = 30;
	public int lines = 3;
	public float lineWidth = 0.3f;
	public float jitter = 0.1f;
	public Material lightningMaterial;
	public float timeToLife = 1f;
	public float amount = 20f;
	public GameObject owner;
	LineRenderer[] lineRenderers = null;
	
	void Start ()
	{
		InitializeLighting ();
		if (lightingReceiver.tag == "Player") {
			lightingReceiver.GetComponent<PlayerHealth> ().TakeDamage (amount);
		}
		Destroy (gameObject, timeToLife);
	}
	
	public void InitializeLighting ()
	{
		//Clean existing line renderers
		lineRenderers = gameObject.GetComponentsInChildren<LineRenderer> ();
		if (lineRenderers != null) {
			for (int i = 0; i < lineRenderers.Length; i++)
				DestroyImmediate (lineRenderers [i].gameObject);
			
			lineRenderers = null;
		}
		//Create game object for each line renderer and parent it to this game object
		for (int i = 0; i < (lines); i++) {
			GameObject temp = new GameObject ();
			temp.transform.parent = gameObject.transform;
			temp.name = "Line Renderer " + (i + 1);
			temp.AddComponent<LineRenderer> ();
		}
		lineRenderers = gameObject.GetComponentsInChildren<LineRenderer> ();
		//Set initial settings for each renderer
		for (int i = 0; i < lineRenderers.Length; i++) {
			lineRenderers [i].castShadows = false;
			lineRenderers [i].receiveShadows = false;
			lineRenderers [i].material = lightningMaterial;
			
			lineRenderers [i].SetVertexCount (sections);
			lineRenderers [i].SetWidth (lineWidth, lineWidth);
		}
	}
	
	void Update ()
	{
		//Determine the length of a section of the bolt
		Vector3 sectionVector = (lightingReceiver.transform.position - transform.position) / sections;
		
		//Initialise an array of vectors for the bolt
		Vector3[] lineVectors = new Vector3[sections];
		
		//Calculate the vectors for the middle sections
		for (int j = 1; j < lineVectors.Length -1; j++)
			lineVectors [j] = transform.position + (sectionVector * j);
		
		//Set the values in the line renderer for ecah bolt
		for (int j = 0; j < lines; j++) {
			if (lineRenderers [j]) {				
				//Set the beginning and end
				lineRenderers [j].SetPosition (0, transform.position);
				lineRenderers [j].SetPosition (lineVectors.Length - 1, lightingReceiver.transform.position);
				lineRenderers [j].SetWidth (lineWidth, lineWidth);
				
				//Set vectors for the rest of the sections adding jitter
				for (int k = 1; k < (sections - 1); k++)
					lineRenderers [j].SetPosition (k, AddVectorJitter (lineVectors [k], jitter));
			}
		}
	}
	
	//Add random jitter to vector
	Vector3 AddVectorJitter (Vector3 vector, float jitter)
	{
		vector += Vector3.left * Random.Range (-jitter, jitter);
		vector += Vector3.up * Random.Range (-jitter, jitter);
		vector += Vector3.forward * Random.Range (-jitter, jitter);
		
		return vector;
	}
}
