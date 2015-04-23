using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
	
	public float Smooth; // speed camera moving
	
	private Transform Player;
	private Transform camContainer;
	private Vector3 relPlayerPosition;
	
	private Vector3 camPosition;
	private Vector3 newCamPosition;
	
	
	
	void Start ()
	{
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
		camContainer = transform.parent.transform;
		
		// Setting camera position in scene
		camPosition = transform.parent.transform.position - Player.position;
	}
	
	void FixedUpdate ()
	{
		
		//	print (Input.mousePosition);
		
		// smooth move cam to new position
//		newCamPosition = Player.position + camPosition;
		newCamPosition = Player.position + camPosition;
		camContainer.position = Vector3.Lerp (camContainer.position, newCamPosition, Time.deltaTime * Smooth);


		// rotate cam
		
		relPlayerPosition = Vector3.Scale (Player.forward, new Vector3 (1, 0, 1)).normalized;
		Quaternion camRotation = Quaternion.LookRotation (relPlayerPosition);
		camContainer.rotation = Quaternion.Lerp (camContainer.rotation, camRotation, Smooth * Time.deltaTime);
		
		/*
		m_CamForward = Vector3.Scale(Player.forward, new Vector3(1, 0, 1)).normalized;

		relPlayerPosition = Player.position - transform.position;
		Quaternion camRotation = Quaternion.LookRotation (relPlayerPosition, Vector3.up);
		transform.rotation = Quaternion.Lerp (transform.rotation, camRotation, Smooth * Time.deltaTime);
		*/
	}
}