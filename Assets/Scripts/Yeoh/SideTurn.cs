using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ForceVehicle))]

public class SideTurn : MonoBehaviour
{
    ForceVehicle vehicle;

    void Awake()
    {
        vehicle = GetComponent<ForceVehicle>();
    }

    void FixedUpdate()
    {
        vehicle.Turn(faceR ? Vector3.right : Vector3.left);
    }

    // ============================================================================

    public bool faceR=true;
    public bool reverse;

    public void TryTurn(float dir_x)
    {
        if(reverse)
        {
            if((dir_x>0 && faceR) || (dir_x<0 && !faceR))
            {
                Turn();
            }
        }
        else
        {
            if((dir_x<0 && faceR) || (dir_x>0 && !faceR))
            {
                Turn();
            }
        }
    }

    void Turn()
    {
        faceR=!faceR;
        //transform.Rotate(0, 180, 0);

        if(sprite)
        sprite.flipX = !faceR;
    }

    [Header("Optional")]
    public SpriteRenderer sprite;
}
