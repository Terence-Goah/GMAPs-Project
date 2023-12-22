using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;
    [SerializeField] PhysicMaterial normalMaterial;
    [SerializeField] PhysicMaterial iceMaterial;

    public float acceleration = 500f;
    public float breakingForce = 600f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentTurnAngle = 0f;

    

    private void FixedUpdate()
    {
        //Accelerates the car forwards and backwards based on W and S keys pressed
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        //Applies a value to break force and brakes the car
        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breakingForce;
        }

        else
        {
            currentBreakForce = 0f;
        }

        //Apply Acceleration to front 2 wheels of the car 
        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;


        //Apply brakes to the car
        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;

        //Steering of the car
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Normal"))
        {
            ApplyMaterial(normalMaterial);
        }
        else if (other.CompareTag("Ice"))
        {
            ApplyMaterial(iceMaterial);
        }
    }

    private void ApplyMaterial(PhysicMaterial newMaterial)
    {
        WheelCollider[] wheelColliders = { frontRight, frontLeft, backRight, backLeft };

        foreach (WheelCollider wheel in wheelColliders)
        {
            WheelFrictionCurve frictionCurve = wheel.forwardFriction;
            frictionCurve.stiffness = newMaterial.dynamicFriction;
            wheel.forwardFriction = frictionCurve;

            frictionCurve = wheel.sidewaysFriction;
            frictionCurve.stiffness = newMaterial.staticFriction;
            wheel.sidewaysFriction = frictionCurve;
        }
    }
}
