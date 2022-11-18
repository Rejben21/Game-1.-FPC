using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public GameObject hitPrefab;
    public bool shouldDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealthController>().DealDamage();
            Instantiate(hitPrefab, other.transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity, other.transform.parent);

            if (shouldDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
