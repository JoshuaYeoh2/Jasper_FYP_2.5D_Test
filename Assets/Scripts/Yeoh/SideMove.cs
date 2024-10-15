using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(ForceVehicle))]

public class SideMove : MonoBehaviour
{
    ForceVehicle vehicle;
    SideTurn turn; // optional

    void Awake()
    {
        vehicle = GetComponent<ForceVehicle>();
        turn = GetComponent<SideTurn>();
    }

    // ============================================================================

    public float dirX;

    public bool canMove=true;
    bool isMoving;

    void Update()
    {
        if(!canMove) return;

        if(moveInput==Vector2.zero) return;

        dirX = moveInput.x;

        isMoving=true;
    }

    void FixedUpdate()
    {
        if(!isMoving) dirX=0;

        Move();

        isMoving=false;
    }

    void Move()
    {
        dirX = vehicle.Round(dirX, 1);
        dirX = Mathf.Clamp(dirX, -1, 1);

        vehicle.Move(vehicle.maxSpeed * dirX, Vector3.right);

        if(turn)
        turn.TryTurn(dirX);
    }

    // ============================================================================

    Vector2 moveInput;

    // temp
    void OnInputMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();        
    }
    
}
