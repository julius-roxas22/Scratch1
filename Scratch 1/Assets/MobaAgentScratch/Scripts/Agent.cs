using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject destinationObj;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        destinationObj = GameObject.Find("Destination");
    }

    void Update()
    {
        agent.SetDestination(destinationObj.transform.position);
        //float dist = (transform.position - agent.destination).sqrMagnitude;
        //if (dist > 0.44f)
        //{
        //}
        //else
        //{
        //    agent.isStopped = true;
        //}

        //Debug.DrawRay(transform.position, agent.destination, Color.red);
    }
}
