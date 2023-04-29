using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (!GameManager.Instance.hasControl)
            return;

        float motor = maxMotorTorque * Input.GetAxis("Horizontal");
        float steering = maxSteeringAngle * Input.GetAxis("Vertical");

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

            if ((motor > 0 && rb.velocity.z < 0) || (motor < 0 && rb.velocity.z > 0))
            {
                axleInfo.leftWheel.brakeTorque = 800;
                axleInfo.rightWheel.brakeTorque = 800;
            }
            else
            {
                axleInfo.leftWheel.brakeTorque = 0;
                axleInfo.rightWheel.brakeTorque = 0;
            }
        }
    }

    private void Update()
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    public void Brake()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            axleInfo.leftWheel.brakeTorque = 4000;
            axleInfo.rightWheel.brakeTorque = 4000;
        }
    }
}