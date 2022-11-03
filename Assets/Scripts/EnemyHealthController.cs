using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthController : MonoBehaviour
{
    public int maxHealth, dealDamage;
    private int curHealth;
    public bool isMage;
    private float timeToHeal = 1;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(curHealth <= 0)
        {
            GetComponent<EnemyController>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Animator>().Play("EnemyDeath");
            Destroy(gameObject, 10);
        }

        if(curHealth > 0 && curHealth < maxHealth)
        {
            Heal();
        }
    }

    public void DealDamage()
    {
        curHealth -= dealDamage;
    }

    private void Heal()
    {
        if(isMage && curHealth < maxHealth)
        {
            timeToHeal -= Time.deltaTime;

            if(timeToHeal <= 0)
            {
                curHealth++;
                timeToHeal = 1;
            }
        }
    }
}
