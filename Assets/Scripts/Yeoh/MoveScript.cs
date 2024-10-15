using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class MoveScript : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb=GetComponent<Rigidbody>();
        baseMaxSpeed = maxSpeed;
    }

    // ============================================================================

    [Header("Move")]
    public float maxSpeed=3;
    [HideInInspector]
    public float baseMaxSpeed;
    public float acceleration=10;
    public float deceleration=10;

    // ============================================================================

    public void UpdateMove(float magnitude, Vector3 direction)
    {
        float accelRate = Mathf.Abs(magnitude)>0 ? acceleration : deceleration; // use decelerate value if no input, and vice versa
    
        float speedDif = magnitude - Vector3.Dot(direction, rb.velocity); // difference between current and target speed

        float movement = Mathf.Abs(speedDif) * accelRate * Mathf.Sign(speedDif); // slow down or speed up depending on speed difference

        rb.AddForce(direction * movement);
    }

    // ============================================================================

    int tweenSpeedLt=0;
    public void TweenSpeed(float to, float time=.25f)
    {
        LeanTween.cancel(tweenSpeedLt);

        if(time>0)
        {
            tweenSpeedLt = LeanTween.value(maxSpeed, to, time)
                .setEaseInOutSine()
                .setOnUpdate( (float value)=>{maxSpeed=value;} )
                .id;
        }
        else maxSpeed=to;
    }

    // ============================================================================

    public void Push(float force, Vector3 direction)
    {
        rb.velocity = Vector3.zero;

        rb.AddForce(direction*force, ForceMode.Impulse);
    }

    // ============================================================================

    [Header("Debug")]
    public float velocity;

    void FixedUpdate()
    {
        velocity = Round(rb.velocity.magnitude, 2);
    }

    public float Round(float num, int decimalPlaces)
    {
        int factor=1;

        for(int i=0; i<decimalPlaces; i++)
        {
            factor *= 10;
        }

        return Mathf.Round(num * factor) / (float)factor;
    }
}