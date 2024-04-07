using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CarAI : MonoBehaviour
{
    public float maxSteerAngle;
    public float maxMotorTorque;
    public float maxBreakTorque;
    public float maxSpeed;
    public List<AxleInfo> axleInfos;
    public Transform checkPoint;

    private Navigator navigator;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        navigator = GetComponent<Navigator>();
        rb = GetComponent<Rigidbody> ();
        GetNextCheckPoint ();
    }

    private void FixedUpdate ()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                Steering (axleInfo);
            }

            if (axleInfo.motor)
            {
                MotorControl (axleInfo);
            }

            if (axleInfo.breakTorque)
            {
                BreakControl (axleInfo);
            }

            ApplyToVisual (axleInfo.leftWheel, axleInfo.leftWheelTrans);
            ApplyToVisual (axleInfo.rightWheel, axleInfo.rightWheelTrans);
        }
    }

    private void Steering (AxleInfo axleInfo)
    {
        Vector3 relative = transform.InverseTransformPoint (checkPoint.position);
        float angle = relative.x / relative.magnitude * maxSteerAngle;

        axleInfo.leftWheel.steerAngle = angle;
        axleInfo.rightWheel.steerAngle = angle;
    }

    private void ApplyToVisual (WheelCollider collider, Transform visualWheel)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose (out position, out rotation);

        visualWheel.position = position;
        visualWheel.rotation = rotation;
    }

    private void MotorControl (AxleInfo axleInfo)
    {
        float speed = rb.velocity.magnitude;
        //Debug.Log ("CurrentSpeed:" + speed);
        if (speed >= GetTargetSpeed ())
        {
            axleInfo.leftWheel.motorTorque = 0;
            axleInfo.rightWheel.motorTorque = 0;
            return;
        }

        Vector3 relative = transform.InverseTransformPoint (checkPoint.position);
        float motorTorque = (1 - (Mathf.Abs (relative.x) / relative.magnitude)) * maxMotorTorque;
        axleInfo.leftWheel.motorTorque = motorTorque;
        axleInfo.rightWheel.motorTorque = motorTorque;

        //Debug.Log ("motorTorque:" + motorTorque);
    }

    private void BreakControl (AxleInfo axleInfo)
    {
        float speed = rb.velocity.magnitude;
        if (speed <= GetTargetSpeed ())
        {
            axleInfo.leftWheel.brakeTorque = 0;
            axleInfo.rightWheel.brakeTorque = 0;
            return;
        }

        Vector3 relative = transform.InverseTransformPoint (checkPoint.position);
        float breakTorque = Mathf.Abs (relative.x) / relative.magnitude * maxBreakTorque;

        axleInfo.leftWheel.brakeTorque = breakTorque;
        axleInfo.rightWheel.brakeTorque = breakTorque;

        //Debug.Log ("breakTorque:" + breakTorque);
    }

    private void OnTriggerEnter (Collider other)
    {
        GetNextCheckPoint ();
    }

    private void GetNextCheckPoint ()
    {
        Transform next = navigator.Next ();
        checkPoint.position = next.position;
        checkPoint.rotation = next.rotation;
    }

    private float GetTargetSpeed ()
    {
        Vector3 relative = transform.InverseTransformPoint (checkPoint.position);
        float speed = (1 - Mathf.Abs (relative.x) / relative.magnitude) * maxSpeed;
        //Debug.Log ("TargetSpeed:" + speed);
        return speed;
    }
}
