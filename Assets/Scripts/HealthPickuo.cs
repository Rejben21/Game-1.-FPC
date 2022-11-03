using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickuo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthController>().AddHealth();
            Destroy(this.gameObject);
        }
    }
}
