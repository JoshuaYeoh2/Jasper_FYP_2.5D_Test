using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]

public class AgentJump : MonoBehaviour
{
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // ============================================================================
    
    public float jumpSeconds=.5f;
    bool isJumping;

    void Update()
    {
        // use custom code instead
        agent.autoTraverseOffMeshLink = false;

        if(!isJumping && agent.isOnOffMeshLink)
        {
            Jump();
        }

        if(isJumping)
        {

        }
    }

    void Jump()
    {
        isJumping=true;
    }

    
    
    // ============================================================================

    public UnityEvent OnJump;
    public UnityEvent OnLand;
}
