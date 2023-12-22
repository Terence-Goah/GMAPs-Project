using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [System.Serializable]
    public class WheelColliders
    {
        public WheelCollider FLWheel;
        public WheelCollider FRWheel;
        public WheelCollider RLWheel;
        public WheelCollider RRWheel;
    }

    [System.Serializable]
    public class WheelMeshes
    {
        public MeshRenderer FLWheel;
        public MeshRenderer FRWheel;
        public MeshRenderer RLWheel;
        public MeshRenderer RRWheel;
    }

    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;
    public float driveInput;        //acceleration input
    public float steerInput;        //steering input
    public float motorPower;        //sim 'Horsepower' of car 


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();               //check for accelerating or braking 
        ApplyWheelPositions();      //updates the wheel rolling
        ApplyMotor();

    }


    void CheckInput()
    {
        driveInput = Input.GetAxis("Vertical");

    }

    void ApplyMotor()
    {
        colliders.RRWheel.motorTorque = motorPower * driveInput;
        colliders.RLWheel.motorTorque = motorPower * driveInput;
    }

    void ApplyWheelPositions()
    {
        UpdateWheel(colliders.FLWheel, wheelMeshes.FLWheel);
        UpdateWheel(colliders.FRWheel, wheelMeshes.FRWheel);
        UpdateWheel(colliders.RLWheel, wheelMeshes.RLWheel);
        UpdateWheel(colliders.RRWheel, wheelMeshes.RRWheel);

    }

    void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        //to match the rotations of the wheelColliders to the physical wheels!
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;

    }






}
//adapted from Nanousis developemetn