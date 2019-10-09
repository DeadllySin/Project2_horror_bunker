using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent Agent;
    [SerializeField] private Transform[] wayPoints;
    int currentWaypoint = 0;

    enum EnemyState { Patrol, Chase }
    [SerializeField] private EnemyState currentState;

    [SerializeField] private Transform target;


    private void Start()
    {
        // Set reference for Navmesh agent
        Agent = GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (currentState == EnemyState.Patrol)
        {
            // code executes for AI to patrol 
            if (Vector3.Distance(transform.position, wayPoints[currentWaypoint].position) < 0.6f)
            {
                currentWaypoint++;
                if (currentWaypoint == wayPoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
            Agent.SetDestination(wayPoints[currentWaypoint].position);
        }
    }

    private void SetDestination()
    {
        if (currentState == EnemyState.Chase)
        {
            Agent.speed = 2f;
            Agent.SetDestination(target.position);
        }

        if (currentState == EnemyState.Patrol)
        {
            Agent.speed = 1f;
            Agent.SetDestination(wayPoints[currentWaypoint].position);
        }
    }
}
