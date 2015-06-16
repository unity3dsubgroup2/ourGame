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

	private Vector3 addOfset;
	
	void Start ()
	{
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
		camContainer = transform.parent.transform;
		
		// Setting camera position in scene
		camPosition = transform.parent.transform.position - Player.position;
	}
	
	void FixedUpdate ()
	{
		// additional ofset
		float maxHalf = Mathf.Max (Screen.width, Screen.height) / 2f;
		addOfset = Input.mousePosition - new Vector3 (Screen.width / 2f, Screen.height / 2f, Input.mousePosition.z);
		addOfset.x /= maxHalf;
		addOfset.z = addOfset.y / maxHalf;
		addOfset.y = 0f;
		
		// smooth move cam to new position
		newCamPosition = Player.position + camPosition + addOfset * 2f;
		camContainer.position = Vector3.Lerp (camContainer.position, newCamPosition, Time.deltaTime * Smooth);
	}
}