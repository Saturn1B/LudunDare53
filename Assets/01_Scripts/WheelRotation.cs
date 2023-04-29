using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    WheelCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        transform.position = position;

        transform.Rotate(new Vector3(rb.velocity.z * Time.deltaTime * 50, 0, 0));
    }
}
