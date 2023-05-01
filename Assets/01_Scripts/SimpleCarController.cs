using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

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

    public AudioSource idleSound;
    public AudioSource throttleSound;

    [SerializeField]
    ParticleSystem flameBurst01, flameBurst02, hard01, hard02, soft01, soft02;

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

        if(motor == 0)
        {
            if(!soft01.isPlaying && !soft02.isPlaying)
            {
                hard01.Stop();
                hard02.Stop();
                soft01.Play();
                soft02.Play();
            }
        }
        else
        {
            if(!hard01.isPlaying && !hard02.isPlaying)
            {
                StartCoroutine(MotorStartBurst());
            }
        }

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
        idleSound.pitch = Mathf.Abs(rb.velocity.z) / 100 + 1;
    }

    public void Brake()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            axleInfo.leftWheel.brakeTorque = 4000;
            axleInfo.rightWheel.brakeTorque = 4000;
        }
    }

    IEnumerator MotorStartBurst()
    {
        soft01.Stop();
        soft02.Stop();
        flameBurst01.Play();
        flameBurst02.Play();
        if(!throttleSound.isPlaying )
            throttleSound.Play();
        yield return new WaitForSeconds(.2f);
        hard01.Play();
        hard02.Play();
    }
}