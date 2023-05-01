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

    [SerializeField] AudioSource collSound;
    [SerializeField] AudioClip[] collSoundEffects;

    [SerializeField] AudioSource monsterSound;
    [SerializeField] AudioClip[] monsterSoundEffects;

    [SerializeField] ParticleSystem explo01;
    [SerializeField] ParticleSystem explo02;
    [SerializeField] ParticleSystem explo03;

    Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        //collSound.clip = collSoundEffects[Random.Range(0, collSoundEffects.Length)];
        if (collSound)
        {
            collSound.pitch = Random.Range(0.7f, 1.3f);
            collSound.Play();
        }

        if (collision.transform.CompareTag("Ground"))
        {
            if (Ammo)
            {
                FindObjectOfType<SimpleCarController>().gameObject.GetComponent<Rigidbody>().AddExplosionForce(100000, collision.transform.position, 50000);
                transform.GetComponentInChildren<ParticleSystem>().Play();
                explo01.Play();
                explo02.Play();
                explo03.Play();
                StartCoroutine(DestroyBullet());
            }
            GameManager.Instance.LooseGame();
        }
    }

    private void Start()
    {
        if (collSound)
            collSound.clip = collSoundEffects[0];
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

        monsterSound.clip = monsterSoundEffects[Random.Range(0, monsterSoundEffects.Length)];
        monsterSound.pitch = Random.Range(0.85f, 1.15f);
        monsterSound.Play();

        float xPos = Random.Range(-4000, 4000);
        float yPos = Random.Range(2000, 5000);

        rb.AddForce(new Vector3(0, yPos, xPos));

        Debug.Log("Shake");

        StartCoroutine(Shake());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1);
        Destroy(this);
    }
}
