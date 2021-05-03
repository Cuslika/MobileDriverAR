using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float torque = 1.0f;

    [SerializeField]
    private float minSpeedBeforeTorque = 0.4f;

    [SerializeField]
    private float minSpeedBeforeIdle = 0.2f;

    [SerializeField]
    private Rigidbody carRigidbody = null;

    private Wheel[] wheels = new Wheel[4];

    private Vector3 startingPosition = Vector3.zero;
    private Quaternion startingRotation;
    
    void Awake()
    {
        wheels = GetComponentsInChildren<Wheel>();

        InputManager.Instance.Bind(this);

        startingPosition = transform.parent.position;
        startingRotation = transform.rotation;


    }
    
    // Update is called once per frame
    void Update()
    {
        if (carRigidbody.velocity.magnitude <= minSpeedBeforeIdle)
            AddWheelsSpeed(0);
    }

    public void Accelerate()
    {
        carRigidbody.AddForce(transform.forward * speed, ForceMode.Acceleration);
        AddWheelsSpeed(speed);
    }

    public void Reverse()
    {
        carRigidbody.AddForce(transform.forward * -speed, ForceMode.Acceleration);
        AddWheelsSpeed(-speed);
    }
    public void TurnLeft()
    {
        if(canApplyTorque())
            carRigidbody.AddTorque(transform.up * -torque);
    }

    public void TurnRight()
    {
        if (canApplyTorque())
            carRigidbody.AddTorque(transform.up * torque);
    }

    public void AddWheelsSpeed(float speed)
    {
        foreach (var wheel in wheels)
        {
            wheel.Speed = speed;
        }
    }

    public bool canApplyTorque()
    {
        Vector3 velocity = carRigidbody.velocity;
        return Math.Abs(velocity.x) >= minSpeedBeforeTorque || Math.Abs(velocity.z) >= minSpeedBeforeTorque;
    }

    public void Reset()
    {
        carRigidbody.position = startingPosition;
        carRigidbody.rotation = startingRotation;
    }
}
