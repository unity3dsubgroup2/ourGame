using UnityEngine;
using System.Collections;

public class Patrolling : MonoBehaviour
{
    public float radius = 25f;
    public int numberOfPoints = 5;

    Vector3[] points;
    int currentPoint = 0;
    NavMeshAgent navAgent;
	
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        // Generate checkpoints for patrolling
        points = new Vector3[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            NavMeshPath path = new NavMeshPath();
            do
            {
                points [i] = new Vector3(Random.Range(-radius, radius), 2.5f, Random.Range(-radius, radius));
                NavMesh.CalculatePath(transform.position, points [i], NavMesh.AllAreas, path); // TODO: change AllAreas to current area
            } while (path.status != NavMeshPathStatus.PathComplete);

        }
        navAgent.SetDestination(points [currentPoint]);
    }

    void Update()
    {
        if (navAgent.remainingDistance < 0.1f)
        { // if the Agent reach current checkpoint - get next one
            currentPoint = (currentPoint < numberOfPoints - 1) ? currentPoint + 1 : 0;
            navAgent.SetDestination(points [currentPoint]);
        }
    }

}
