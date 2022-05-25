using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject Player;
    public GameObject Sight;
    float SightRange = 50f;
    RaycastHit hit;

    NavMeshAgent agent;
    public float ConstProxiToPlayer = 50;

    Vector3 temp;
    Vector3 centrepos;
    public bool IsSmall = false;

    public int attackDamage = 100;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (IsSmall)
        {
            centrepos = transform.position;
            attackDamage = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsSmall)
        {
            centrepos = Player.transform.position;
        }
        if (Physics.Raycast(Sight.transform.position, (Player.transform.position - Sight.transform.position), out hit, SightRange))
        {
            if (hit.transform == Player.transform)
            {
                agent.SetDestination(hit.point);
            }
        }
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
        {
            if (!IsSmall)
            {
                agent.SetDestination(centrepos + Random.insideUnitSphere * ConstProxiToPlayer);
            }
            else
            {
                temp = centrepos + Random.insideUnitSphere * ConstProxiToPlayer;
                if (!Physics.CheckSphere(temp, 3))
                {
                    agent.SetDestination(temp);
                }
            }

        }
    }

}

