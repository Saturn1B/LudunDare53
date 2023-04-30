using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    [SerializeField]
    bool AnimalBox;
    [SerializeField]
    bool Ammo;

    float waitTime;
    [SerializeField]
    float waitMin, waitMax;

    Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            //if (Ammo)
            //{
            //    FindObjectOfType<SimpleCarController>().gameObject.GetComponent<Rigidbody>().AddExplosionForce(100000, collision.transform.position, 100000);
            //    transform.GetComponentInChildren<ParticleSystem>().Play();
            //}
            GameManager.Instance.LooseGame();
        }
    }

    private void Start()
    {
        if (AnimalBox)
        {
            rb = GetComponent<Rigidbody>();
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        waitTime = Random.Range(waitMin, waitMax);
        yield return new WaitForSeconds(waitTime);

        float xPos = Random.Range(-4000, 4000);
        float yPos = Random.Range(2000, 5000);

        rb.AddForce(new Vector3(0, yPos, xPos));

        Debug.Log("Shake");

        StartCoroutine(Shake());
    }
}
