using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private int damage = 15;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealthController>().curHealth -= damage;
        }

        if(other.CompareTag("Player") && !other.GetComponent<PlayerHealthController>().godmode)
        {
            other.GetComponent<PlayerHealthController>().curHealth -= damage * 2;
            other.GetComponent<PlayerHealthController>().damagePanel.color = new Color(1, 0, 0, .2f);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
