using UnityEngine;
using System.Collections;

public class Patrolling : MonoBehaviour
{
	public float radius = 15f;
	public int numberOfPoints = 5;
	public bool patrolling = true;

	Vector3 spawnPosition;
	public Vector3[] points;
	int currentPoint = 0;
	int iterationsCount = 0;
	NavMeshAgent navAgent;
	
	void Start ()
	{
		spawnPosition = transform.position;
		navAgent = GetComponent<NavMeshAgent> ();
		// Generate checkpoints for patrolling
		points = new Vector3[numberOfPoints];
		for (int i = 0; i < numberOfPoints; i++) {
			NavMeshPath path = new NavMeshPath ();
			do {
				points [i] = spawnPosition + (new Vector3 (Random.Range (-radius, radius), transform.position.y, Random.Range (-radius, radius)));
				NavMesh.CalculatePath (transform.position, points [i], NavMesh.AllAreas, path);
				iterationsCount++;
			} while (path.status != NavMeshPathStatus.PathComplete && iterationsCount < 50);
			iterationsCount = 0;
		}
		navAgent.SetDestination (points [currentPoint]);
	}

	void Update ()
	{
		if (patrolling && (navAgent.remainingDistance <= 1f || navAgent.speed == 0f)) { // if the Agent reach current checkpoint - get next one
			currentPoint = (currentPoint < numberOfPoints - 1) ? currentPoint + 1 : 0;  //after last checkpoint go to first
			navAgent.SetDestination (points [currentPoint]);
		}
	}

	public void ResumePatrolling ()
	{
		navAgent.SetDestination (points [currentPoint]);
		patrolling = true;
	}
}
