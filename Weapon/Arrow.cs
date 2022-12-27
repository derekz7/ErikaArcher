using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bx;
    private bool disableRotation;
    //[SerializeField]
    //private float destroyTime = 10f;
    private AudioSource arrowAudio;

    [SerializeField]
    private int damage;
    [SerializeField]
    private ParticleSystem hit;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bx = GetComponent<BoxCollider>();
        arrowAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!disableRotation)
            transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name.ToString());
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Arrow")
        {
            hit.Play();
            arrowAudio.Play();
            disableRotation = true;
            rb.isKinematic = true;
            bx.isTrigger = true;
            Destroy(gameObject,5);
        }
    }


    public void SetDamage(int value)
    {
        this.damage = value;
    }
    public int GetDamage()
    {
        return damage;
    }
}
