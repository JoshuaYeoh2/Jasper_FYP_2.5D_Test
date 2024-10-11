using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(ForceVehicle))]

public class SideMove : MonoBehaviour
{
    ForceVehicle vehicle;

    void Awake()
    {
        vehicle = GetComponent<ForceVehicle>();
    }

    // Input ============================================================================

    Vector2 moveInput;

    void OnInputMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();        
    }

    void Update()
    {
        UpdateMoveInput();
    }   

    public bool canMove=true;

    void UpdateMoveInput()
    {
        if(!canMove) return;

        if(moveInput==Vector2.zero) return;

        dirX = moveInput.x;

        isMoving=true;
    }

    // ============================================================================

    bool isMoving;
    public float dirX;

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

        TryFlip();
    }

    // ============================================================================

    [Header("Flip")]
    public bool faceR=true;
    public bool reverse;
    public SpriteRenderer sprite;

    void TryFlip()
    {
        if(reverse)
        {
            if((dirX>0 && faceR) || (dirX<0 && !faceR))
            {
                Flip();
            }
        }
        else
        {
            if((dirX<0 && faceR) || (dirX>0 && !faceR))
            {
                Flip();
            }
        }
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        faceR=!faceR;

        if(sprite)
        sprite.flipX = !faceR;
    }
}
