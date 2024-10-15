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
        agent.autoTraverseOffMeshLink = false;
    }

    void Update()
    {
        
    }

    // ============================================================================

    public float jumpTravelTime=.5f;

    bool onNavMeshLink;
    
    // ============================================================================

    public UnityEvent OnJump;
    public UnityEvent OnLand;
}
