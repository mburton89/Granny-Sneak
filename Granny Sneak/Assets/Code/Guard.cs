using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public AIWaypoint firstWaypoint;
    Transform player;

    public Light spotlight;
    public float viewDistance;
    public LayerMask viewMask;
    float viewAngle;

    //GAME DESIGN TODOs
    //How should AI Guard behave when Granny runs away
    //Should player lose or should player have chance to run away

    //GAME DEV TODOs
    //Get GitHub account
    //Design level on whiteboard
    //Duplicate scene and develop your level
    //Try to bring in new mixamo character for your Guard

    void Start()
    {
        navMeshAgent.SetDestination(firstWaypoint.transform.position);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            spotlight.color = Color.red;
            navMeshAgent.speed = 4;
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AIWaypoint>())
        {
            navMeshAgent.SetDestination(other.GetComponent<AIWaypoint>().nextWaypoint.transform.position);
        }
    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
