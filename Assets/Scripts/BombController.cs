using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public GameObject explosionPrefab;
    public float moveSpeed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * moveSpeed;
        rigidbody.AddTorque(transform.right * moveSpeed);
    }

    void Update()
    {
        
    }

    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
