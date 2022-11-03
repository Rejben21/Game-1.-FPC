using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public bool canMove;
    public float moveSpeed;
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(canMove)
        {
            rigidbody.velocity = transform.forward * moveSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthController>().DealDamage();
        }

        if (!other.CompareTag("Enemy") && canMove)
        {
            Destroy(this.gameObject);
        }
    }
}
