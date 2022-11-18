using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismembermentElement : MonoBehaviour
{
    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = transform.up * Random.Range(2, 5);
        rigidbody.AddTorque(transform.right * Random.Range(-5, 5));
        rigidbody.AddTorque(transform.up * Random.Range(-5, 5));
    }

    void Update()
    {
        Destroy(gameObject, 10);
    }
}
