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

    void Update()
    {
        agent.autoTraverseOffMeshLink = false;
    }

    // ============================================================================

    public float jumpSeconds=.5f;

    bool isJumping;
    
    // ============================================================================

    public UnityEvent OnJump;
    public UnityEvent OnLand;
}
