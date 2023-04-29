using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{
    [SerializeField]
    float balanceReactivity = 10;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.hasControl)
            return;

        float balance = Input.GetAxis("Horizontal2");

        if(balance != 0)
        {
            float balanceX = transform.eulerAngles.x + balance * Time.deltaTime * balanceReactivity;

            if (balanceX > 180)
                balanceX -= 360;

            if(balanceX > 20)
            {
                balanceX = 20;
            }
            if (balanceX < -20)
            {
                balanceX = -20;
            }

            transform.eulerAngles = new Vector3(balanceX , 0, 0);
        }
    }
}
