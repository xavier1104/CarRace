using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public Transform leftWheelTrans;
    public Transform rightWheelTrans;
    public bool motor;
    public bool steering;
    public bool breakTorque;
}

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float maxBreakTorque;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate ()
    {
        float motor = maxMotorTorque * Input.GetAxis ("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis ("Horizontal");
        float breakTorque = maxBreakTorque * Input.GetAxis ("Break");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }

            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            if (axleInfo.breakTorque)
            {
                axleInfo.leftWheel.brakeTorque = breakTorque;
                axleInfo.rightWheel.brakeTorque = breakTorque;
            }

            ApplyToVisual (axleInfo.leftWheel, axleInfo.leftWheelTrans);
            ApplyToVisual (axleInfo.rightWheel, axleInfo.rightWheelTrans);
        }
    }

    private void ApplyToVisual (WheelCollider collider, Transform visualWheel)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose (out position, out rotation);

        visualWheel.position = position;
        visualWheel.rotation = rotation;
    }
}
