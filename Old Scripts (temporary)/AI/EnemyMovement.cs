using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private Transform[] wayPoints;
    int currentWaypoint = 0;

    enum EnemyState { Patrol, Chase }
    [SerializeField] private EnemyState currentState;

    [SerializeField] private float decisionDelay;
    [SerializeField] private Transform target;

    public Fovdetect fovDetect;

    private void Start()
    {
        // Set reference for AI head's detection system
        fovDetect = GameObject.Find("AI_Head").GetComponent<Fovdetect>();

        // Set reference for Navmesh agent
        Agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("SetDestination", 1.5f, decisionDelay);
    }

    private void Update()
    {
        PatrolMovement();
        CheckPlayerDetection();
    }

    private void PatrolMovement()
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
            Agent.speed = 2.0f;
            Agent.SetDestination(target.position);
        }

        if (currentState == EnemyState.Patrol)
        {
            Agent.speed = 1.0f;
            Agent.SetDestination(wayPoints[currentWaypoint].position);
        }
    }

    private void CheckPlayerDetection()
    {
        if (fovDetect.isExposed == true)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Patrol;
        }
    }
}
