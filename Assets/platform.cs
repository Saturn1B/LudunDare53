using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public int speed = 3;
    public bool niet = false;

    void Update()
    {
        if(niet == true)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        niet = true;
    }
}
