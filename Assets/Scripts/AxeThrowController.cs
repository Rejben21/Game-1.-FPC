using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrowController : MonoBehaviour
{
    public float moveSpeed;
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rigidbody.velocity = transform.forward * moveSpeed;
    }
}
