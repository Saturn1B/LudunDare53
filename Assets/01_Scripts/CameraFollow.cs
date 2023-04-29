using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject objectToFollow;

    private void FixedUpdate()
    {
        transform.position = new Vector3(9.5f, 4, objectToFollow.transform.position.z);
    }
}
