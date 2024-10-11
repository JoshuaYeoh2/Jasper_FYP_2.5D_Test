using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ForceVehicle : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb=GetComponent<Rigidbody>();
        defMaxSpeed = maxSpeed;
        defTurnSpeed = turnSpeed;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("Move")]
    public float maxSpeed=3;
    [HideInInspector] public float defMaxSpeed;

    public void Steer(Vector3 vector)
    {
        Turn(vector.normalized);

        float speed = Mathf.Clamp(vector.magnitude, 0, maxSpeed); // never go past max speed

        Move(speed, transform.up);
        Move(0, transform.right);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public float acceleration=10;
    public float deceleration=10;

    public void Move(float magnitude, Vector3 direction)
    {
        float accelRate = Mathf.Abs(magnitude)>0 ? acceleration : deceleration; // use decelerate value if no input, and vice versa
    
        float speedDif = magnitude - Vector3.Dot(direction, rb.velocity); // difference between current and target speed

        float movement = Mathf.Abs(speedDif) * accelRate * Mathf.Sign(speedDif); // slow down or speed up depending on speed difference

        rb.AddForce(direction * movement);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("Turn")]
    public float turnSpeed=10;
    [HideInInspector] public float defTurnSpeed;
    public bool linearTurn;

    void Turn(Vector3 dir)
    {
        if(dir==Vector3.zero) return;

        Quaternion lookRotation = Quaternion.LookRotation(dir);

        lookRotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f); // only rotate on the Y axis

        transform.rotation = linearTurn ?
            Quaternion.Lerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime): // linearly face the direction
            Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime); // smoothly face the direction
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

    public void Push(float force, Vector3 direction)
    {
        rb.velocity = Vector3.zero;

        rb.AddForce(direction*force, ForceMode.Impulse);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

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