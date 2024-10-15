using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveScript))]
[RequireComponent(typeof(TurnScript))]

public class SteerScript : MonoBehaviour
{
    MoveScript move;
    TurnScript turn;

    void Awake()
    {
        move = GetComponent<MoveScript>();
        turn = GetComponent<TurnScript>();
    }

    // ============================================================================

    public void Steer(Vector3 vector)
    {
        turn.UpdateTurn(vector.normalized);

        float speed = Mathf.Clamp(vector.magnitude, 0, move.maxSpeed); // never go past max speed

        move.UpdateMove(speed, transform.up);
        move.UpdateMove(0, transform.right);
    }
}
