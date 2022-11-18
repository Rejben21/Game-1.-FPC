using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrowController : MonoBehaviour
{
    public float moveSpeed;
    private new Rigidbody rigidbody;
    private bool hit;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * moveSpeed;
    }

    void Update()
    {
        if(!hit)
        {
            rigidbody.AddTorque(transform.right * moveSpeed * 2);
        }

        Destroy(gameObject, 20);
    }

    private void OnCollisionEnter(Collision other)
    {
        hit = true;
    }
}
